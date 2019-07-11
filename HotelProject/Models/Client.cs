using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Name { get; set; }
        [Display(Name = "Почта")]
        [Required(ErrorMessage = "Не указана почта")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Email { get; set; }
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Не указан телефон")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Phone { get; set; }
    }
}
