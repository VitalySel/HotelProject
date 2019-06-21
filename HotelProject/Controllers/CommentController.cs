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
        public IActionResult Add(Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Comment comment = db.Comments.Find(id);
            return View(comment);
        }
        [HttpPost]
        public IActionResult Edit(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Comment b = db.Comments.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Comment b = db.Comments.Find(id);

            db.Comments.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}