using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace deliveryapp.Controllers.User
{
    [Route("User/[controller]")]
    public class ProfileController : Controller
    {
        private readonly AppDBContext _context;
        public ProfileController(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public IActionResult Index()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Access");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest("Invalid User ID format");
            }

            List<Order> orders = _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Restaurant)
                .ToList();

            List<OrderItem> orderItems = _context.OrderItems
                .Where(oi => orders.Select(o => o.Id).Contains(oi.OrderId))
                .ToList();

            OrdersVM orderViewModel = new OrdersVM
            {
                Orders = orders,
                OrderItems = orderItems
            };

            return View("~/Views/User/Orders.cshtml", orderViewModel);
        }
    }

    
}
