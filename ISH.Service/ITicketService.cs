using ISH.Service.Dtos.Tickets;

namespace ISH.Service
{
    public interface ITicketService
    {
        TicketDto CreateTicket(CreateTicketDto ticket);
        TicketDto GetById(Guid id);
        List<TicketDto> CreateXTicketsForViewSlot(Guid viewSlotId, int xTickets, int price);
        List<TicketDto> GetAllTickets();
        List<TicketDto> GetTicketsByViewSlot(Guid viewSlotId);
        List<TicketDto> FilterTickets(FilterTicketsDto filter);
        TicketDto UpdateTicket(UpdateTicketDto ticket);
        void DeleteTicket(Guid id);
    }
}
