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
    public class PropertieController : Controller
    {
        HotelContext db;
        IHostingEnvironment _appEnvironment;
        public PropertieController(HotelContext context, IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            db = context;
        }
        public IActionResult Propertie_Index()
        {
            return View(db.Properties.ToList());
        }

        public async Task<IActionResult> Propertie_Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertie = await db.Properties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertie == null)
            {
                return NotFound();
            }

            return View(propertie);
        }
        [HttpGet]
        public IActionResult Propertie_Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Propertie_Add([Bind("Id,Title,Description")] Propertie propertie, IFormFile uploadedFile)
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
                    propertie.Image = path;
                }

                db.Properties.Add(propertie);
                db.SaveChanges();

                TempData["Propertie Add"] = "Вы добавили новое свойство";

                return RedirectToAction("Propertie_Index");
            }
            return View(propertie);
        }
        [HttpGet]
        public IActionResult Propertie_Edit(int id)
        {
            Propertie propertie = db.Properties.Find(id);
            return View(propertie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Propertie_Edit([Bind("Id,Title,Description")] Propertie propertie, IFormFile uploadedFile)
        {
            db.Entry(propertie).State = EntityState.Modified;
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string path = "/files/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    propertie.Image = path;
                }

                db.SaveChanges();
                TempData["Propertie Edit"] = "Вы изменили свойство";
                return RedirectToAction("Propertie_Index");
            }
            return View(propertie);
        }
        [HttpGet]
        public IActionResult Propertie_Delete(int id)
        {
            Propertie b = db.Properties.Find(id);
            return View(b);
        }

        [HttpPost, ActionName("Propertie_Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Propertie_DeleteConfirmed(int id)
        {
            Propertie b = db.Properties.Find(id);

            db.Properties.Remove(b);
            db.SaveChanges();
            TempData["Propertie Delete"] = "Вы удалили свойство";
            return RedirectToAction("Propertie_Index");
        }
    }
}