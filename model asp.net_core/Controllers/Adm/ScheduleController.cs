using Microsoft.AspNetCore.Mvc;

namespace model_asp.net_core.Controllers.Admin
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Admin/Schedule/Index.cshtml");
        }
    }
}
