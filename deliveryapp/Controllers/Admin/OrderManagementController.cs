using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace deliveryapp.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class OrderManagementController : Controller
    {
        private readonly AppDBContext _context;
        public OrderManagementController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
          
            List<Order> orders = _context.Orders
                .Include(o => o.Restaurant)
                .Include(o=> o.User)
                .Include(o => o.Repartidor)
                .ToList();

            OrdersVM orderViewModel = new OrdersVM
            {
                Orders = orders,
                OrderItems = null
            };
            return View("~/Views/Admin/OrderManagement/Index.cshtml", orderViewModel);
        }

        [HttpGet("OrderItem/{id}")]
        public async Task<IActionResult> ShowOrderItem(int id)
        {
            
            List<Order> orders = _context.Orders
               .Include(o => o.Restaurant)
               .Include(o => o.User)
               .Include(o => o.Repartidor)
               .ToList();
            List<OrderItem> orderItems = await _context.OrderItems.Where(o => o.OrderId == id).Include(o => o.Product).ToListAsync();
            OrdersVM orderViewModel = new OrdersVM
            {
                Orders = orders,
                OrderItems = orderItems
            };
            return View("~/Views/Admin/OrderManagement/Index.cshtml", orderViewModel);
        }

        [HttpGet("Editar")]
        public async Task<IActionResult> Editar(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            return PartialView("~/Views/Admin/OrderManagement/_OrderStatusForm.cshtml", order);
        }

        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(int id, string status)
        {
            Order? order = await _context.Orders.FindAsync(id);
            Repartidor? repartidor = await _context.Repartidor.FindAsync(order.RepartidorId);
            order.Status = status;
            repartidor.status = "Disponible"; 
            _context.Orders.Update(order);
            _context.Repartidor.Update(repartidor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "OrderManagement");
        }
    }
}
