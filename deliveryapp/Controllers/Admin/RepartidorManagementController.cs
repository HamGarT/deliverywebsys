using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.Services;
using deliveryapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace deliveryapp.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class RepartidorManagementController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IImageService _imageService;
        public RepartidorManagementController(AppDBContext appDBContext, IImageService imageService)
        {
            _context = appDBContext;
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            List<Repartidor> repartidores = _context.Repartidor.ToList();
            return View("~/Views/Admin/RepartidorManagement/Index.cshtml", repartidores);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return PartialView("~/Views/Admin/RepartidorManagement/_RepartidorForm.cshtml");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(RepartidorVM repartidorVM)
        {
            string imagePath = string.Empty;


            if (repartidorVM.ImageUpload != null && repartidorVM.ImageUpload.Length > 0)
            {
                imagePath = await _imageService.SaveImageAsync(repartidorVM.ImageUpload, "repartidores");
            }
            else
            {
                imagePath = repartidorVM.ImagePath;
            }
            Repartidor repartidor = new Repartidor
            {
                Nombres = repartidorVM.Nombres,
                Apellidos = repartidorVM.Apellidos,
                Dni = repartidorVM.Dni,
                status = repartidorVM.status,
                Telefono = repartidorVM.Telefono,
                ImageUrl = imagePath
            };

            await _context.AddAsync(repartidor);
            await _context.SaveChangesAsync();
            List<Repartidor> repartidores = await _context.Repartidor.ToListAsync();
            return RedirectToAction("Index", "RepartidorManagement");
        }


        [HttpGet("Editar")]
        public async Task<IActionResult> Editar(int id)
        {
            Repartidor repartidor = await _context.Repartidor.FirstOrDefaultAsync(u => u.Id == id);
            
            RepartidorVM repartidorVM = new RepartidorVM
            {
                Id = repartidor.Id,
                Nombres = repartidor.Nombres,
                Apellidos = repartidor.Apellidos,
                Dni=repartidor.Dni,
                status = repartidor.status,
                Telefono =repartidor.Telefono,
                ImagePath = repartidor.ImageUrl
            };
            return PartialView("~/Views/Admin/RepartidorManagement/_RepartidorForm.cshtml", repartidorVM);
        }

        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(RepartidorVM repartidorVM)
        {
            string imagePath = string.Empty;


            if (repartidorVM.ImageUpload != null && repartidorVM.ImageUpload.Length > 0)
            {
                imagePath = await _imageService.SaveImageAsync(repartidorVM.ImageUpload, "repartidores");
            }
            else
            {
                imagePath = repartidorVM.ImagePath;
            }

            Repartidor repartidor = new Repartidor
            {
                Id = repartidorVM.Id,
                Nombres = repartidorVM.Nombres,
                Apellidos = repartidorVM.Apellidos,
                Dni = repartidorVM.Dni,
                status = repartidorVM.status,
                Telefono = repartidorVM.Telefono,
                ImageUrl = imagePath
            };

            _context.Repartidor.Update(repartidor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "RepartidorManagement");
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Repartidor repartidor = await _context.Repartidor.FirstOrDefaultAsync(u => u.Id == id);
            if (repartidor == null)
            {
                return NotFound();
            }
            _context.Repartidor.Remove(repartidor);
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(repartidor.ImageUrl))
            {
                _imageService.DeleteImage(repartidor.ImageUrl);
            }
            return RedirectToAction("Index", "RepartidorManagement");

        }
    }
}
