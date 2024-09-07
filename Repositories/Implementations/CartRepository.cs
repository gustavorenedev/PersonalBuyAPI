using EcommerceAPI.DbContext;
using EcommerceAPI.Model;
using EcommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories.Implementations;

public class CartRepository : ICartRepository
{
    private readonly ApplicationContext _context;

    public CartRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task CreateCart(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<Cart> GetCartByClientId(int clientId)
    {
        return await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
    }

    public async Task UpdateCart(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }
}
