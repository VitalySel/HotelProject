using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;

namespace HotelProject.Controllers
{
    public class PropertieController : Controller
    {
        HotelContext db;
        public PropertieController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Propertie_Index()
        {
            return View(db.Properties.ToList());
        }
        [HttpGet]
        public IActionResult Propertie_Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Propertie_Add(Propertie propertie)
        {
            db.Properties.Add(propertie);
            db.SaveChanges();

            return RedirectToAction("Propertie_Index");
        }
        [HttpGet]
        public IActionResult Propertie_Edit(int id)
        {
            Propertie propertie = db.Properties.Find(id);
            return View(propertie);
        }
        [HttpPost]
        public IActionResult Propertie_Edit(Propertie propertie)
        {
            db.Entry(propertie).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Propertie_Index");
        }
        [HttpGet]
        public IActionResult Propertie_Delete(int id)
        {
            Propertie b = db.Properties.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Propertie_Delete")]
        public IActionResult Propertie_DeleteConfirmed(int id)
        {
            Propertie b = db.Properties.Find(id);

            db.Properties.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Propertie_Index");
        }
    }
}