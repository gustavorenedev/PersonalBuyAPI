using AutoMapper;
using EcommerceAPI.DTOs;
using EcommerceAPI.Model;

namespace EcommerceAPI.Mapping;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartViewDTO>();
        CreateMap<CartViewDTO, Cart>();

        CreateMap<ItemCart, CartViewDTO.CartItemViewDTO>()
            .ForMember(dest => dest.Product, opt => opt.Ignore()); // Ignora Product por enquanto

        CreateMap<CartViewDTO.CartItemViewDTO, ItemCart>();
    }
}
