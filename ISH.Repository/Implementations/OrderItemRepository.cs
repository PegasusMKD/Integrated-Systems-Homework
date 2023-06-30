using ISH.Data.Orders;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationContext _context;

        public OrderItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<OrderItem> GetOrderItemsByOrder(Guid orderGuid) =>
            _context.orderItems.Where(item => item.Order.Guid == orderGuid).ToList();
    }
}
