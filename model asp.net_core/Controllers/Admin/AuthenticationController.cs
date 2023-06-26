using Microsoft.AspNetCore.Mvc;

namespace model_asp.net_core.Controllers.Admin
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Admin/Authentication/Index.cshtml");
        }
    }
}
