using ISH.Data.Cart;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly DbSet<Cart> _dataset;
        public Cart GetCartByUser(Guid userId) => _dataset.First(cart => cart.User.Guid == userId);
    }
}
