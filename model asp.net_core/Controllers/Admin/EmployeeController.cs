using Microsoft.AspNetCore.Mvc;
using model_asp.net_core.Models;
using System.Diagnostics;

namespace model_asp.net_core.Controllers.Admin
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View("Views/Admin/Employee/Index.cshtml");
        }
        public IActionResult Create()
        {
            return View("Views/Admin/Employee/Create.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
