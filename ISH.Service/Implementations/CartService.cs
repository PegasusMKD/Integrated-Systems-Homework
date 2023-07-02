using AutoMapper;
using ISH.Data.Cart;
using ISH.Data.Tickets;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Service.Dtos.Cart;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class CartService : ICartService
    {
        private readonly IBaseRepository<Cart> _baseCartRepository;
        private readonly IBaseRepository<Ticket> _baseTicketRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(IBaseRepository<Cart> cartRepository, IMapper mapper, IBaseRepository<Ticket> baseTicketRepository, ICartRepository cartRepository1)
        {
            _baseCartRepository = cartRepository;
            _mapper = mapper;
            _baseTicketRepository = baseTicketRepository;
            _cartRepository = cartRepository1;
        }

        public CartDto RemoveTicket(Guid cartId, Guid ticketId)
        {
            var eCart = _baseCartRepository.GetById(cartId);
            if (eCart == null)
                throw new Exception("Cart doesn't exist!");

            var eTicket = _baseTicketRepository.GetById(ticketId);
            if (eTicket == null)
                throw new Exception("Ticket doesn't exist!");

            eCart.Tickets.Remove(eTicket);
            _baseCartRepository.Update(eCart);
            _baseCartRepository.SaveChangesAsync();
            return _mapper.Map<CartDto>(eCart);
        }

        public CartDto GetCartById(Guid id) => _mapper.Map<CartDto>(_baseCartRepository.GetById(id));

        public void DeleteCartById(Guid id) => _baseCartRepository.Delete(id);

        public CartDto GetCartByUser(string userId) => 
            _mapper.Map<CartDto>(_cartRepository.GetCartByUser(userId));

        public CartDto AddTicket(Guid cartId, Guid ticketId)
        {
            var eCart = _baseCartRepository.GetById(cartId);
            if (eCart == null)
                throw new Exception("Cart doesn't exist!");

            var eTicket = _baseTicketRepository.GetById(ticketId);
            if (eTicket == null)
                throw new Exception("Ticket doesn't exist!");

            eCart.Tickets.Add(eTicket);
            _baseCartRepository.Update(eCart);
            _baseCartRepository.SaveChangesAsync();
            return _mapper.Map<CartDto>(eCart);
        }
    }
}
