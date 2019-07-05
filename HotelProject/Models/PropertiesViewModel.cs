using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProject.Models
{
    public class PropertiesViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [NotMapped]
        public bool checkboxAnswer { get; set; }
    }
}
