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
        public async Task <IActionResult> Admin_Add(Category category, IFormFile uploadedFile)
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
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Admin_Index");
        }

        [HttpGet]
        public IActionResult Admin_Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Admin_Edit(Category category, IFormFile uploadedFile)
        {
            db.Entry(category).State = EntityState.Modified;
            if (uploadedFile != null)
            {
                string path = "/files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                category.Image = path;
            }
            
            db.SaveChanges();
            return RedirectToAction("Admin_Index");
        }
        [HttpGet]
        public IActionResult Admin_Delete(int id)
        {
            Category b = db.Categories.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Admin_Delete")]
        public IActionResult Admin_DeleteConfirmed(int id)
        {
            Category b = db.Categories.Find(id);

            db.Categories.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Admin_Index");
        }
    }
}