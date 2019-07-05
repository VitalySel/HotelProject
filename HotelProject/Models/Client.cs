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
        public string Email { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
    }
}
