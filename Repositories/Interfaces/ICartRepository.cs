using EcommerceAPI.Model;

namespace EcommerceAPI.Repositories.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartByClientId(int clientId);
    Task CreateCart(Cart cart);
    Task UpdateCart(Cart cart);
}
