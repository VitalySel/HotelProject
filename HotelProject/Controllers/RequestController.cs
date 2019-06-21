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
        public IActionResult Edit(int id)
        {
            Request request = db.Requests.Find(id);
            return View(request);
        }
        [HttpPost]
        public IActionResult Edit(Request request)
        {
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Request b = db.Requests.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Option_DeleteConfirmed(int id)
        {
            Request b = db.Requests.Find(id);

            db.Requests.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}