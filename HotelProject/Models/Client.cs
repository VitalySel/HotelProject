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
        public string Name { get; set; }
        [Display(Name = "Почта")]
        [Required(ErrorMessage = "Не указана почта")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}
