using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMaster.Models
{
    public class GameStateModel
    {
        public int Id { get; set; }

        [Display(Name = "Game Name")]
        [Required]
        [MaxLength(15)]
        public string GameName { get; set; }

        [Display(Name = "Maximum Number of Players")]
        [Required]
        [Range(2, 4)]
        public int MaxPlayerCount { get; set; }

        public List<PlayerModel> listOfPlayers { get; set; }

        public string WinningPlayer { get; set; }

    }
}
