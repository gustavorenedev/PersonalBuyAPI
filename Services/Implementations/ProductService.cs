using EcommerceAPI.DTOs;
using EcommerceAPI.Services.Interfaces;

namespace EcommerceAPI.Services.Implementations;

public class ProductService : IProductService
{
    public Task<ProductDTO> CreateProductAsync(ProductDTO productDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductDTO> GetProductByIdAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDTO> UpdateProductAsync(int productId, ProductDTO productDto)
    {
        throw new NotImplementedException();
    }
}