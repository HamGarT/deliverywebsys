using Microsoft.AspNetCore.Mvc;

namespace deliveryapp.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}
