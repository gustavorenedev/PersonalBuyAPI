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

    public async Task CreateCartAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<Cart> GetCartByClientIdAsync(int clientId)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);
    }

    public async Task UpdateCartAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCartAsync(int clientId)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);

        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }
}
