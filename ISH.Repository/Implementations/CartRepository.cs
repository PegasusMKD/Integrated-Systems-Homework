using ISH.Data.Cart;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _context;

        public CartRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Cart GetCartByUser(string userId) => _context.carts.Include(cart => cart.Tickets).ThenInclude(ticket => ticket.ViewSlot).First(cart => cart.User.Id == userId);
    }
}
