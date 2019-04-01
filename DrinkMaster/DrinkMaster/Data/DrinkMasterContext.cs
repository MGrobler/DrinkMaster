﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DrinkMaster.Models;

namespace DrinkMaster.Models
{
    public class DrinkMasterContext : DbContext
    {
        public DrinkMasterContext (DbContextOptions<DrinkMasterContext> options)
            : base(options)
        {
        }

        public DbSet<GameModel> GameModel { get; set; }

        public DbSet<PlayerModel> PlayerModel { get; set; }

        public DbSet<DrinksModel> DrinksModel { get; set; }

        public DbSet<DrinkMaster.Models.GameStateModel> GameStateModel { get; set; }

    }
}
