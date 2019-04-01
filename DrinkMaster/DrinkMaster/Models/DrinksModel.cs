using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMaster.Models
{
    public class DrinksModel
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Drink Name")]
        [Required]
        public string DrinkName { get; set; }

        [Display(Name = "Alcohol Percentage")]
        [Range(0, 100)]
        [Required]
        public double AlcoholPercentage { get; set; }
    }
}
