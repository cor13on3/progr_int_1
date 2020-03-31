﻿using Lost.Models;
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
        public async Task<IActionResult> Login([Bind] Uzytkownik obj)
        {
            if (ModelState.IsValid)
            {
                var user = _dal.Daj(obj.Email);
                if (user.Haslo == obj.Haslo)
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
        public IActionResult Register([Bind] Uzytkownik obj)
        {
            if (ModelState.IsValid)
                _dal.Dodaj(obj);
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Login");
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