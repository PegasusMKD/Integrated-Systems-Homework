using AutoMapper;
using ISH.Data.Orders;
using ISH.Service.Dtos.Orders;

namespace ISH.Service.AutoMapper.Orders
{
    public class OrderItemMapperProfile : Profile
    {
        public OrderItemMapperProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
