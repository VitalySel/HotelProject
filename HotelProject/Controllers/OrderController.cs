using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelProject.Controllers
{
    [Authorize(Roles = "admin")]
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
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Commentary,Name,Surname,Count,Child,Phone,Email")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                TempData["Order Add"] = "Вы добавили новый заказ";
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(order);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Commentary,Name,Surname,Count,Child,Phone,Email")] Order order)
        {
            db.Entry(order).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                TempData["Order Edit"] = "Вы изменили заказ";
                return RedirectToAction("Index");
            }
            return View(order);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Order b = db.Orders.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Order b = db.Orders.Find(id);

            db.Orders.Remove(b);
            db.SaveChanges();
            TempData["Order Delete"] = "Вы удалили заказ";
            return RedirectToAction("Index");
        }
    }
}