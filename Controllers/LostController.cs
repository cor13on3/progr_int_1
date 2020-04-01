using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lost.Data;
using Lost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lost.Controllers
{
    public class LostController : Controller
    {
        private ZaginieniDAL _dal;

        public LostController(LostContext lostContext)
        {
            _dal = new ZaginieniDAL(lostContext);
        }

        [HttpGet]
        public ActionResult Index(string plec = null)
        {
            var osoby = _dal.GetOsoby(plec);
            return View(osoby);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var osoba = _dal.DajOsobe(id);
            var bytes = _dal.DajZdjecie(id);
            string imageData = @"data:image/jpg;base64," + Convert.ToBase64String(bytes);
            ViewBag.ImageData = imageData;
            return View(osoba);
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