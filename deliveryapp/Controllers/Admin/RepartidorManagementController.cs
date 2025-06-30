using deliveryapp.Data;
using deliveryapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace deliveryapp.Controllers.Admin
{
    [Route("Admin/[controller]")]
    public class RepartidorManagementController : Controller
    {
        private readonly AppDBContext _context;
        public RepartidorManagementController(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }
        public IActionResult Index()
        {
            List<Repartidor> repartidores = _context.Repartidor.ToList();
            return View("~/Views/Admin/RepartidorManagement/Index.cshtml", repartidores);
        }
    }
}
