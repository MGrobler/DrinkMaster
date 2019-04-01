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
            return View(await _context.GameStateModel.Include( c => c.listOfPlayers).ThenInclude(c => c.playerDrinks).ToListAsync());

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
                gameStateModel.listOfPlayers = new List<PlayerModel>();
                var playerModel = new PlayerModel();
                playerModel.PlayerName = "Namer";
                //playerModel.TotalPoints = 100;
                var playerDrink = new PlayerDrinkModel();
                gameStateModel.listOfPlayers.Add(playerModel);
                gameStateModel.listOfPlayers[0].playerDrinks = new List<PlayerDrinkModel>();
                playerDrink.Name = "Drinker";
                playerDrink.AlcoholPercentage = 5;
                playerDrink.DrinkQuantity = 100;
                gameStateModel.listOfPlayers[0].playerDrinks.Add(playerDrink);
                _context.PlayerDrinkModel.Add(playerDrink);
                _context.PlayerModel.Add(playerModel);
                _context.Add(gameStateModel);
                await _context.SaveChangesAsync();
                var modell = await _context.GameStateModel.FindAsync(1);
                if (modell == null)
                {
                    return NotFound();
                }
                System.Diagnostics.Debug.WriteLine(modell.listOfPlayers[0].playerDrinks[0].Name);
                await CalculatePoints(0, 0);
                return RedirectToAction(nameof(Index));
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
    }
}
