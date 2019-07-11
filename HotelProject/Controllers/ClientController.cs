using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class ClientController : Controller
    {
        HotelContext db;
        public ClientController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Clients.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Name,Email,Phone")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(client);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Client client = db.Clients.Find(id);
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Name,Email,Phone")]Client client)
        {
            db.Entry(client).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Client b = db.Clients.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Client b = db.Clients.Find(id);

            db.Clients.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}