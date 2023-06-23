using ISH.Data.Authentication;

namespace ISH.Data.Orders
{
    public class Order
    {
        /**
         * Order serves as the "aggregate" for an order, more specifically, something like a header for the invoice (and it should be the header)
         */
        public Guid Guid { get; set; }
        public string OrderNumber { get; set; } // Has to be unique! (implement some functionality to generate this order number)
        public int TotalPrice { get; set; }
        public User OrderedBy { get; set; }
    }
}
