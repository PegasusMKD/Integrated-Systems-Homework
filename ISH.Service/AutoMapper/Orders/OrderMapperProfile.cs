using AutoMapper;
using ISH.Data.Orders;
using ISH.Service.Dtos.Authentication;
using ISH.Service.Dtos.Orders;

namespace ISH.Service.AutoMapper.Orders
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}
