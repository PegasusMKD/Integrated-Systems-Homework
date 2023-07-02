using AutoMapper;
using ISH.Data.Authentication;
using ISH.Data.Tickets;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Repository.Implementations;
using ISH.Service.Dtos.Tickets;
using System.Linq.Expressions;

namespace ISH.Service.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IBaseRepository<Ticket> _baseRepository;
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IBaseRepository<ViewSlot> _baseViewSlotRepository;

        public TicketService(IBaseRepository<Ticket> ticketRepository, IMapper mapper, ITicketRepository ticketRepository1, IBaseRepository<ViewSlot> baseViewSlotRepository)
        {
            _baseRepository = ticketRepository;
            _mapper = mapper;
            _ticketRepository = ticketRepository1;
            _baseViewSlotRepository = baseViewSlotRepository;
        }

        public TicketDto CreateTicket(CreateTicketDto ticket)
        {
            var mTicket = _mapper.Map<Ticket>(ticket);
            mTicket.ViewSlot = _baseViewSlotRepository.GetById(ticket.ViewSlotId)!;
            var eTicket = _baseRepository.Create(mTicket);
            _baseRepository.SaveChanges();
            return _mapper.Map<TicketDto>(eTicket);
        }

        public TicketDto GetById(Guid id) => _mapper.Map<TicketDto>(_baseRepository.GetById(id));

        public List<TicketDto> CreateXTicketsForViewSlot(Guid viewSlotId, int xTickets, int price)
        {
            ViewSlot? eViewSlot = _baseViewSlotRepository.GetById(viewSlotId);
            if (eViewSlot == null)
                throw new Exception("View Slot does not exist!");

            int latestSeatNumber = _ticketRepository.CountTicketsByViewSlot(viewSlotId);
            var tickets = Enumerable.Range(0, xTickets).Select(x => new Ticket()
            {
                SeatNumber = latestSeatNumber + x,
                ViewSlot = eViewSlot,
                Price = price
            }).Select(_baseRepository.Create)
                .Select(_mapper.Map<TicketDto>)
                .ToList();
            _baseRepository.SaveChanges();
            return tickets;
        }

        public List<TicketDto> GetAllTickets() => _ticketRepository.GetAllTicketsWithViewSlot().Select(_mapper.Map<TicketDto>).ToList();

        public List<TicketDto> GetTicketsByViewSlot(Guid viewSlotId) => _ticketRepository
            .GetTicketsByViewSlot(viewSlotId).ConvertAll(_mapper.Map<TicketDto>);

        public List<TicketDto> FilterTickets(FilterTicketsDto filter)
        {
            throw new NotImplementedException(); // TODO: ??
        }

        public TicketDto UpdateTicket(UpdateTicketDto ticket)
        {
            var mTicket = _mapper.Map<Ticket>(ticket);
            mTicket.ViewSlot = _baseViewSlotRepository.GetById(ticket.ViewSlotId)!;
            var eTicket = _baseRepository.Update(mTicket);
            _baseRepository.SaveChanges();
            return _mapper.Map<TicketDto>(eTicket);
        }

        public void DeleteTicket(Guid id)
        {
            _baseRepository.Delete(id);
            _baseRepository.SaveChanges();
        }
    }
}
