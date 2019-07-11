using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class ProductWithPropertiesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Propertie> PropertyList { get; set; }
    }
}
