using EcommerceAPI.Model;

namespace EcommerceAPI.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(int productId, Product product);
    Task DeleteProductAsync(int productId);
    Task<Product> GetProductByNameAsync(string name);
    Task<Product> GetProductByIdAsync(int productId);
}
