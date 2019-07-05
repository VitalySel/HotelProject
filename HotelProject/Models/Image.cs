using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelProject.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Display(Name = "Путь картинки")]
        public string ImagePath { get; set; }



        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
