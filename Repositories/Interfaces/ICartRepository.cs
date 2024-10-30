using EcommerceAPI.Model;

namespace EcommerceAPI.Repositories.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartByClientIdAsync(int clientId);
    Task CreateCartAsync(Cart cart);
    Task UpdateCartAsync(Cart cart);
    Task DeleteCartAsync(int clientId);
}
