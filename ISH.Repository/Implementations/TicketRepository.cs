using ISH.Data.Tickets;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationContext _context;


        public TicketRepository(ApplicationContext context)
        {
            _context = context;
        }

        public int CountTicketsByViewSlot(Guid viewSlotId) => 
            _context.tickets.Count(ticket => ticket.ViewSlot.Guid == viewSlotId);

        public List<Ticket> GetTicketsByViewSlot(Guid viewSlotId) =>
            _context.tickets.Where(ticket => ticket.ViewSlot.Guid == viewSlotId).ToList();
    }
}
