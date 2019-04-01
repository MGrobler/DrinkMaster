using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkMaster.Models;
using DrinkMaster.StaticData;

namespace DrinkMaster.Controllers
{
    public class DrinksModelsController : Controller
    {
        private readonly DrinkMasterContext _context;
        private int _playerId;

        public DrinksModelsController(DrinkMasterContext context)
        {
            _context = context;
            DrinksData.DefaultDrinks.ForEach((element) =>
            {
                _context.DrinksModel.Add(element);
            });
            _context.SaveChangesAsync();
        }

        // GET: DrinksModels
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.DrinksModel.ToListAsync());
        }*/

        // [HttpPost]
        public async Task<IActionResult> Index(int playerId)
        {
            _playerId = playerId;

            return View(await _context.DrinksModel.ToListAsync());
        }

        // GET: DrinksModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinksModel = await _context.DrinksModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinksModel == null)
            {
                return NotFound();
            }

            return View(drinksModel);
        }

        // GET: DrinksModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrinksModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DrinkName,AlcoholPercentage")] DrinksModel drinksModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinksModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drinksModel);
        }

        // GET: DrinksModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinksModel = await _context.DrinksModel.FindAsync(id);
            if (drinksModel == null)
            {
                return NotFound();
            }
            return View(drinksModel);
        }

        // POST: DrinksModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DrinkName,AlcoholPercentage")] DrinksModel drinksModel)
        {
            if (id != drinksModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinksModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinksModelExists(drinksModel.Id))
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
            return View(drinksModel);
        }

        // GET: DrinksModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinksModel = await _context.DrinksModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drinksModel == null)
            {
                return NotFound();
            }

            return View(drinksModel);
        }

        // POST: DrinksModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drinksModel = await _context.DrinksModel.FindAsync(id);
            _context.DrinksModel.Remove(drinksModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinksModelExists(int id)
        {
            return _context.DrinksModel.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Select(int id)
        {
            /*_playerId = 1;

            var drinkModel = await _context.DrinksModel.FindAsync(id);
            var playerDrinkModel = new PlayerDrinkModel();
            playerDrinkModel.AlcoholPercentage = drinkModel.AlcoholPercentage;
            playerDrinkModel.Name = drinkModel.DrinkName;

            var model = await _context.GameStateModel.Include(c => c.listOfPlayers).ThenInclude(c => c.playerDrinks).ToListAsync();
            var gameStateModel = model.First();
            gameStateModel.listOfPlayers[_playerId - 1].playerDrinks.Add(playerDrinkModel);

            _context.PlayerDrinkModel.Add(playerDrinkModel);
            _context.GameStateModel.Update(gameStateModel);*/
            // await _context.SaveChangesAsync();
            return RedirectToAction("DrinkAdded", "GameStateModels");

            var modell = await _context.GameStateModel.Include(c => c.listOfPlayers).ThenInclude(c => c.playerDrinks).ToListAsync();
            var temp = modell.First();
            temp.listOfPlayers.ForEach(element =>
            {
                if (element.playerDrinks == null)
                {
                    element.playerDrinks = new List<PlayerDrinkModel>();
                }
            });
            var playerDrink1 = new PlayerDrinkModel();
            playerDrink1.Points = 2;
            playerDrink1.Name = "harde hout";
            playerDrink1.DrinkQuantity = 3;
            temp.listOfPlayers[0].playerDrinks.Add(playerDrink1);
            await _context.SaveChangesAsync();
            return RedirectToAction("Game", "GameStateModels");
        }
    }
}
