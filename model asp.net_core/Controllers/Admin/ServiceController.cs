using Microsoft.AspNetCore.Mvc;

namespace model_asp.net_core.Controllers.Admin
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Admin/Service/Index.cshtml");
        }
    }
}
