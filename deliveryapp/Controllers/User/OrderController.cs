using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace deliveryapp.Controllers.User
{
    [Route("User/[controller]")]
    public class OrderController : Controller
    {
        private readonly AppDBContext _context;
        public OrderController(AppDBContext appDBContext)
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

            OrdersVM orderViewModel = new OrdersVM
            {
                Orders = orders,
                OrderItems = null
            };

            return View("~/Views/User/Orders.cshtml", orderViewModel);
        }

        [HttpGet("OrderItem/{id}")]
        public async Task<IActionResult> ShowOrderItem(int id)
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
            List<OrderItem> orderItems = await _context.OrderItems.Where(o=> o.OrderId == id).Include(o=>o.Product).ToListAsync();
            OrdersVM orderViewModel = new OrdersVM
             {
                 Orders = orders,
                 OrderItems = orderItems
             };
            return View("~/Views/User/Orders.cshtml", orderViewModel);
        }
    }

    
}
