using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Display(Name = "Отзыв")]
        public string Feedback { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }

        [Display(Name = "Ответ")]
        public string AnswerFeedback { get; set; }
        public bool IsProcessing { get; set; }
    }
}
