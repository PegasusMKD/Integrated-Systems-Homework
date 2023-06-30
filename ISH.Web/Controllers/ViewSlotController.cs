using ISH.Service;
using ISH.Service.Dtos.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.Controllers
{
    [ApiController]
    [Route("api/view-slot")]
    public class ViewSlotController : ControllerBase
    {
        private readonly ILogger<ViewSlotController> _logger;
        private readonly IViewSlotService _viewSlotService;


        public ViewSlotController(ILogger<ViewSlotController> logger, IViewSlotService viewSlotService)
        {
            _logger = logger;
            _viewSlotService = viewSlotService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) => Ok(_viewSlotService.GetById(id));

        [HttpGet]
        public IActionResult GetAll() => Ok(_viewSlotService.GetAllViewSlots());

        [HttpPost]
        public IActionResult CreateViewSlot([FromBody] ViewSlotDto viewSlot) =>
            Ok(_viewSlotService.CreateViewSlot(viewSlot));

        [HttpPut]
        public IActionResult UpdateViewSlot([FromBody] ViewSlotDto viewSlot) =>
            Ok(_viewSlotService.UpdateViewSlot(viewSlot));

        [HttpDelete("{id}")]
        public void DeleteViewSlot([FromRoute] Guid id) => _viewSlotService.DeleteViewSlot(id);
    }
}
