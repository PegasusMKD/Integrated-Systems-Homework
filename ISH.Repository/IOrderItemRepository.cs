using ISH.Data.Orders;

namespace ISH.Repository
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetOrderItemsByOrder(Guid orderGuid);
    }
}
