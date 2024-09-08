using AutoMapper;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;
using EcommerceAPI.Repositories.Interfaces;
using EcommerceAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDTO> CreateProductAsync(ProductDTO productDto)
    {
        if (productDto == null)
            throw new ArgumentNullException(nameof(productDto), "Os dados do produto não podem ser nulos");

        ValidateProductDto(productDto);

        var existingProduct = await _productRepository.GetProductByNameAsync(productDto.Name);
        if (existingProduct != null)
            throw new InvalidOperationException("Já existe um produto com o mesmo nome");

        var product = _mapper.Map<Product>(productDto);
        var createdProduct = await _productRepository.CreateProductAsync(product);
        return _mapper.Map<ProductDTO>(createdProduct);
    }

    public async Task DeleteProductAsync(int productId)
    {
        var existingProduct = await _productRepository.GetProductByIdAsync(productId);
        if (existingProduct == null)
            throw new KeyNotFoundException($"Produto com ID {productId} não encontrado");

        await _productRepository.DeleteProductAsync(productId);
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null)
            throw new KeyNotFoundException($"Produto com ID {productId} não encontrado");

        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO> UpdateProductAsync(int productId, ProductDTO productDto)
    {
        if (productDto == null)
            throw new ArgumentNullException(nameof(productDto), "Os dados do produto não podem ser nulos");

        ValidateProductDto(productDto);

        var existingProduct = await _productRepository.GetProductByIdAsync(productId);
        if (existingProduct == null)
            throw new KeyNotFoundException($"Produto com ID {productId} não encontrado");

        var product = _mapper.Map<Product>(productDto);
        var updatedProduct = await _productRepository.UpdateProductAsync(productId, product);
        return _mapper.Map<ProductDTO>(updatedProduct);
    }

    private void ValidateProductDto(ProductDTO productDto)
    {
        // Valida se o nome do produto foi fornecido
        if (string.IsNullOrWhiteSpace(productDto.Name))
            throw new ValidationException("O nome do produto é obrigatório");

        // Valida se o preço é um valor positivo
        if (productDto.Price <= 0)
            throw new ValidationException("O preço do produto deve ser um valor positivo");
    }
}