using AutoMapper;
using ShopApp.Entities;

namespace ShopApp.RaCruds;

public class RaProfile : Profile
{
    public RaProfile()
    {
        CreateMap<Cart, CartDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<CartItem, CartItemDto>();
    }
}
