using AutoMapper;
using ISH.Data.Authentication;
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
        private readonly IUserRepository _userRepository;

        public CartService(IBaseRepository<Cart> cartRepository, IMapper mapper, IBaseRepository<Ticket> baseTicketRepository, ICartRepository cartRepository1, IUserRepository userRepository)
        {
            _baseCartRepository = cartRepository;
            _mapper = mapper;
            _baseTicketRepository = baseTicketRepository;
            _cartRepository = cartRepository1;
            _userRepository = userRepository;
        }

        public CartDto AddTicket(string userId, Guid ticketId)
        {
            var eUser = _userRepository.GetUserById(userId);
            if (eUser == null)
                throw new Exception("User does not exist!");

            var eCart = _cartRepository.GetCartByUser(eUser.Id);
            if (eCart == null)
                throw new Exception("Cart doesn't exist!");

            var eTicket = _baseTicketRepository.GetById(ticketId);
            if (eTicket == null)
                throw new Exception("Ticket doesn't exist!");

            eCart.Tickets.Add(eTicket);
            eCart.CartPrice += eTicket.Price;
            _baseCartRepository.Update(eCart);
            _baseCartRepository.SaveChanges();
            return _mapper.Map<CartDto>(eCart);
        }

        public CartDto RemoveTicket(string userId, Guid ticketId)
        {
            var eUser = _userRepository.GetUserById(userId);
            if (eUser == null)
                throw new Exception("User does not exist!");

            var eCart = _cartRepository.GetCartByUser(eUser.Id);
            if (eCart == null)
                throw new Exception("Cart doesn't exist!");

            var eTicket = _baseTicketRepository.GetById(ticketId);
            if (eTicket == null)
                throw new Exception("Ticket doesn't exist!");

            eCart.Tickets.Remove(eTicket);
            eCart.CartPrice -= eTicket.Price;
            _baseCartRepository.Update(eCart);
            _baseCartRepository.SaveChanges();
            return _mapper.Map<CartDto>(eCart);
        }

        public CartDto GetCartById(Guid id) => _mapper.Map<CartDto>(_baseCartRepository.GetById(id));

        public void DeleteCartById(Guid id) => _baseCartRepository.Delete(id);
        public void DeleteCartByUser(string id)
        {
            var eUser = _userRepository.GetUserById(id);
            if (eUser == null)
                throw new Exception("User does not exist!");

            var eCart = _cartRepository.GetCartByUser(eUser.Id);
            if (eCart == null)
                throw new Exception("Cart doesn't exist!");
            DeleteCartById(eCart.Guid);
        }

        public CartDto CreateCartForUser(string userId)
        {
            var eUser = _userRepository.GetUserById(userId);
            if (eUser == null)
                throw new Exception("User does not exist!");
            Cart cart = new Cart
            {
                User = eUser
            };
            _baseCartRepository.Create(cart);
            _baseCartRepository.SaveChanges();
            return _mapper.Map<CartDto>(cart);
        }

        public CartDto GetCartByUser(string userId) => 
            _mapper.Map<CartDto>(_cartRepository.GetCartByUser(userId));
        
    }
}
