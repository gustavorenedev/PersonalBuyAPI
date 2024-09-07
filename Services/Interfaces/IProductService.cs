using EcommerceAPI.DTOs;

namespace EcommerceAPI.Services.Interfaces;

public interface IProductService
{
    Task<ProductDTO> GetProductByIdAsync(int productId);
    Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
    Task<ProductDTO> CreateProductAsync(ProductDTO productDto);
    Task<ProductDTO> UpdateProductAsync(int productId, ProductDTO productDto);
    Task DeleteProductAsync(int productId);
}
