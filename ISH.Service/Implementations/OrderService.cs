using AutoMapper;
using ISH.Data.Cart;
using ISH.Data.Orders;
using ISH.Data.Tickets;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Orders;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _baseOrderRepository;
        private readonly IBaseRepository<Cart> _baseCartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        private readonly IOrderItemService _orderItemService;

        public OrderService(IBaseRepository<Order> baseOrderRepository, IMapper mapper, IOrderItemService orderItemService, IOrderRepository orderRepository, IBaseRepository<Cart> baseCartRepository, ITicketService ticketService)
        {
            _baseOrderRepository = baseOrderRepository;
            _mapper = mapper;
            _orderItemService = orderItemService;
            _orderRepository = orderRepository;
            _baseCartRepository = baseCartRepository;
            _ticketService = ticketService;
        }

        public OrderDto CreateOrder(Guid cartId)
        {
            // TODO: Add payment requirement
            var eCart = _baseCartRepository.GetById(cartId);
            if (eCart == null)
                throw new Exception("Cart does not exist anymore!");


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
