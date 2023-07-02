using ISH.Data.Orders;

namespace ISH.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAllByBoughtByWithOrderedByAndItems(string userId);
        List<Order> GetAllByWithOrderedByAndItems();
    }
}
