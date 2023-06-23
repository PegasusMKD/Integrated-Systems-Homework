using AutoMapper;
using ISH.Service.Dtos.Cart;

namespace ISH.Service.AutoMapper.Cart
{
    public class CartMapperProfile : Profile
    {
        public CartMapperProfile()
        {
            CreateMap<Data.Cart.Cart, CartDto>();
            CreateMap<CartDto, Data.Cart.Cart>();
        }
    }
}
