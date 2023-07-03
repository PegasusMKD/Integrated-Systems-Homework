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

        public List<Order> GetAllByBoughtByWithOrderedBy(string userId) =>
            _context.orders.Where(order => order.OrderedBy.Id == userId).Include(order => order.OrderedBy).ToList();

        public Order GetByIdWithOrderedBy(Guid orderId) =>
            _context.orders.Include(order => order.OrderedBy).SingleOrDefault(order => order.Guid == orderId);

        public List<Order> GetAllByWithOrderedBy() =>
            _context.orders.Include(order => order.OrderedBy).ToList();
    }
}
