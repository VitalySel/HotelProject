using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelProject.Controllers
{
    public class ProductController : Controller
    {
        HotelContext db;
        public ProductController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Product_Index()
        {
            return View(db.Products.ToList());
        }
        [HttpGet]
        public IActionResult Product_Add()
        {
            SelectList categor = new SelectList(db.Categories, "Id", "Title");
            ViewBag.Categories = categor;
           

            return View();
        }
        [HttpPost]
        public IActionResult Product_Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Product_Index");
        }
        [HttpGet]
        public IActionResult Product_Edit(int? id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                SelectList categor = new SelectList(db.Categories, "Id", "Title", product.CategoryId);
                ViewBag.Categories = categor;
               
                return View(product);
            }
            var propertieList = db.Properties.ToList();
            ViewBag.Prop = propertieList;
            return View();

        }
        [HttpPost]
        public IActionResult Product_Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Product_Index");
        }

        [HttpGet]
        public IActionResult Product_Delete(int id)
        {
            Product b = db.Products.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Product_Delete")]
        public IActionResult Product_DeleteConfirmed(int id)
        {
            Product b = db.Products.Find(id);

            db.Products.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Products_Index");
        }
    }
}