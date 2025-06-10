using deliveryapp.Data;
using deliveryapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace deliveryapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _context;

        public HomeController(ILogger<HomeController> logger, AppDBContext appDBContext)
        {
            _logger = logger;
            _context  = appDBContext;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public async  Task<IActionResult> Service()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return View(restaurants);
        }

        public async  Task<IActionResult> Menu(int idRestaurant)
        {
            var foods = await _context.Products.Where(p => p.IdRestaurant == idRestaurant).ToListAsync();
            return View(foods);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
