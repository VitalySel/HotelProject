using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Не указано описание")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Description { get; set; }
        [Display(Name = "Краткое описание")]
        public string ShortDescription { get; set; }
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Не указана цена")]
        public int Price { get; set; }
        [Display(Name = "Картинка")]
        public string Image { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //[NotMapped]
        //public IEnumerable<Propertie> Properties { get; set; }

        //public Product()
        //{
        //    Properties = new List<Propertie>();
        //}
    }
    
}
