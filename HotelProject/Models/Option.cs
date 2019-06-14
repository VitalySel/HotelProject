using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public int Price { get; set; }
        public bool IsPublish { get; set; } 
    }
}
