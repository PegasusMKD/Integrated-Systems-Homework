using ISH.Data.Tickets;

namespace ISH.Repository
{
    public interface ITicketRepository
    {
        int CountTicketsByViewSlot(Guid  viewSlotId);
        List<Ticket> GetTicketsByViewSlot(Guid viewSlotId);
        List<Ticket> GetAllTicketsWithViewSlot();
    }
}
