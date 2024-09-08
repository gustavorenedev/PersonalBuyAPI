using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using EcommerceAPI.Repositories.Interfaces;
using EcommerceAPI.Services.Interfaces;

namespace EcommerceAPI.Services.Implementations;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<CartDTO> GetCartByClientIdAsync(int clientId)
    {
        var cart = await _cartRepository.GetCartByClientIdAsync(clientId);
        return MapToCartDTO(cart);
    }

    // Cria um carrinho para o cliente, se não existir
    public async Task<CartDTO> CreateCartForClientAsync(int clientId)
    {
        var existingCart = await _cartRepository.GetCartByClientIdAsync(clientId);
        if (existingCart != null)
        {
            return MapToCartDTO(existingCart);
        }

        var cart = new Cart
        {
            ClientId = clientId,
            Items = new List<ItemCart>()
        };

        await _cartRepository.CreateCartAsync(cart);

        return MapToCartDTO(cart);
    }

    // Adiciona um produto ao carrinho existente ou cria um carrinho se não existir
    public async Task<CartDTO> AddProductToCartAsync(int clientId, CartDTO.CartItemDTO cartItemDto)
    {
        var cart = await _cartRepository.GetCartByClientIdAsync(clientId);
        if (cart == null)
        {
            cart = new Cart
            {
                ClientId = clientId,
                Items = new List<ItemCart>()
            };
        }

        var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == cartItemDto.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += cartItemDto.Quantity;
        }
        else
        {
            var product = await _productRepository.GetProductByIdAsync(cartItemDto.ProductId);
            cart.Items.Add(new ItemCart
            {
                ProductId = cartItemDto.ProductId,
                Product = product,
                Quantity = cartItemDto.Quantity
            });
        }

        await _cartRepository.UpdateCartAsync(cart);
        return MapToCartDTO(cart);
    }


    // Remove um produto do carrinho
    public async Task<CartDTO> RemoveProductFromCartAsync(int clientId, int productId)
    {
        var cart = await _cartRepository.GetCartByClientIdAsync(clientId);
        if (cart != null)
        {
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                await _cartRepository.UpdateCartAsync(cart);
            }
        }
        return MapToCartDTO(cart);
    }

    // Atualiza a quantidade de um produto no carrinho
    public async Task<CartDTO> UpdateProductQuantityInCartAsync(int clientId, int productId, int quantityChange)
    {
        var cart = await _cartRepository.GetCartByClientIdAsync(clientId);
        if (cart != null)
        {
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity += quantityChange;
                if (item.Quantity <= 0)
                {
                    cart.Items.Remove(item);
                }
                else
                {
                    item.Product.Price = await _productRepository.GetProductPriceAsync(productId);
                }
                await _cartRepository.UpdateCartAsync(cart);
            }
        }
        return MapToCartDTO(cart);
    }

    private CartDTO MapToCartDTO(Cart cart)
    {
        return new CartDTO
        {
            CartId = cart.CartId,
            ClientId = cart.ClientId,
            Items = cart.Items.Select(i => new CartDTO.CartItemDTO
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                Price = i.Product.Price
            }).ToList()
        };
    }
}
