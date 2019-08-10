using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Option
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
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Не указана цена")]
        public int Price { get; set; }
        [UIHint("Boolean")]
        [Display(Name = "Добавление")]
        public bool IsPublish { get; set; } 
    }
}
