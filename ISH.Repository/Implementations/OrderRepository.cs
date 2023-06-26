using ISH.Data.Orders;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbSet<Order> _dataset;

        public OrderRepository(DbSet<Order> dataset)
        {
            _dataset = dataset;
        }

        public List<Order> GetAllByBoughtBy(Guid userId) => 
            _dataset.Where(order => order.OrderedBy.Guid == userId).ToList();
    }
}
