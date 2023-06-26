﻿using AutoMapper;
using ISH.Data.Authentication;
using ISH.Data.Tickets;
using ISH.Repository;
using ISH.Repository.Core;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IBaseRepository<Ticket> _baseRepository;
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IBaseRepository<ViewSlot> _baseViewSlotRepository;

        public TicketService(IBaseRepository<Ticket> ticketRepository, Mapper mapper, ITicketRepository ticketRepository1, IBaseRepository<ViewSlot> baseViewSlotRepository)
        {
            _baseRepository = ticketRepository;
            _mapper = mapper;
            _ticketRepository = ticketRepository1;
            _baseViewSlotRepository = baseViewSlotRepository;
        }

        public TicketDto CreateTicket(TicketDto ticket) =>
            _mapper.Map<TicketDto>(_baseRepository.Create(_mapper.Map<Ticket>(ticket)));

        public TicketDto GetById(Guid id) => _mapper.Map<TicketDto>(_baseRepository.GetById(id));

        public List<TicketDto> CreateXTicketsForViewSlot(ViewSlotDto viewSlot, int xTickets)
        {
            ViewSlot? eViewSlot = _baseViewSlotRepository.GetById(viewSlot.Guid);
            if (eViewSlot == null)
                throw new Exception("View Slot does not exist!");

            int latestSeatNumber = _ticketRepository.CountTicketsByViewSlot(viewSlot.Guid);
            var tickets = Enumerable.Range(0, xTickets).Select(x => new Ticket()
            {
                SeatNumber = latestSeatNumber + x,
                ViewSlot = eViewSlot
            }).Select(_baseRepository.Create)
                .Select(_mapper.Map<TicketDto>);
            _baseRepository.SaveChangesAsync();
            return tickets.ToList();
        }

        public List<TicketDto> GetAllTickets() => _baseRepository.GetAll().Select(_mapper.Map<TicketDto>).ToList();

        public List<TicketDto> GetTicketsByViewSlot(ViewSlotDto viewSlot) => _ticketRepository
            .GetTicketsByViewSlot(viewSlot.Guid).ConvertAll(_mapper.Map<TicketDto>);

        public List<TicketDto> FilterTickets(FilterTicketsDto filter)
        {
            throw new NotImplementedException(); // TODO: ??
        }

        public TicketDto UpdateTicket(TicketDto ticket) =>
            _mapper.Map<TicketDto>(_baseRepository.Update(_mapper.Map<Ticket>(ticket)));

        public void DeleteTicket(Guid id) => _baseRepository.Delete(id);
    }
}