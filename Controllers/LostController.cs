using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lost.Models;
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
        public ActionResult Index()
        {
            var osoby = _dal.GetOsoby().ToList();
            return View(osoby);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var osoba = _dal.DajOsobe(id);
            return View(osoba);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind] Zaginiony obj)
        {
            if (ModelState.IsValid)
            {
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