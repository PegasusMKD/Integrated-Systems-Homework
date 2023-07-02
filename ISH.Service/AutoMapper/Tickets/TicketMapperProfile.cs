using AutoMapper;
using ISH.Data.Tickets;
using ISH.Service.Dtos.Authentication;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.AytoMapper.Tickets
{
    public class TicketMapperProfile : Profile
    {
        public TicketMapperProfile()
        {
            CreateMap<Ticket, TicketDto>();
            CreateMap<TicketDto, Ticket>();
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<UpdateTicketDto, Ticket>();
        }

    }
}
