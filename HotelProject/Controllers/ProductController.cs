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
using Microsoft.AspNetCore.Authorization;

namespace HotelProject.Controllers
{
    [Authorize(Roles = "admin")]
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
            
            //var productc = db.Products.Include(p => p.Category);

            IQueryable<Product> products = db.Products.Include(p => p.Category);
            if (categoryId != null && categoryId != 0)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }

            List<Category> category = db.Categories.ToList();
            category.Insert(0, new Category { Title = "Все", Id = 0 });


            ProductFilter plv = new ProductFilter
            {
                Products = products.ToList(),
                Categories = new SelectList(category, "Id", "Title"),
            };

            //пагинация
            return View(plv);
        }

        public async Task<IActionResult> Product_Details(int? id, ProductWithPropertiesViewModel productViewModel)
        {

            if (id == null)
            {
                return NotFound();
            }

            //добавить категории и свойства в детали

            Product product = db.Products.Find(id);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpGet]
        public IActionResult Product_Add()
        {
            ProductWithPropertiesViewModel product = new ProductWithPropertiesViewModel();

            List<Propertie> propertieList = new List<Propertie>();
            propertieList = (from propertie in db.Properties select propertie).ToList();

            product.PropertyList = propertieList;
            
            SelectList categor = new SelectList(db.Categories, "Id", "Title");
            ViewBag.Categories = categor;

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Product_Add([Bind("Id,Title,Description,ShortDescription,Price,CategoryId,PropertyList")]  ProductWithPropertiesViewModel productViewModel, IFormFile uploadedFile)
        {
            Product product = new Product
            {
                Id = productViewModel.Id,
                Title = productViewModel.Title,
                Description = productViewModel.Description,
                ShortDescription = productViewModel.ShortDescription,
                Price = productViewModel.Price,
                CategoryId = productViewModel.CategoryId,
            };
            if (ModelState.IsValid)
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

                db.Products.Add(product);
                db.SaveChanges();
                int newId = product.Id;
                
                List<Propertie> properties = productViewModel.PropertyList.Where(prop => prop.isChecked == true).ToList();
                foreach (var item in properties)
                {
                    PropValue propValue = new PropValue
                    {
                        ProductId = product.Id,
                        PropertieId = item.Id,
                    };
                    db.PropValues.Add(propValue);
                }

                db.SaveChanges();
                TempData["Product Add"] = "Вы добавили новый продукт";

                return RedirectToAction("Product_Index");
            }
            return View(product);
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
            var model = new ProductWithPropertiesViewModel()
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Image = product.Image,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };
            List<Propertie> propertieList = new List<Propertie>();
            propertieList = (from propertie in db.Properties select propertie).ToList();

            //тут   
            List<PropValue> propValues = db.PropValues.Where(p => p.ProductId == id).ToList();

            if (propValues.Count != 0)
            {
                foreach (var propertie in propertieList)
                {
                    if (propValues.FirstOrDefault(pV => pV.PropertieId == propertie.Id) != null)
                    {
                        propertie.isChecked = true;
                    }
                }
            }
            model.PropertyList = propertieList;

            if (model != null)
            {
                SelectList categor = new SelectList(db.Categories, "Id", "Title", model.CategoryId);
                ViewBag.Categories = categor;
               
                return View(model);
            }

            //var propertieList = db.Properties.ToList();
            ViewBag.Prop = propertieList;

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Product_Edit([Bind("Id,Title,Description,ShortDescription,Price,CategoryId, Image")] Product product, IFormFile uploadedFile, ProductWithPropertiesViewModel model,int id)
        {
            //ошибка с изображениями
            db.Entry(product).State = EntityState.Modified;

           

            if (ModelState.IsValid)
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

                //свойства
                List<PropValue> oldPropValues = await db.PropValues.Where(p => p.ProductId == id).ToListAsync();

                if(oldPropValues.Count!=0)
                {
                    foreach(var item in model.PropertyList)
                    {
                        if(item.isChecked)
                        {
                            if (oldPropValues.FirstOrDefault(fv => fv.PropertieId == item.Id && fv.ProductId == model.Id) == null)
                                db.Update(new PropValue { PropertieId = item.Id, ProductId = model.Id });
                        }
                        else
                        {
                            if (oldPropValues.FirstOrDefault(fv => fv.PropertieId == item.Id && fv.ProductId == model.Id) != null)
                                db.Remove(oldPropValues.First(fv => fv.PropertieId == item.Id && fv.ProductId == model.Id));
                        }
                    }
                }
                else
                {
                    foreach (var item in model.PropertyList)
                        if (item.isChecked)
                            db.Update(new PropValue { PropertieId = item.Id, ProductId = model.Id });
                }

                await db.SaveChangesAsync();
                TempData["Product Edit"] = "Вы изменили продукт";
                return RedirectToAction("Product_Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Product_Delete(int id)
        {
            Product b = db.Products.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Product_Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Product_DeleteConfirmed(int id)
        {
            Product b = db.Products.Find(id);

            db.Products.Remove(b);
            db.SaveChanges();
            TempData["Product Delete"] = "Вы удалили продукт";
            return RedirectToAction("Product_Index");
        }
    }
}