using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Category
    {
        
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Не указано описание")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Description { get; set; }
        [Display(Name = "Картинка")]
        public string Image { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
