using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HotelProject.Controllers
{
    [Authorize(Roles = "admin")]
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await db.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                //уникальность логина
                if (db.Users.Any(x => x.Email == user.Email))
                {
                    ModelState.AddModelError("", "Данный логин уже занят");
                    return View(user);
                }

                db.Users.Add(user);
                db.SaveChanges();

                TempData["User Add"] = "Вы добавили пользователя";
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
        public IActionResult Edit([Bind("Id,Email,Password")] User user)
        {
            db.Entry(user).State = EntityState.Modified;
            if (ModelState.IsValid)
            {


                //уникальность логина
                if (db.Users.Any(x => x.Email == user.Email))
                {
                    ModelState.AddModelError("", "Данный логин уже занят");
                    return View(user);
                }

                db.SaveChanges();
                TempData["User Edit"] = "Вы изменили пользователя";
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
            TempData["User Delete"] = "Вы удалили пользователя";
            return RedirectToAction("Index");
        }

    }
}