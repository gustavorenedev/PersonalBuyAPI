using EcommerceAPI.Model;

namespace EcommerceAPI.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(int productId, Product product);
    Task DeleteProductAsync(int productId);
}
