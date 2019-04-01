using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkMaster.Models;

namespace DrinkMaster.Controllers
{
    public class GameStateModelsController : Controller
    {
        private readonly DrinkMasterContext _context;

        public GameStateModelsController(DrinkMasterContext context)
        {
            _context = context;
        }

        // GET: GameStateModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameStateModel.Include(c => c.listOfPlayers).ThenInclude(c => c.playerDrinks).ToListAsync());
        }

        public async Task<IActionResult> Game()
        {
            return View(await _context.GameStateModel.Include(c => c.listOfPlayers).ThenInclude(c => c.playerDrinks).ToListAsync());
        }

        public async Task<IActionResult> EndGame()
        {
            // TODO: Clear _context to remove player models
            return RedirectToAction("Create", "GameStateModels");
        }

        // GET: GameStateModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameStateModel = await _context.GameStateModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameStateModel == null)
            {
                return NotFound();
            }

            return View(gameStateModel);
        }

        // GET: GameStateModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameStateModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameName,MaxPlayerCount")] GameStateModel gameStateModel)
        {
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Hello");
                gameStateModel.listOfPlayers = setTestData(gameStateModel);
                
                _context.Add(gameStateModel);
                await _context.SaveChangesAsync();
                var modell = await _context.GameStateModel.FindAsync(1);
                if (modell == null)
                {
                    return NotFound();
                }

                return RedirectToAction("Create", "PlayerModels");
            }
            return View(gameStateModel);
        }

        // GET: GameStateModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameStateModel = await _context.GameStateModel.FindAsync(id);
            if (gameStateModel == null)
            {
                return NotFound();
            }
            return View(gameStateModel);
        }

        // POST: GameStateModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameName,MaxPlayerCount")] GameStateModel gameStateModel)
        {
            if (id != gameStateModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameStateModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameStateModelExists(gameStateModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gameStateModel);
        }

        // GET: GameStateModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameStateModel = await _context.GameStateModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameStateModel == null)
            {
                return NotFound();
            }

            return View(gameStateModel);
        }

        // POST: GameStateModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameStateModel = await _context.GameStateModel.FindAsync(id);
            _context.GameStateModel.Remove(gameStateModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameStateModelExists(int id)
        {
            return _context.GameStateModel.Any(e => e.Id == id);
        }

        // POST: GameStateModels/CalculatePoints/5
        [HttpPost]
        public async Task<IActionResult> CalculatePoints(int id, int drinkId)
        {
            var model = await _context.GameStateModel.Include(c => c.listOfPlayers).ThenInclude(c => c.playerDrinks).ToListAsync();
            var gameStateModel = model.First();
            if (gameStateModel == null)
            {
                return NotFound();
            }

            var drink = gameStateModel.listOfPlayers[id].playerDrinks[drinkId];
            var points = drink.AlcoholPercentage / 100.0 * drink.DrinkQuantity;

            drink.Points = points;
            gameStateModel.listOfPlayers[id].TotalPoints += points;

            var winningPts = 0.0;
            foreach (var player in gameStateModel.listOfPlayers)
            {
                if (player.TotalPoints >= winningPts)
                {
                    gameStateModel.WinningPlayer = player.PlayerName;
                    winningPts = player.TotalPoints;
                }
            }
            try
            {
                _context.Update(gameStateModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameStateModelExists(gameStateModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View(gameStateModel);
        }

        private List<PlayerModel> setTestData(GameStateModel gameStateModel)
        {
            gameStateModel.listOfPlayers = new List<PlayerModel>();
            var playerModel = new PlayerModel();
            playerModel.PlayerName = "Namer";
            playerModel.TotalPoints = 100;
            playerModel.playerDrinks = new List<PlayerDrinkModel>();
            var playerDrink = new PlayerDrinkModel();
            playerDrink.Points = 2;
            playerDrink.Name = "harde hout";
            playerDrink.DrinkQuantity = 3;
            playerModel.playerDrinks.Add(playerDrink);


            var playerModel1 = new PlayerModel();
            playerModel1.PlayerName = "Namer323";
            playerModel1.TotalPoints = 109;
            playerModel1.playerDrinks = new List<PlayerDrinkModel>();
            var playerDrink1 = new PlayerDrinkModel();
            playerDrink1.Points = 2;
            playerDrink1.Name = "harde hout";
            playerDrink1.DrinkQuantity = 3;
            playerModel1.playerDrinks.Add(playerDrink1);

            var playerModel2 = new PlayerModel();
            playerModel2.PlayerName = "Person";
            playerModel2.TotalPoints = 1002;
            playerModel2.playerDrinks = new List<PlayerDrinkModel>();
            var playerDrink2 = new PlayerDrinkModel();
            playerDrink2.Points = 2;
            playerDrink2.Name = "harde hout";
            playerDrink2.DrinkQuantity = 3;
            playerModel2.playerDrinks.Add(playerDrink2);

            var playerModel3 = new PlayerModel();
            playerModel3.PlayerName = "Pi";
            playerModel3.TotalPoints = 10011;
            playerModel3.playerDrinks = new List<PlayerDrinkModel>();
            var playerDrink3 = new PlayerDrinkModel();
            playerDrink3.Points = 2;
            playerDrink3.Name = "harde hout";
            playerDrink3.DrinkQuantity = 3;
            playerModel3.playerDrinks.Add(playerDrink3);

            gameStateModel.listOfPlayers.Add(playerModel);
            _context.PlayerModel.Add(playerModel);
            gameStateModel.listOfPlayers.Add(playerModel1);
            _context.PlayerModel.Add(playerModel1);
            gameStateModel.listOfPlayers.Add(playerModel2);
            _context.PlayerModel.Add(playerModel2);
            gameStateModel.listOfPlayers.Add(playerModel3);
            _context.PlayerModel.Add(playerModel3);

            return gameStateModel.listOfPlayers;
        }

        public async Task<IActionResult> AddDrink() // (int id?)
        {
            return RedirectToAction("Index", "DrinksModels");
        }
    }


    
}
