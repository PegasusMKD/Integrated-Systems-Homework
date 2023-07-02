using AutoMapper;
using ISH.Data.Orders;
using ISH.Data.Tickets;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Repository.Implementations;
using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Orders;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _baseOrderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        private readonly IOrderItemService _orderItemService;

        public OrderService(IBaseRepository<Order> baseOrderRepository, IMapper mapper, IOrderItemService orderItemService, IOrderRepository orderRepository, ITicketService ticketService, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _baseOrderRepository = baseOrderRepository;
            _mapper = mapper;
            _orderItemService = orderItemService;
            _orderRepository = orderRepository;
            _ticketService = ticketService;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        public OrderDto CreateOrder(string userId)
        {
            var eUser = _userRepository.GetUserById(userId);
            if (eUser == null)
                throw new Exception("User does not exist!");

            // TODO: Add payment requirement
            var eCart = _cartRepository.GetCartByUser(eUser.Id);
            if (eCart == null)
                throw new Exception("Cart doesn't exist!");
            
            foreach (var eCartTicket in eCart.Tickets)
            {
                if (eCartTicket.BoughtBy != null || eCartTicket.TicketStatus == TicketStatus.Bought)
                    throw new Exception("Ticket already purchased by someone!");

                eCartTicket.TicketStatus = TicketStatus.Bought;
                eCartTicket.BoughtBy = eCart.User;
                _ticketService.UpdateTicket(_mapper.Map<TicketDto>(eCartTicket));
            }

            var order = new Order
            {
                OrderedBy = eCart.User,
                TotalPrice = eCart.Tickets.Sum(ticket => ticket.Price)
            };

            order = _baseOrderRepository.Create(order);

            var orderItems = eCart.Tickets.GroupBy(
                ticket => ticket.ViewSlot.Guid,
                ticket => ticket,
                (_, tickets) =>
                {
                    var ticketsList = tickets.ToList();
                    var sampleTicket = ticketsList.First();

                    return new OrderItem
                    {
                        ItemPrice = sampleTicket.Price * ticketsList.Count,
                        TicketPrice = sampleTicket.Price,
                        MovieName = sampleTicket.ViewSlot.MovieName,
                        TimeSlot = sampleTicket.ViewSlot.TimeSlot,
                        Quantity = ticketsList.Count,
                        Order = order
                    };
                }
            ).Select(_mapper.Map<OrderItemDto>).ToList();
            orderItems = _orderItemService.CreateOrderItems(orderItems);

            _baseOrderRepository.SaveChangesAsync();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = orderItems;
            return orderDto;
        }

        public void NotifyUser(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public void GenerateInvoice(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public List<OrderDto> GetOrdersByUser(string userId) =>
            _orderRepository.GetAllByBoughtBy(userId).Select(_mapper.Map<OrderDto>).ToList();

        public List<OrderDto> GetOrders() => 
            _baseOrderRepository.GetAll().Select(order => _mapper.Map<OrderDto>(order)).ToList();
    }
}
