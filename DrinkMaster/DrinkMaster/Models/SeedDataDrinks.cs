using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMaster.Models
{
    public class SeedDataDrinks
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DrinkMasterContext(
                serviceProvider.GetRequiredService<DbContextOptions<DrinkMasterContext>>()))
            {
                if (context.DrinksModel.Any())
                {
                    return;   // DB has been seeded
                }

                context.DrinksModel.AddRange(
                   new DrinksModel
                   {
                       DrinkName = "Castle Lager",
                       AlcoholPercentage = 5.0
                   }
                );
                context.SaveChanges();
            }
        }
    }
}
