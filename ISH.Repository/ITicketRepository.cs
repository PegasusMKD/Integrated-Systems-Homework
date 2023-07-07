using ISH.Data.Tickets;

namespace ISH.Repository
{
    public interface ITicketRepository
    {
        int CountTicketsByViewSlot(Guid  viewSlotId);
        List<Ticket> GetTicketsByViewSlot(Guid viewSlotId);
        List<Ticket> GetAllTicketsWithViewSlot();
        List<Ticket> GetAllTicketsByGenreWithViewSlotAndBoughtBy(string? genre);
        Ticket GetTicketWithViewSlot(Guid id);
        List<Ticket> FilterByDates(DateTime? filterFromTimeSlot, DateTime? filterToTimeSlot);
    }
}
