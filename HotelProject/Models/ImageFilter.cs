using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelProject.Models
{
    public class ImageFilter
    {
        public IEnumerable<Image> Images { get; set; }
        public SelectList Products { get; set; }
    }
}
