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
    public class ImageController : Controller
    {
        HotelContext db;
        IHostingEnvironment _appEnvironment;
        public ImageController(HotelContext context, IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Images.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            SelectList imag = new SelectList(db.Products, "Id", "Title");
            ViewBag.Products = imag;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(IFormFile uploadedFile, int productId)
        {
            if (uploadedFile != null)
            {
                string path = "/files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Image file = new Image { Title = uploadedFile.FileName, ImagePath = path, ProductId = productId };
                db.Images.Add(file);
                db.SaveChanges();
            }
            return RedirectToAction("Index");   
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Image image = db.Images.Find(id);
            return View(image);
        }
        [HttpPost]
        public IActionResult Edit(Image image)
        {
            db.Entry(image).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Image b = db.Images.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Image b = db.Images.Find(id);

            db.Images.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}