using EcommerceAPI.DTOs;
using static EcommerceAPI.DTOs.CartDTO;

namespace EcommerceAPI.Services.Interfaces;

public interface ICartService
{
    Task<CartDTO> GetCartByClientIdAsync(int clientId);
    Task<CartDTO> AddProductToCartAsync(int clientId, CartItemDTO cartItemDto);
    Task<CartDTO> UpdateCartAsync(int clientId, CartDTO cartDto);
}
