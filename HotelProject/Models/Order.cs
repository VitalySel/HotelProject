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
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Surname { get; set; }
        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Не указано количество")]
        public int Count { get; set; }
        [Display(Name = "Количество детей")]
        [Required(ErrorMessage = "Не указано количество")]
        public int Child { get; set; }
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Не указан телефон")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Phone { get; set; }
        [Display(Name = "Почта")]
        [Required(ErrorMessage = "Не указана почта")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Email { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
