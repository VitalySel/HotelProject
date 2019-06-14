using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelProject.Models;

namespace HotelProject
{
    public class SampleData
    {
        public static void Initialize(HotelContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
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
