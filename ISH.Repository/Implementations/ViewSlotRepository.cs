using ISH.Data.Tickets;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository.Implementations
{
    public class ViewSlotRepository : IViewSlotRepository
    {
        private readonly ApplicationContext _context;

        public ViewSlotRepository(ApplicationContext context)
        {
            _context = context;
        }

        public ViewSlot GetByIdWithGenre(Guid id) =>
            _context.viewSlots.Include(slot => slot.Genre).SingleOrDefault(slot => slot.Guid == id)!;

        public List<ViewSlot> GetAllWithGenre() =>
            _context.viewSlots.Include(slot => slot.Genre).ToList();
    }
}
