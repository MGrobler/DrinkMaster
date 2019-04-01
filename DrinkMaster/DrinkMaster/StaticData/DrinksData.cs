using DrinkMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMaster.StaticData
{
    public class DrinksData
    {
        public static List<DrinksModel> DefaultDrinks
        {
            get
            {
                return listOfDrinks;
            }
        }

        private static List<DrinksModel> listOfDrinks = new List<DrinksModel>()
        {
            new DrinksModel() { Id = 100 , DrinkName = "Beer", AlcoholPercentage = 5 , Volume = 6},
            new DrinksModel() { Id = 101, DrinkName = "Gin & Tonic", AlcoholPercentage = 6, Volume = 62 },
            new DrinksModel() { Id = 102, DrinkName = "Dubbel Brannewyn en Coke", AlcoholPercentage = 9, Volume = 11 },
            new DrinksModel() { Id = 103, DrinkName = "Tequila", AlcoholPercentage = 40 , Volume = 21},
        };
    }

}
