using ISH.Data.Authentication;

namespace ISH.Data.Tickets
{
    public class Ticket
    {
        /**
         * Ticket represents a singular ticket that the cinema has created.
         */
        public Guid Guid { get; set; }
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Available;
        public int Price { get; set; }
        public string Seat { get; set; } // Will have some naming scheme "XYY" (where X is some letter for the row, and YY is the column, aka position in the row)
        public ViewSlot ViewSlot { get; set; }
        public User? BoughtBy { get; set; }
    }
}
