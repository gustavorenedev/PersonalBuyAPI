using EcommerceAPI.DbContext;
using EcommerceAPI.Model;
using EcommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationContext _context;

    public ProductRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
    }

    public async Task<Product> GetProductByNameAsync(string name)
    {
        return await _context.Products
                .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<decimal> GetProductPriceAsync(int productId)
    {
        return await _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => p.Price)
                .FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> UpdateProductAsync(int productId, Product product)
    {
        var existingProduct = await _context.Products.FindAsync(productId);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.Quantity = product.Quantity;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        return null;
    }
}
