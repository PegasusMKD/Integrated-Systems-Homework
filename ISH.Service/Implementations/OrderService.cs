using AutoMapper;
using ISH.Data.Orders;
using ISH.Repository.Core;
using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Orders;

namespace ISH.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderItemService _orderItemService;
        private readonly ICartService _cartService;

        public OrderService(IBaseRepository<Order> orderRepository, IMapper mapper, IOrderItemService orderItemService, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderItemService = orderItemService;
            _cartService = cartService;
        }

        public OrderDto CreateOrder(CartDto cart)
        {
            throw new NotImplementedException();
        }

        public void NotifyUser(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public void GenerateInvoice(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public List<OrderDto> GetOrdersByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<OrderDto> GetOrders() => _orderRepository.GetAll().Select(order => _mapper.Map<OrderDto>(order)).ToList();
    }
}
