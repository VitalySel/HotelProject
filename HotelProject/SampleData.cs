using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Models
{
    public class SampleData
    {
        public static void Initialize(HotelContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange
                    (
                    new Product
                    {
                        Title = "Hotel",
                        Description = "Very Super",
                        ShortDescription = "Super",
                        Price = 600
                    }
                    );
                    
                context.SaveChanges();
            }
        }
    }
}
