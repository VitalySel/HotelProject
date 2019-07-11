using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;


namespace HotelProject.Controllers
{
    public class RequestController : Controller
    {
        HotelContext db;
        public RequestController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Requests.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Phone,Email,Text")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(request);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Request request = db.Requests.Find(id);
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Phone,Email,Text")] Request request)
        {
            db.Entry(request).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Request b = db.Requests.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Option_DeleteConfirmed(int id)
        {
            Request b = db.Requests.Find(id);

            db.Requests.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}