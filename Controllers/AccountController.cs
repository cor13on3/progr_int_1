using Lost.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lost.Controllers
{
    public class AccountController : Controller
    {
        private UzytkownikDAL _dal;

        public AccountController()
        {
            _dal = new UzytkownikDAL();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login([Bind] Uzytkownik obj)
        {
            if (ModelState.IsValid)
            {
                var user = _dal.Daj(obj.Email);
                if (user.Haslo == obj.Haslo)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("user", obj.Email),
                    };
                    await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "user")));
                    return RedirectToAction("Index", "Lost");
                }
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register([Bind] Uzytkownik obj)
        {
            if (ModelState.IsValid)
            {
                _dal.Dodaj(obj);
                var claims = new List<Claim>
                {
                    new Claim("user", obj.Email),
                };
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "user")));
                return RedirectToAction("Index", "Lost");
            }
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("Login");
        }
    }
}