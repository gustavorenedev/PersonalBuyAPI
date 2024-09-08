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

// Registro dos Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<ICartRepository, CartRepository>();

// Registro dos Services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICartService, CartService>();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ecommerce API",
        Version = "v1",
        Description = "API para gerenciamento de clientes, produtos e carrinhos de compras em um sistema de ecommerce."
    });
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

clientApi.MapPost("/", async ([FromBody] ClientDTO clientDto, IClientService clientService) => Results.Ok(await clientService.CreateClientAsync(clientDto)))
    .WithName("CreateClient")
    .WithTags("Clients")
    .WithDescription("Cria um novo cliente.");

clientApi.MapPut("/{id}", async (int id, [FromBody] ClientDTO clientDto, IClientService clientService) => Results.Ok(await clientService.UpdateClientAsync(id, clientDto)))
    .WithName("UpdateClient")
    .WithTags("Clients")
    .WithDescription("Atualiza os detalhes de um cliente existente.");

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
    .WithDescription("Cria um novo produto.");

productApi.MapPut("/{id}", async (int id, [FromBody] ProductDTO productDto, IProductService productService) => Results.Ok(await productService.UpdateProductAsync(id, productDto)))
    .WithName("UpdateProduct")
    .WithTags("Products")
    .WithDescription("Atualiza os detalhes de um produto existente.");

productApi.MapDelete("/{id}", async (int id, IProductService productService) =>
{
    await productService.DeleteProductAsync(id);
    return Results.NoContent();
})
    .WithName("DeleteProduct")
    .WithTags("Products")
    .WithDescription("Exclui um produto pelo ID.");
#endregion

app.Run();
