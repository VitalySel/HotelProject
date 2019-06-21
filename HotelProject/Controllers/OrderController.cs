using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;


namespace HotelProject.Controllers
{
    public class OrderController : Controller
    {
        HotelContext db;
        public OrderController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Orders.ToList());
        }       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }
        [HttpPost]
        public IActionResult Edit(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Order b = db.Orders.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Order b = db.Orders.Find(id);

            db.Orders.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}