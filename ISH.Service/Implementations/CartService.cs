using AutoMapper;
using ISH.Data.Cart;
using ISH.Repository.Core;
using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class CartService : ICartService
    {
        private readonly IBaseRepository<Cart> _cartRepository;
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public CartService(IBaseRepository<Cart> cartRepository, IMapper mapper, ITicketService ticketService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _ticketService = ticketService;
        }

        public CartDto RemoveTicket(TicketDto ticket)
        {
            throw new NotImplementedException();
        }

        public CartDto GetCartById(Guid id) => _mapper.Map<CartDto>(_cartRepository.GetById(id));

        public void DeleteCartById(Guid id) => _cartRepository.Delete(id);

        public CartDto GetCartByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public CartDto AddTicket(TicketDto ticket)
        {
            throw new NotImplementedException();
        }
    }
}
