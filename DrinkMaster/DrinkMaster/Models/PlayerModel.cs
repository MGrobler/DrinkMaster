﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMaster.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; }
        [Required]

        public List<PlayerDrinkModel> playerDrinks;

        public int TotalPoints { get; set; }
    }
}
