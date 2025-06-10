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
    public class ProductManagementController : Controller
    {
        private readonly AppDBContext  _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductManagementController(IWebHostEnvironment webHostEnvironment, AppDBContext dbContext) { 
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _dbContext.Products.Include(u=> u.Restaurant).ToListAsync();
            return View("~/Views/Admin/ProductManagement/Index.cshtml", products);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create() {
            List<Restaurant> restaurants = await _dbContext.Restaurants.ToListAsync();
            ProductCreationVM productVM = new ProductCreationVM
            {
                Restaurants    = restaurants,
            };
            return PartialView("~/Views/Admin/ProductManagement/Create.cshtml", productVM);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductCreationVM productCreationVM)
        {
            string imagePath = string.Empty;


            if (productCreationVM.ImageUpload != null && productCreationVM.ImageUpload.Length > 0)
            {

                if (productCreationVM.ImageUpload.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageUpload", "El archivo es demasiado grande (máximo 5MB)");
                    return View(productCreationVM);
                }


                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(productCreationVM.ImageUpload.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageUpload", "Formato de imagen no válido. Use: jpg, jpeg, png, gif, webp");
                    return View(productCreationVM);
                }


                var fileName = $"{Guid.NewGuid()}{fileExtension}";


                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "food");


                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productCreationVM.ImageUpload.CopyToAsync(fileStream);
                }


                imagePath = $"/images/food/{fileName}";
                
            }

            Product product = new Product
            {
                Name = productCreationVM.Product.Name,
                Description = productCreationVM.Product.Description,
                Price = productCreationVM.Product.Price,
                ImageUrl = imagePath,
                IdRestaurant = productCreationVM.SelectedRestaurantId,
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "ProductManagement");
        }
    }
}
