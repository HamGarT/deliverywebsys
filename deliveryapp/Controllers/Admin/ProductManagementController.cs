using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.Services;
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
        private readonly IImageService _imageService;

        public ProductManagementController(
            AppDBContext dbContext,
            IImageService imageService) 
        { 
            _dbContext = dbContext;
            _imageService = imageService;
            
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
                imagePath = await _imageService.SaveImageAsync(productCreationVM.ImageUpload, "food");
            }
            else
            {
                imagePath = productCreationVM.Product.ImageUrl;
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


        [HttpGet("Editar")]
        public async Task<IActionResult> Editar(int id)
        {
            Product producto = await _dbContext.Products.Include(u => u.Restaurant).FirstOrDefaultAsync(u => u.Id == id);
            List<Restaurant> restaurantes = await _dbContext.Restaurants.ToListAsync();
            ProductCreationVM productCreationVM = new ProductCreationVM
            {
                Product = producto,
                Restaurants = restaurantes
            };
            return PartialView("~/Views/Admin/ProductManagement/Editar.cshtml", productCreationVM);
        }

        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(ProductCreationVM productCreationVM)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(u => u.Id == productCreationVM.Product.Id);
            if (product == null)
            {
                return NotFound();
            }
            string imagePath = string.Empty;
            if (productCreationVM.ImageUpload != null && productCreationVM.ImageUpload.Length > 0)
            {
                imagePath = await _imageService.SaveImageAsync(productCreationVM.ImageUpload, "food");
            }
            else
            {
                imagePath = product.ImageUrl;
            }
            product.Id = productCreationVM.Product.Id;
            product.Name = productCreationVM.Product.Name;
            product.Description = productCreationVM.Product.Description;
            product.Price = productCreationVM.Product.Price;
            product.ImageUrl = imagePath;
            product.IdRestaurant = productCreationVM.SelectedRestaurantId;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            
            return RedirectToAction("Index", "ProductManagement");
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                _imageService.DeleteImage(product.ImageUrl);
            }
            return RedirectToAction("Index", "ProductManagement");

        }    
    }
}
