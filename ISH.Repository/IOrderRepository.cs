using ISH.Data.Orders;

namespace ISH.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAllByBoughtByWithOrderedBy(string userId);
        Order GetByIdWithOrderedBy(Guid orderId);
        List<Order> GetAllByWithOrderedBy();
    }
}
