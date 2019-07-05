using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Краткое описание")]
        public string ShortDescription { get; set; }
        [Display(Name = "Цена")]
        public int Price { get; set; }
        [Display(Name = "Картинка")]
        public string Image { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //public int PropertieId { get; set; }
        //public Propertie Propertie { get; set; }
    }
}
