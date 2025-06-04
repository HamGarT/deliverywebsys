using Microsoft.AspNetCore.Mvc;

namespace deliveryapp.Controllers.User
{
    [Route("User/[controller]")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/User/Profile/Index.cshtml");
        }
    }
}
