using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;


namespace HotelProject.Controllers
{
    public class UserController : Controller
    {
        HotelContext db;
        public UserController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Login,Password")] User user)
        {
            db.Entry(user).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            User b = db.Users.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            User b = db.Users.Find(id);

            db.Users.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}