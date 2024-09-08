using AutoMapper;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}