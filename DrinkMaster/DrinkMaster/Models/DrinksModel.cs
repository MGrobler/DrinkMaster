using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMaster.Models
{
    public class DrinksModel
    {
        public int Id { get; set; }
        [Required]
        public string DrinkName { get; set; }
        [Required]
        public double AlcoholPercentage { get; set; }
    }
}
