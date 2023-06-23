namespace ISH.Data.Orders
{
    public class OrderItem
    {
        /**
         * OrderItem serves as a singular item in the order (and thus the invoice).
         *
         *
         * All of the "View Slot" info will be added here as a per-line basis since we want to let the user buy multiple tickets, for multiple time slots
         *
         * For example,
         *  he should be able to order 2 tickets to watch "movie A" at 23-06-2023T10:00:00
         *  and also order a ticket to watch "movie C" at 24-06-2023T22:00:00
         */
        public Guid Guid { get; set; }
        public int TicketPrice { get; set; }
        public int ItemPrice { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; } // Serves if the cashier/user wants to leave some comment on the specific line
        public DateTime TimeSlot { get; set; }
        public string MovieName { get; set; }
    }
}
