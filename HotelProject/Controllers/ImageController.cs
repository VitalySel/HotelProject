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
using Microsoft.AspNetCore.Authorization;

namespace HotelProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class ImageController : Controller
    {
        HotelContext db;
        IHostingEnvironment _appEnvironment;
        public ImageController(HotelContext context, IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            db = context;
        }
        public IActionResult Index(int? productId)
        {
            var image = db.Images.Include(p => p.Product);


            //фильтр по продуктам
            IQueryable<Image> images = db.Images.Include(p => p.Product);
            if (productId != null && productId != 0)
            {
                images = images.Where(p => p.ProductId == productId);
            }

            List<Product> product = db.Products.ToList();
            product.Insert(0, new Product { Title = "Все", Id = 0 });

            ImageFilter plv = new ImageFilter
            {
                Images = images.ToList(),
                Products = new SelectList(product, "Id", "Title"),
            };
            return View(plv);
        }
        [HttpGet]
        public IActionResult Add()
        {
            SelectList imag = new SelectList(db.Products, "Id", "Title");
            ViewBag.Products = imag;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(IFormFile uploadedFile, int productId,Image image)
        {
            if (uploadedFile != null)
            {
                string path = "/files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Image file = new Image { Title = image.Title, ImagePath = path, ProductId = productId };

                //уникальность заголовка
                if (db.Images.Any(x => x.Title == file.Title))
                {
                    ModelState.AddModelError("", "Данный заголовок уже занят");
                    return View(file);
                }
                db.Images.Add(file);
                TempData["Add Image"] = "Вы добавили новую картинку";
                db.SaveChanges();
            }
            return RedirectToAction("Index");   
        }

        public async Task<IActionResult> ListForProducts(int productId)
        {
            List<Image> images = await db.Images.Where(image => image.ProductId == productId).ToListAsync();
            return View(images);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Image image = db.Images.Find(id);
            if (image != null)
            {
                SelectList images = new SelectList(db.Products, "Id", "Title", image.ProductId);
                ViewBag.Products = images;

                return View(image);
            }
           
            return View(image);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(Image image, IFormFile uploadedFile, int productId)
        {
            db.Entry(image).State = EntityState.Modified;

            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string path = "/files/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    //Image file = new Image { Title = image.Title, ImagePath = path, ProductId = productId };
                    image.ImagePath = path;


                    //уникальность заголовка
                    if (db.Images.Any(x => x.Title == image.Title))
                    {
                        ModelState.AddModelError("", "Данный заголовок уже занят");
                        return View(image);
                    }
                }
                db.SaveChanges();
                TempData["Edit Image"] = "Вы изменили картинку";
                return RedirectToAction("Index");
            }
            return View(image);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Image b = db.Images.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Image b = db.Images.Find(id);

            db.Images.Remove(b);
            db.SaveChanges();
            TempData["Delete Image"] = "Вы удалили картинку";
            return RedirectToAction("Index");
        }
    }
}