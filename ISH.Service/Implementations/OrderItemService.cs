using AutoMapper;
using ISH.Data.Orders;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Service.Dtos.Orders;

namespace ISH.Service.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IBaseRepository<OrderItem> _baseRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IBaseRepository<OrderItem> baseRepository, IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public List<OrderItemDto> GetOrderItemsByOrderId(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public List<OrderItemDto> CreateOrderItems(List<OrderItemDto> orderItems)
        {
            throw new NotImplementedException();
        }
    }
}
