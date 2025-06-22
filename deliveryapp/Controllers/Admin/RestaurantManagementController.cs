using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.Services;
using deliveryapp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.AccessControl;

namespace deliveryapp.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class RestaurantManagementController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;
        private readonly AppDBContext _context;

        public RestaurantManagementController(
            IWebHostEnvironment webHostEnvironment, 
            AppDBContext context,
            IImageService imageService)
        {
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return View("~/Views/Admin/RestaurantManagement/Index.cshtml", restaurants);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return PartialView("~/Views/Admin/RestaurantManagement/_RestaurantForm.cshtml");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(RestaurantVM restaurantVM )
        {
            string imagePath = string.Empty;

            if (restaurantVM.ImageUpload != null && restaurantVM.ImageUpload.Length > 0)
            {
                imagePath = await _imageService.SaveImageAsync(restaurantVM.ImageUpload, "restaurants");
            }
            else
            {
                imagePath = restaurantVM.ImagePath;
            }


            var restaurant = new Restaurant
            {
                Name = restaurantVM.Name,
                Description = restaurantVM.Description,
                Address = restaurantVM.Address,
                PhoneNumber = restaurantVM.PhoneNumber,
                Email = restaurantVM.Email,
                ImageUrl = imagePath
            };
            
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();

            return RedirectToAction("Index", "RestaurantManagement");
        }

        [HttpGet("Editar")]
        public IActionResult Editar(int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);
            RestaurantVM restaurantVM = new RestaurantVM
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = restaurant.Address,
                PhoneNumber = restaurant.PhoneNumber,
                Email = restaurant.Email,
                ImagePath = restaurant.ImageUrl
            };
            return PartialView("~/Views/Admin/RestaurantManagement/_RestaurantForm.cshtml", restaurantVM);
        }

        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(RestaurantVM restaurantVM)
        {
            string imagePath = string.Empty;


            if (restaurantVM.ImageUpload != null && restaurantVM.ImageUpload.Length > 0)
            {
                imagePath = await _imageService.SaveImageAsync(restaurantVM.ImageUpload, "restaurants"); 
            }else{
                imagePath = restaurantVM.ImagePath; 
            }

            Restaurant restaurant = new Restaurant
            {
                Id = restaurantVM.Id,
                Name = restaurantVM.Name,
                Description = restaurantVM.Description,
                Address = restaurantVM.Address,
                PhoneNumber = restaurantVM.PhoneNumber,
                Email = restaurantVM.Email,
                ImageUrl = imagePath
            };

            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "RestaurantManagement");
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = _context.Restaurants.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(restaurant.ImageUrl))
            {
                _imageService.DeleteImage(restaurant.ImageUrl);
            }
            return RedirectToAction("Index", "RestaurantManagement");
        }

    }
}
