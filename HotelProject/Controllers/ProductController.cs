using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelProject.Controllers
{
    public class ProductController : Controller
    {
        HotelContext db;
        IHostingEnvironment _appEnvironment;
        public ProductController(HotelContext context, IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            db = context;
        }
        public IActionResult Product_Index(int? categoryId)
        {
            
            var productc = db.Products.Include(p => p.Category);

            IQueryable<Product> products = db.Products.Include(p => p.Category);
            if (categoryId != null && categoryId != 0)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }
            List<Category> category = db.Categories.ToList();
            category.Insert(0, new Category { Title = "All", Id = 0 });

            ProductFilter plv = new ProductFilter
            {
                Products = products.ToList(),
                Categories = new SelectList(category, "Id", "Title"),
            };
            return View(plv);
        }
        [HttpGet]
        public IActionResult Product_Add()
        {
            SelectList categor = new SelectList(db.Categories, "Id", "Title");
            ViewBag.Categories = categor;

            List<Propertie> propertieList = new List<Propertie>();
            propertieList = (from propertie in db.Properties select propertie).ToList();
            ViewBag.Properties = propertieList;


            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Product_Add(Product product, IFormFile uploadedFile,List<Propertie> objPropertie )
        {
            if (uploadedFile != null)
            {
                string path = "/files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                product.Image = path;
            }

            var countChecked = 0;
            var countUnchecked = 0;
            for (int i = 0; i < objPropertie.Count(); i++)
            {
                if (objPropertie[i].checkboxAnswer == true)
                {
                    countChecked = countChecked + 1;
                }
                else
                {
                    countUnchecked = countUnchecked + 1;
                }
            }
          
            db.Products.Add(product);
            db.SaveChanges();

            return RedirectToAction("Product_Index");
        }
        public async Task<IActionResult> ListForCategories(int categoryId)
        {
            List<Product> prods = await db.Products.Where(product => product.CategoryId == categoryId).ToListAsync();
            return View(prods);
        }
        [HttpGet]
        public IActionResult Product_Edit(int? id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                SelectList categor = new SelectList(db.Categories, "Id", "Title", product.CategoryId);
                ViewBag.Categories = categor;
               
                return View(product);
            }
            var propertieList = db.Properties.ToList();
            ViewBag.Prop = propertieList;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Product_Edit(Product product, IFormFile uploadedFile)
        {
            db.Entry(product).State = EntityState.Modified;
            if (uploadedFile != null)
            {
                string path = "/files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                product.Image = path;
            }
            db.SaveChanges();
            return RedirectToAction("Product_Index");
        }

        [HttpGet]
        public IActionResult Product_Delete(int id)
        {
            Product b = db.Products.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Product_Delete")]
        public IActionResult Product_DeleteConfirmed(int id)
        {
            Product b = db.Products.Find(id);

            db.Products.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Product_Index");
        }
    }
}