using EcommerceAPI.DbContext;
using EcommerceAPI.DTOs;
using EcommerceAPI.Repositories.Implementations;
using EcommerceAPI.Repositories.Interfaces;
using EcommerceAPI.Services.Implementations;
using EcommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


#region builder

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext
builder.Services.AddDbContext<ApplicationContext>(o =>
{
    o.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registro dos Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

// Registro dos Services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHttpClient<IPaymentService, PaymentService>();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Personal Buy API",
        Version = "v1",
        Description = "API para gerenciamento de clientes, produtos e carrinhos de compras, com foco em uma ML para envio de sugestões de compras em emails no sistema de ecommerce."
    });

    // Configurar exemplos de modelos de dados
    //c.SchemaFilter<SwaggerSchemaExamplesFilter>();
});

#endregion

#region app
var app = builder.Build();

// Configuração do Swagger Middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseRouting();
#endregion

#region Rotas para Auth
var authApi = app.MapGroup("/auth");

authApi.MapPost("/login", async ([FromBody] AuthDTO authDto, IAuthService authService) =>
{
    try
    {
        var result = await authService.LoginAsync(authDto);
        return Results.Ok(result);
    }
    catch (UnauthorizedAccessException)
    {
        return Results.Unauthorized();
    }
})
    .WithName("Login")
    .WithTags("Auth")
    .WithDescription("Autentica um cliente e retorna uma mensagem de sucesso.");

authApi.MapPost("/register", async ([FromBody] ClientDTO clientDto, IAuthService authService) =>
{
    try
    {
        await authService.RegisterAsync(clientDto);
        return Results.Created($"/client/{clientDto.Email}", clientDto);
    }
    catch (InvalidOperationException)
    {
        return Results.BadRequest("Email already in use.");
    }
})
    .WithName("Register")
    .WithTags("Auth")
    .WithDescription("Registra um novo cliente no sistema. Não é necessário passar o ID, pode apagar");

#endregion

#region Rotas para Client
var clientApi = app.MapGroup("/client");

clientApi.MapGet("/", async (IClientService clientService) => await clientService.GetAllClientsAsync())
    .WithName("GetAllClients")
    .WithTags("Clients")
    .WithDescription("Obtém todos os clientes cadastrados.");

clientApi.MapGet("/{id}", async (int id, IClientService clientService) => await clientService.GetClientByIdAsync(id) is { } client
    ? Results.Ok(client)
    : Results.NotFound())
    .WithName("GetClientById")
    .WithTags("Clients")
    .WithDescription("Obtém um cliente específico pelo ID.");

clientApi.MapPut("/{id}", async (int id, [FromBody] ClientDTO clientDto, IClientService clientService) => Results.Ok(await clientService.UpdateClientAsync(id, clientDto)))
    .WithName("UpdateClient")
    .WithTags("Clients")
    .WithDescription("Atualiza os detalhes de um cliente existente. Não é necessário passar o ID, pode apagar");

clientApi.MapDelete("/{id}", async (int id, IClientService clientService) =>
{
    await clientService.DeleteClientAsync(id);
    return Results.NoContent();
})
    .WithName("DeleteClient")
    .WithTags("Clients")
    .WithDescription("Exclui um cliente pelo ID.");
#endregion

#region Rotas para Product
var productApi = app.MapGroup("/product");

productApi.MapGet("/", async (IProductService productService) => await productService.GetAllProductsAsync())
    .WithName("GetAllProducts")
    .WithTags("Products")
    .WithDescription("Obtém todos os produtos disponíveis.");

productApi.MapGet("/{id}", async (int id, IProductService productService) => await productService.GetProductByIdAsync(id) is { } product
    ? Results.Ok(product)
    : Results.NotFound())
    .WithName("GetProductById")
    .WithTags("Products")
    .WithDescription("Obtém um produto específico pelo ID.");


