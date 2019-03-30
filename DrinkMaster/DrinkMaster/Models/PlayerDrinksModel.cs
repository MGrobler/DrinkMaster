using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMaster.Models
{
    public class PlayerDrinksModel
    {
        public int Id { get; set; }
        [Required]
        public int PlayerID { get; set; }
        [Required]
        public int DrinkID { get; set; }
        [Required]
        public int DrinkQuantity { get; set; }
        [Required]
        public double Points { get; set; }
    }
}
