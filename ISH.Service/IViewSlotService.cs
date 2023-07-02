using ISH.Service.Dtos.Tickets;
using ISH.Service.Dtos.View_Slot;

namespace ISH.Service
{
    public interface IViewSlotService
    {
        ViewSlotDto CreateViewSlot(CreateViewSlotDto viewSlot);
        ViewSlotDto GetById(Guid id);
        List<ViewSlotDto> GetAllViewSlots();
        ViewSlotDto UpdateViewSlot(UpdateViewSlotDto viewSlot);
        void DeleteViewSlot(Guid id);
    }
}
