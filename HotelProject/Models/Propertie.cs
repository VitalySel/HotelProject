using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class Propertie
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Не указано описание")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Description { get; set; }
        [Display(Name = "Картинка")]
        public string Image { get; set; }

        [NotMapped]
        public bool isChecked { get; set; }

    }
}
