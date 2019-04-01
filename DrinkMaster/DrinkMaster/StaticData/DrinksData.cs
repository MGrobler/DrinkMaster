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
            new DrinksModel() { DrinkName = "Beer", AlcoholPercentage = 0.05 },
            new DrinksModel() { DrinkName = "Gin & Tonic", AlcoholPercentage = 0.06 },
            new DrinksModel() { DrinkName = "Dubbel Brannewyn en Coke", AlcoholPercentage = 0.09 },
            new DrinksModel() { DrinkName = "Tequila", AlcoholPercentage = 0.4 },
        };
    }

}
