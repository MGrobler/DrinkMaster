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
        public async Task<IActionResult> Index()
        {
<<<<<<< HEAD
            return View(await _context.DrinksModel.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(int playerId)
        {
            _playerId = playerId;            
            
=======
>>>>>>> 037d9a967f181b26b4bb23dc5eaf3b47bff71ea7
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
            var drinkModel = await _context.DrinksModel.FindAsync(id);
            var playerDrinkModel = new PlayerDrinkModel();

            // TODO: 

            var playerModel = await _context.PlayerModel.FindAsync(_playerId);
            playerModel.playerDrinks.Add(playerDrinkModel);

            return RedirectToAction("Game", "GameStateModels");
        }
    }
}
