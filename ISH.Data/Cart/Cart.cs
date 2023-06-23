using ISH.Data.Authentication;

namespace ISH.Data.Cart
{
    public class Cart
    {
        public Guid Guid { get; set; }
        public User User { get; set; }
        public int CartPrice { get; set; }
    }
}
