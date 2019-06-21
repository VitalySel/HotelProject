using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class ImageController : Controller
    {
        HotelContext db;
        public ImageController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Images.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Image image)
        {
            db.Images.Add(image);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }
        [HttpPost]
        public IActionResult Edit(Image image)
        {
            db.Entry(image).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Image b = db.Images.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Image b = db.Images.Find(id);

            db.Images.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}