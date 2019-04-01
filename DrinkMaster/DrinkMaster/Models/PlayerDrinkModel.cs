﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMaster.Models
{
    public class PlayerDrinkModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double AlcoholPercentage { get; set; }
        [Required]
        public int DrinkQuantity { get; set; }
        [Required]
        public double Points { get; set; }
    }
}
