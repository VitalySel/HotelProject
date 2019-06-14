using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public int Rating { get; set; }

        public string AnswerFeedback { get; set; }
        public bool IsProcessing { get; set; }
    }
}
