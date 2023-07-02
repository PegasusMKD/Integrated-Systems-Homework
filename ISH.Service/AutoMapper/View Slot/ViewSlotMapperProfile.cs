using AutoMapper;
using ISH.Data.Tickets;
using ISH.Service.Dtos.Tickets;
using ISH.Service.Dtos.View_Slot;

namespace ISH.Service.AutoMapper.View_Slot
{
    public class ViewSlotMapperProfile : Profile
    {
        public ViewSlotMapperProfile()
        {
            CreateMap<ViewSlot, ViewSlotDto>();
            CreateMap<ViewSlotDto, ViewSlot>();
            CreateMap<CreateViewSlotDto, ViewSlot>();
            CreateMap<UpdateViewSlotDto, ViewSlot>();
        }
    }
}
