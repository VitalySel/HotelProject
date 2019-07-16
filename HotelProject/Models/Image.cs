using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }
        [Display(Name = "Картинка")]
        public string ImagePath { get; set; }


        [Display(Name = "Продукт")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
