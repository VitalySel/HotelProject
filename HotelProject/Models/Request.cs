using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Request
    {
        public int Id { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Display(Name = "Время")]
        public DateTime DataTimeRequest { get; set; }
        [Display(Name = "Текст")]
        public string Text { get; set; }

    }
}
