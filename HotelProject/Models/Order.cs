using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name = "Время заказа")]
        public DateTime DataTimeOrder { get; set; }
        [Display(Name = "Комментарий")]
        public string Commentary { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Количество")]
        public int Count { get; set; }
        [Display(Name = "Дети")]
        public int Child { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Почта")]
        public string Email { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
