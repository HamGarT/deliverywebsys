using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace deliveryapp.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class RestaurantManagementController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDBContext _context;

        public RestaurantManagementController(IWebHostEnvironment webHostEnvironment, AppDBContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return View("~/Views/Admin/RestaurantManagement/Index.cshtml", restaurants);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return PartialView("~/Views/Admin/RestaurantManagement/Create.cshtml");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(RestaurantVM restaurantVM )
        {
            string imagePath = string.Empty;


            if (restaurantVM.ImageUpload != null && restaurantVM.ImageUpload.Length > 0)
            {
                
                if (restaurantVM.ImageUpload.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageUpload", "El archivo es demasiado grande (máximo 5MB)");
                    return View(restaurantVM);
                }

               
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(restaurantVM.ImageUpload.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageUpload", "Formato de imagen no válido. Use: jpg, jpeg, png, gif, webp");
                    return View(restaurantVM);
                }

               
                var fileName = $"{Guid.NewGuid()}{fileExtension}";

                
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "restaurants");

               
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);

                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await restaurantVM.ImageUpload.CopyToAsync(fileStream);
                }

               
                imagePath = $"/images/restaurants/{fileName}";
                restaurantVM.ImagePath = imagePath;
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

    }
}
