using ISH.Data.Tickets;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DbSet<Ticket> _dataset;

        public TicketRepository(DbSet<Ticket> dataset)
        {
            _dataset = dataset;
        }


        public int CountTicketsByViewSlot(Guid viewSlotId) => 
            _dataset.Count(ticket => ticket.ViewSlot.Guid == viewSlotId);

        public List<Ticket> GetTicketsByViewSlot(Guid viewSlotId) =>
            _dataset.Where(ticket => ticket.ViewSlot.Guid == viewSlotId).ToList();
    }
}
