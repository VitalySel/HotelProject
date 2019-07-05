using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class ProductFilter
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList Categories { get; set; }
    }
}
