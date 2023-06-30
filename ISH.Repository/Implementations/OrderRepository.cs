using ISH.Data.Orders;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public List<Order> GetAllByBoughtBy(Guid userId) => 
            _context.orders.Where(order => order.OrderedBy.Guid == userId).ToList();
    }
}
