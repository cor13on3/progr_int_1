using Lost.Data;
using Lost.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lost.Controllers
{
    public class AccountController : Controller
    {
        private UzytkownikDAL _dal;

        public AccountController(LostContext lostContext)
        {
            _dal = new UzytkownikDAL(lostContext);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind] Uzytkownik obj)
        {
            if (ModelState.IsValid)
            {
                var user = _dal.Daj(obj.Email);
                if (user.Haslo == obj.Haslo)
                {
                    await Zaloguj(user);
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
        public async Task<IActionResult> Register([Bind] Uzytkownik obj)
        {
            if (ModelState.IsValid)
            {
                _dal.Dodaj(obj);
                var user = _dal.Daj(obj.Email);
                await Zaloguj(user);
            }
            return RedirectToAction("Index", controllerName: "Home");
        }

        private async Task Zaloguj(Uzytkownik user)
        {
            var claims = new List<Claim>
                    {
                        new Claim("Email", user.Email),
                        new Claim("Role", user.Rola.ToString()),
                        new Claim("Banned", user.Zbanowany.ToString())
                    };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", controllerName: "Home");
        }

        [HttpGet]
        public IActionResult List()
        {
            var res = _dal.Przegladaj();
            return View(res);
        }

        [HttpGet]
        public IActionResult Ban(string email, bool value = true)
        {
            _dal.Banuj(email, value);
            return RedirectToAction("List");
        }
    }
}