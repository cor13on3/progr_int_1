using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lost.Controllers
{
    public class LostController : Controller
    {
        private ZaginieniDAL _dal;

        public LostController()
        {
            _dal = new ZaginieniDAL();
        }

        [HttpGet]
        public ActionResult Index(string plec = null)
        {
            var osoby = _dal.GetOsoby(plec).ToList();
            return View(osoby);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var osoba = _dal.DajOsobe(id);
            return View(osoba);
        }

        [HttpGet]
        public HttpContext Image(int id)
        {
            var bytesFromDatabase = _dal.DajZdjecie(id);
            var context = HttpContext;
            context.Response.ContentType = "image/jpeg";
            context.Response.Body.Write(bytesFromDatabase, 0, bytesFromDatabase.Length);
            return context;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind] Zaginiony obj, List<IFormFile> Zdjecie)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Zdjecie)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            obj.Zdjecie = stream.ToArray();
                        }
                    }
                }
                _dal.DodajOsobe(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public ActionResult Edit(int id)
        {
            var osoba = _dal.DajOsobe(id);
            return View(osoba);
        }

        [HttpPost]
        public ActionResult Edit([Bind] Zaginiony osoba)
        {
            if (ModelState.IsValid)
            {
                _dal.EdytujOsobe(osoba);
                return RedirectToAction("Index");
            }
            return View(osoba);
        }

        public ActionResult Delete(int id)
        {
            _dal.UsunOsobe(id);
            return RedirectToAction("Index");
        }
    }
}