productApi.MapPost("/", async ([FromBody] ProductDTO productDto, IProductService productService) => Results.Ok(await productService.CreateProductAsync(productDto)))
    .WithName("CreateProduct")
    .WithTags("Products")
    .WithDescription("Cria um novo produto. Não é necessário passar o ID, pode apagar");

productApi.MapPut("/{id}", async (int id, [FromBody] ProductDTO productDto, IProductService productService) => Results.Ok(await productService.UpdateProductAsync(id, productDto)))
    .WithName("UpdateProduct")
    .WithTags("Products")
    .WithDescription("Atualiza os detalhes de um produto existente. Não é necessário passar o ID, pode apagar");

productApi.MapDelete("/{id}", async (int id, IProductService productService) =>
{
    await productService.DeleteProductAsync(id);
    return Results.NoContent();
})
    .WithName("DeleteProduct")
    .WithTags("Products")
    .WithDescription("Exclui um produto pelo ID.");
#endregion

#region Rotas para Cart
var cartApi = app.MapGroup("/cart");

cartApi.MapGet("/{clientId}", async (int clientId, ICartService cartService) =>
    await cartService.GetCartByClientIdAsync(clientId) is { } cart
        ? Results.Ok(cart)
        : Results.NotFound())
    .WithName("GetCartByClientId")
    .WithTags("Carts")
    .WithDescription("Obtém o carrinho de compras de um cliente pelo ID.");

cartApi.MapPost("/{clientId}/create", async (int clientId, ICartService cartService) =>
{
    var createCart = await cartService.CreateCartForClientAsync(clientId);
    return Results.Ok(createCart);
})
    .WithName("CreateCartForClient")
    .WithTags("Carts")
    .WithDescription("Cria um carrinho de compras para um cliente.");

cartApi.MapPost("/{clientId}/add", async (int clientId, [FromBody] AddProductoToCartDTO dto, ICartService cartService) =>
{
    var updatedCart = await cartService.AddProductToCartAsync(clientId, dto);
    return Results.Ok(updatedCart);
})
    .WithName("AddProductToCart")
    .WithTags("Carts")
    .WithDescription("Adiciona um produto ao carrinho de compras de um cliente.");

cartApi.MapDelete("/{clientId}/remove/{productId}", async (int clientId, int productId, ICartService cartService) =>
{
    var updatedCart = await cartService.RemoveProductFromCartAsync(clientId, productId);
    return Results.Ok(updatedCart);
})
    .WithName("RemoveProductFromCart")
    .WithTags("Carts")
    .WithDescription("Remove um produto do carrinho de compras de um cliente.");

cartApi.MapPut("/{clientId}/update/{productId}/{quantityChange}", async (int clientId, int productId, int quantityChange, ICartService cartService) =>
{
    var updatedCart = await cartService.UpdateProductQuantityInCartAsync(clientId, productId, quantityChange);
    return Results.Ok(updatedCart);
})
    .WithName("UpdateProductQuantityInCart")
    .WithTags("Carts")
    .WithDescription("Atualiza a quantidade de um produto no carrinho de compras de um cliente.");

cartApi.MapDelete("/{clientId}/delete", async (int clientId, ICartService cartService) =>
{
    try
    {
        await cartService.DeleteCartAsync(clientId);
        return Results.NoContent(); // Retorna 204 No Content se a exclusão for bem-sucedida
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(ex.Message); // Retorna 404 se o carrinho não for encontrado
    }
})
    .WithName("DeleteCart")
    .WithTags("Carts")
    .WithDescription("Exclui o carrinho de compras de um cliente.");
#endregion

#region Rota para Pagamento
var paymentApi = app.MapGroup("/Payment");

paymentApi.MapPost("/{clientId}", async (int clientId, IPaymentService paymentService) =>
{
    var payment = await paymentService.PaymentCart(clientId);
    return Results.Ok(payment);
})
    .WithName("Payment")
    .WithTags("Payments")
    .WithDescription("Faz o pagamento do carrinho do cliente");

#endregion

app.Run();
