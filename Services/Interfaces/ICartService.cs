using EcommerceAPI.DTOs;
using static EcommerceAPI.DTOs.CartDTO;

namespace EcommerceAPI.Services.Interfaces;

public interface ICartService
{
    Task<CartDTO> CreateCartForClientAsync(int clientId);
    Task<CartDTO> GetCartByClientIdAsync(int clientId);
    Task<CartDTO> AddProductToCartAsync(int clientId, AddProductoToCartDTO cartItemDto);
    Task<CartDTO> RemoveProductFromCartAsync(int clientId, int productId);
    Task<CartDTO> UpdateProductQuantityInCartAsync(int clientId, int productId, int quantityChange);
    Task DeleteCartAsync(int clientId);
}
