using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HotelProject.Controllers
{
    public class HomeController : Controller
    {
        HotelContext db;
        public HomeController(HotelContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

    }
}

