using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelProject.Controllers
{
    public class CategoryController : Controller
    {
        HotelContext db;
        IHostingEnvironment _appEnvironment;
        public CategoryController(HotelContext context, IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            db = context;
        }
        public IActionResult Admin_Index()
        {
            return View(db.Categories.ToList());
        }
        [HttpGet]
        public IActionResult Admin_Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Admin_Add([Bind("Id,Title,Description,Image")] Category category, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string path = "/files/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    category.Image = path;
                }

                //уникальность заголовка и описания
                if (db.Categories.Any(x=>x.Title ==category.Title))
                {
                    ModelState.AddModelError("", "Данный заголовок уже занят");
                    return View(category);
                }
                else if(db.Categories.Any(x => x.Description == category.Description))
                {
                    ModelState.AddModelError("", "Данное описание уже занято");
                    return View(category);
                }

                db.Categories.Add(category);
                db.SaveChanges();
                TempData["SM"] = "Вы добавили новую категорию";
                return RedirectToAction("Admin_Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Admin_Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Admin_Edit([Bind("Id,Title,Description,Image")] Category category, IFormFile uploadedFile)
        {
            db.Entry(category).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string path = "/files/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    category.Image = path;
                }
                //уникальность заголовка и описания
                if (db.Categories.Any(x => x.Title == category.Title))
                {
                    ModelState.AddModelError("", "Данный заголовок уже занят");
                    return View(category);
                }
                else if (db.Categories.Any(x => x.Description == category.Description))
                {
                    ModelState.AddModelError("", "Данное описание уже занято");
                    return View(category);
                }

                db.SaveChanges();

                TempData["Edit Message"] = "Вы изменили категорию";
                return RedirectToAction("Admin_Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Admin_Delete(int id)
        {
            Category b = db.Categories.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Admin_Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Admin_DeleteConfirmed(int id)
        {
            Category b = db.Categories.Find(id);

            db.Categories.Remove(b);
            db.SaveChanges();
            TempData["SM"] = "Вы удалили категорию";
            return RedirectToAction("Admin_Index");
        }
    }
}