using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DataTimeRequest { get; set; }
        public string Text { get; set; }

    }
}
