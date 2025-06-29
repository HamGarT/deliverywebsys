using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.Services;
using deliveryapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace deliveryapp.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _context;
        private readonly ProductService _productService;

        public HomeController(ILogger<HomeController> logger, AppDBContext appDBContext, ProductService productService)
        {
            _logger = logger;
            _context  = appDBContext;
            _productService =   productService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet("Restaurants")]
        public async  Task<IActionResult> Service()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return View(restaurants);
        }

        [HttpGet("Restaurant/{restaurant_id}")]
        public async  Task<IActionResult> Menu(int restaurant_id)
        {
            var foods = await _context.Products.Where(p => p.IdRestaurant == restaurant_id).ToListAsync();
            var  restaurant = await _context.Restaurants.FindAsync(restaurant_id);
            var viewModel = new RestaurantMenuVM
            {
                Restaurant = restaurant,
                Products = foods,
                Cart = _productService.GetAll()
                
            };
            return View(viewModel);
        }

        [HttpGet("ProductCard")]
        public IActionResult ProductCard(int id)
        {
            var product = _context.Products.Find(id);
            
            return PartialView("~/Views/Home/_ProductCard.cshtml", product);
        }

        [HttpGet("AddCart")]
        public IActionResult AddCart(int id, int quantity, decimal price)
        {
            var product = _context.Products.Find(id);
            var cartItem = new CartItem
            {
               ProductId = product.Id,
               Quantity =  quantity,
               Price = price,
               Product = product,

            };
            _productService.Add(cartItem);
            ViewData["Mensaje"] = "Producto agregado al carrito: " + product.Name;
            var restaurantId = product.IdRestaurant;
            return RedirectToAction("Menu", "Home", new { restaurant_id = restaurantId });
        }

        [HttpGet("AddOrders")]
        public async Task<IActionResult> AddToUserOrders()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
              
                return RedirectToAction("Login", "Access");
            }
           
            Order order = new Order
            {
                UserId = int.Parse(userIdClaim), 
                RestaurantId = 1, 
                OrderDate = DateTime.Now,
                Status = "Pendiente",
                TotalAmount = _productService.GetTotalAmount(),
                DeliveryAddress= "123 Main St"
           };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach (var item in _productService.GetAll())
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
            _productService.ClearCart();
            return RedirectToAction("Service", "Home");
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
