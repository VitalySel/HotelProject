using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class CategoryController : Controller
    {
        HotelContext db;
        public CategoryController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Admin_Index()
        {
            return View(db.Categories.ToList());
        }
        [HttpGet]
        public IActionResult Admin_Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Admin_Add(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Admin_Index");
        }

        [HttpGet]
        public IActionResult Admin_Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Admin_Edit(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Admin_Index");
        }
        [HttpGet]
        public IActionResult Admin_Delete(int id)
        {
            Category b = db.Categories.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Admin_Delete")]
        public IActionResult Admin_DeleteConfirmed(int id)
        {
            Category b = db.Categories.Find(id);

            db.Categories.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Admin_Index");
        }
    }
}