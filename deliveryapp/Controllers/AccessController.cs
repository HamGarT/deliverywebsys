using deliveryapp.Data;
using deliveryapp.Models;
using deliveryapp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Security.Claims;

namespace deliveryapp.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDBContext _context;
        public AccessController(AppDBContext appDBContext)
        {
            _context = appDBContext;
        }

        public IActionResult AccessDenied()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var roles = await _context.Roles.FirstOrDefaultAsync(r => r.name == "User");
            Usuario user = new Usuario()
            {
                Names = registerVM.Names,
                LastNames = registerVM.LastNames,
                Email = registerVM.Email,
                Password = registerVM.Password,
                IdRole = roles.Id
            };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login", "Access");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var user = await _context.Users
                        .Include(u => u.Rol)
                        .FirstOrDefaultAsync(u => u.Email == loginVM.Email && u.Password == loginVM.Password); 
            if (user == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado. Email: " + loginVM.Email + " / Password: " + loginVM.Password;
                return View();
            }

            var firstName = user.Names.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            var lastName = user.LastNames.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            string username = $"{firstName} {lastName}";
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol.name)
                
            };

            var indentity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(indentity);
            await HttpContext.SignInAsync("CookieAuth", principal);
            if(user.Rol.name == "Admin")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return RedirectToAction("Index", "Order");
        }

      


    }
}
