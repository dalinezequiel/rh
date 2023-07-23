using Microsoft.AspNetCore.Mvc;

namespace model_asp.net_core.Controllers.Ajaxi
{
    public class AjaxiController : Controller
    {
        [HttpGet]
        [Route("ajaxi/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
