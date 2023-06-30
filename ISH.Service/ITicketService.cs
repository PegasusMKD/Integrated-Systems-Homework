using ISH.Service.Dtos.Tickets;

namespace ISH.Service
{
    public interface ITicketService
    {
        TicketDto CreateTicket(TicketDto ticket);
        TicketDto GetById(Guid id);
        List<TicketDto> CreateXTicketsForViewSlot(Guid viewSlotId, int xTickets);
        List<TicketDto> GetAllTickets();
        List<TicketDto> GetTicketsByViewSlot(Guid viewSlotId);
        List<TicketDto> FilterTickets(FilterTicketsDto filter);
        TicketDto UpdateTicket(TicketDto ticket);
        void DeleteTicket(Guid id);
    }
}
