using Microsoft.AspNetCore.Mvc;

namespace Integrated_Systems_Homework.ViewControllers
{
    [Controller]
    [Route("views/import")]
    public class ImportController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
