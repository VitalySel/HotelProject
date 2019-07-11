using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Не указан логин")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Password { get; set; }

        [Display(Name = "Роль")]
        public int RoleId { get; set; }
    }
}
