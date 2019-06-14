using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DataTimeOrder { get; set; }
        public string Commentary { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Count { get; set; }
        public int Child { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
        public int ProductId { get; set; }
    }
}
