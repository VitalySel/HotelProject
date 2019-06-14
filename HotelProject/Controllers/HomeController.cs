﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;

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
            return View();
        }
    }
}

