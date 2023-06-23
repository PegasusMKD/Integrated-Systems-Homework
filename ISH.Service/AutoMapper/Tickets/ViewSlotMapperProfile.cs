using AutoMapper;
using ISH.Data.Tickets;
using ISH.Service.Dtos.Tickets;

namespace ISH.Service.AutoMapper.Tickets
{
    public class ViewSlotMapperProfile : Profile
    {
        public ViewSlotMapperProfile()
        {
            CreateMap<ViewSlot, ViewSlotDto>();
            CreateMap<ViewSlotDto, ViewSlot>();
        }
    }
}
