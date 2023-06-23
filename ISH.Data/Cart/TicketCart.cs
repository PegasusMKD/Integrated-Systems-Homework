using ISH.Data.Tickets;

namespace ISH.Data.Cart
{
    public class TicketCart
    {
        /**
         * TicketCart serves as a Many-To-Many relation
         *
         * Didn't go for a One-To-Many since that would enable malicious users to "keep tickets hostage" by just bulk adding them to their cart, and never actually buying the tickets
         */
        public Guid Guid { get; set; }
        public Ticket Ticket { get; set; }
        public Cart Cart { get; set; }
    }
}
