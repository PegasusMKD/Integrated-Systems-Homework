using ISH.Data.Orders;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DbSet<OrderItem> _dataset;

        public OrderItemRepository(DbSet<OrderItem> dataset)
        {
            _dataset = dataset;
        }

        public List<OrderItem> GetOrderItemsByOrder(Guid orderGuid) =>
            _dataset.Where(item => item.Order.Guid == orderGuid).ToList();
    }
}
