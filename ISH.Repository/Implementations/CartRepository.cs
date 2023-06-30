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

        public Cart GetCartByUser(Guid userId) => _context.carts.First(cart => cart.User.Guid == userId);
    }
}
