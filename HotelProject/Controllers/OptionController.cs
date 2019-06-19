using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;

namespace HotelProject.Controllers
{
    public class OptionController : Controller
    {
        HotelContext db;
        public OptionController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Option_Index()
        {
            return View(db.Options.ToList());
        }
        [HttpGet]
        public IActionResult Option_Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Option_Add(Option option)
        {
            db.Options.Add(option);
            db.SaveChanges();

            return RedirectToAction("Option_Index");
        }
        [HttpGet]
        public IActionResult Option_Edit(int id)
        {
            Option option = db.Options.Find(id);
            return View(option);
        }
        [HttpPost]
        public IActionResult Option_Edit(Option option)
        {
            db.Entry(option).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Option_Index");
        }

        [HttpGet]
        public IActionResult Option_Delete(int id)
        {
            Option b = db.Options.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Option_Delete")]
        public IActionResult Option_DeleteConfirmed(int id)
        {
            Option b = db.Options.Find(id);

            db.Options.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Option_Index");
        }

    }
}