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

        public async Task<IActionResult> Option_Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var option = await db.Options
                .FirstOrDefaultAsync(m => m.Id == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Option_Add([Bind("Id,Title,Description,Price")] Option option)
        {
            if (ModelState.IsValid)
            {
                //уникальность заголовка и описания
                if (db.Options.Any(x => x.Title == option.Title))
                {
                    ModelState.AddModelError("", "Данный заголовок уже занят");
                    return View(option);
                }
                else if (db.Options.Any(x => x.Description == option.Description))
                {
                    ModelState.AddModelError("", "Данное описание уже занято");
                    return View(option);
                }


                db.Options.Add(option);

                TempData["Option Add"] = "Вы добавили новую опцию";
                db.SaveChanges();
                return RedirectToAction("Option_Index");
            }
            return View(option); 
        }
        [HttpGet]
        public IActionResult Option_Edit(int id)
        {
            Option option = db.Options.Find(id);
            return View(option);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Option_Edit([Bind("Id,Title,Description,Price")] Option option)
        {
            db.Entry(option).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                //уникальность заголовка и описания
                if (db.Options.Any(x => x.Title == option.Title))
                {
                    ModelState.AddModelError("", "Данный заголовок уже занят");
                    return View(option);
                }
                else if (db.Options.Any(x => x.Description == option.Description))
                {
                    ModelState.AddModelError("", "Данное описание уже занято");
                    return View(option);
                }

                db.SaveChanges();
                TempData["Option Edit"] = "Вы изменили опцию";
                return RedirectToAction("Option_Index");
            }
            return View(option);
        }

        [HttpGet]
        public IActionResult Option_Delete(int id)
        {
            Option b = db.Options.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Option_Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Option_DeleteConfirmed(int id)
        {
            Option b = db.Options.Find(id);

            db.Options.Remove(b);
            db.SaveChanges();
            TempData["Option Delete"] = "Вы удалили опцию";
            return RedirectToAction("Option_Index");
        }

    }
}