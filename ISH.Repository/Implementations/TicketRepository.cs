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
            _context.tickets.Include(ticket => ticket.ViewSlot).Where(ticket => ticket.ViewSlot.Guid == viewSlotId).ToList();

        public List<Ticket> GetAllTicketsWithViewSlot() =>
        _context.tickets.Include(ticket => ticket.ViewSlot).ToList();

        public List<Ticket> GetAllTicketsByGenreWithViewSlotAndBoughtBy(string? genre) =>
        _context.tickets
            .Include(ticket => ticket.ViewSlot)
            .ThenInclude(slot => slot.Genre)
            .Include(ticket => ticket.BoughtBy)
            .Where(ticket => genre == null || ticket.ViewSlot.Genre.Name == genre)
            .ToList();
    }
}
