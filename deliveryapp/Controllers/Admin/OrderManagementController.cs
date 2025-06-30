using Microsoft.AspNetCore.Mvc;

namespace deliveryapp.Controllers.Admin
{
    public class OrderManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
