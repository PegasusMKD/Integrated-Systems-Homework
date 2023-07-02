using ISH.Data.Orders;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Order> GetAllByBoughtByWithOrderedByAndItems(string userId) =>
            _context.orders.Where(order => order.OrderedBy.Id == userId).Include(order => order.OrderedBy).ToList();

        public List<Order> GetAllByWithOrderedByAndItems() =>
            _context.orders.Include(order => order.OrderedBy).ToList();
    }
}
