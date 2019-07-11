using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelProject.Controllers
{
    public class CommentController : Controller
    {
        HotelContext db;
        public CommentController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Comments.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Feedback,Name,Surname,Phone,Email")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(comment);  
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Comment comment = db.Comments.Find(id);
            return View(comment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Feedback,Name,Surname,Phone,Email")] Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Comment b = db.Comments.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Comment b = db.Comments.Find(id);

            db.Comments.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}