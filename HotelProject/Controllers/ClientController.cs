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
    public class ClientController : Controller
    {
        HotelContext db;
        public ClientController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Client_Index()
        {
            return View(db.Clients.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Client_Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await db.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpGet]
        public IActionResult Client_Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Client_Add([Bind("Id,Name,Email,Phone")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                TempData["Add Client"] = "Вы добавили нового клиента";
                db.SaveChanges();

                return RedirectToAction("Client_Index");
            }
            return View(client);
        }
        [HttpGet]
        public IActionResult Client_Edit(int id)
        {
            Client client = db.Clients.Find(id);
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Client_Edit([Bind("Id,Name,Email,Phone")] Client client)
        {
            db.Entry(client).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                //уникальность телефона и почты
                if (db.Clients.Any(x => x.Phone == client.Phone))
                {
                    ModelState.AddModelError("", "Данный телефон уже был указат");
                    return View(client);
                }
                else if (db.Clients.Any(x => x.Email == client.Email))
                {
                    ModelState.AddModelError("", "Данная почта уже была указана");
                    return View(client);
                }


                db.SaveChanges();
                TempData["Edit Client"] = "Вы изменили клиента";
                return RedirectToAction("Client_Index");
            }
            return View(client);
        }
        [HttpGet]
        public IActionResult Client_Delete(int id)
        {
            Client b = db.Clients.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Client_Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Client b = db.Clients.Find(id);

            db.Clients.Remove(b);
            TempData["Delete Client"] = "Вы удалили клиента";
            db.SaveChanges();
            return RedirectToAction("Client_Index");
        }

    }
}