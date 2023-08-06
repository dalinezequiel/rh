using Microsoft.AspNetCore.Mvc;

namespace model_asp.net_core.Controllers.Admin
{
    public class DashboardController : Controller
    {
        [Route("")]
        [Route("dashboard")]
        [Route("dashboard/index")]
        public IActionResult Index()
        {
            return View("Views/Adm/Dashboard/Index.cshtml");
        }
    }
}
