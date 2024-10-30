using AutoMapper;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;

namespace EcommerceAPI.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();
    }
}
