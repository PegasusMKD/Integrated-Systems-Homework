using ISH.Data.Cart;

namespace ISH.Repository
{
    public interface ICartRepository
    {
        Cart? GetCartByUser(string userId);
    }
}
