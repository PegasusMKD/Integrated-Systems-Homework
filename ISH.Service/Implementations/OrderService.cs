﻿using AutoMapper;
using ISH.Data.Cart;
using ISH.Data.Orders;
using ISH.Data.Tickets;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Repository.Implementations;
using ISH.Service.Dtos;
using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Orders;
using ISH.Service.Dtos.Stripe;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _baseOrderRepository;
        private readonly IBaseRepository<Ticket> _baseTicketRepository;
        private readonly IBaseRepository<OrderItem> _baseOrderItemsRepository;
        private readonly IBaseRepository<Cart> _baseCartRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        private readonly IStripeService _stripeService;
        private readonly IMailService _mailService;

        private readonly IMapper _mapper;

        public OrderService(
            IBaseRepository<Order> baseOrderRepository, IMapper mapper, IOrderRepository orderRepository, ICartRepository cartRepository,
            IUserRepository userRepository, IBaseRepository<Ticket> baseTicketRepository, IBaseRepository<OrderItem> baseOrderItemsRepository,
            IBaseRepository<Cart> baseCartRepository, IOrderItemRepository orderItemRepository, IStripeService stripeService, IMailService mailService)
        {
            _baseOrderRepository = baseOrderRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _baseTicketRepository = baseTicketRepository;
            _baseOrderItemsRepository = baseOrderItemsRepository;
            _baseCartRepository = baseCartRepository;
            _orderItemRepository = orderItemRepository;
            _stripeService = stripeService;
            _mailService = mailService;
        }

        public OrderDto CreateOrder(string userId, AddStripeCard paymentDetails, CancellationToken ct)
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
                _baseTicketRepository.Update(eCartTicket);
            }

            var order = new Order
            {
                OrderedBy = eCart.User,
                TotalPrice = eCart.Tickets.Sum(ticket => ticket.Price)
            };

            order = _baseOrderRepository.Create(order);

            var orderItems = eCart.Tickets.GroupBy(
                ticket => new { ticket.ViewSlot.Guid, ticket.Price},
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
            ).Select(_baseOrderItemsRepository.Create)
                .Select(_mapper.Map<OrderItemDto>)
                .ToList();

            _baseCartRepository.Delete(eCart.Guid);
            var paymentSuccess = _stripeService.AddStripePaymentAsync(eUser, order.TotalPrice, paymentDetails, ct);
            if (!paymentSuccess)
            {
                return null;
            }
            _baseOrderRepository.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = orderItems;
            NotifyUser(orderDto);
            return orderDto;
        }

        public OrderDto? CreateOrder(string userId, string stripeEmail, string stripeToken, CancellationToken ct)
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
                _baseTicketRepository.Update(eCartTicket);
            }

            var order = new Order
            {
                OrderedBy = eCart.User,
                TotalPrice = eCart.Tickets.Sum(ticket => ticket.Price)
            };

            order = _baseOrderRepository.Create(order);

            var orderItems = eCart.Tickets.GroupBy(
                ticket => new { ticket.ViewSlot.Guid, ticket.Price },
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
            ).Select(_baseOrderItemsRepository.Create)
                .Select(_mapper.Map<OrderItemDto>)
                .ToList();

            _baseCartRepository.Delete(eCart.Guid);
            var paymentSuccess = _stripeService.AddStripePaymentAsync(eUser, order.TotalPrice, stripeEmail, stripeToken, ct);
            if (!paymentSuccess)
            {
                return null;
            }
            _baseOrderRepository.SaveChanges();

            var orderDto = _mapper.Map<OrderDto>(order);
            orderDto.Items = orderItems;
            NotifyUser(orderDto);
            return orderDto;
        }

        public void NotifyUser(OrderDto order)
        {
            _mailService.SendMail(new MailDataDto
            {
                EmailBody = $"Your order was successful for \"Order#{order.OrderNumber}\"!",
                EmailSubject = "Order confirmation - Integrated Systems",
                EmailToId = order.OrderedBy.Email,
                EmailToName = order.OrderedBy.UserName
            });
        }

        public List<OrderDto> GetOrdersByUser(string userId) =>
            _orderRepository.GetAllByBoughtByWithOrderedBy(userId)
                .Select(_mapper.Map<OrderDto>)
                .Select(order =>
                {
                    order.Items = _orderItemRepository.GetOrderItemsByOrder(order.Guid)
                        .Select(_mapper.Map<OrderItemDto>)
                        .ToList();
                    return order;
                })
                .ToList();

        public OrderDto GetOrderById(Guid orderId)
        {
            var order = _mapper.Map<OrderDto>(_orderRepository.GetByIdWithOrderedBy(orderId));
            order.Items = _orderItemRepository.GetOrderItemsByOrder(orderId).Select(_mapper.Map<OrderItemDto>).ToList();
            return order;
        }

        public List<OrderDto> GetOrders() =>
            _orderRepository.GetAllByWithOrderedBy().Select(_mapper.Map<OrderDto>)
                .Select(order =>
                {
                    order.Items = _orderItemRepository.GetOrderItemsByOrder(order.Guid)
                        .Select(_mapper.Map<OrderItemDto>)
                        .ToList();
                    return order;
                })
                .ToList();
    }
}
