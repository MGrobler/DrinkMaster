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
    public class PlayerDrinkModelsController : Controller
    {
        private readonly DrinkMasterContext _context;

        public PlayerDrinkModelsController(DrinkMasterContext context)
        {
            _context = context;
        }

        // GET: PlayerDrinkModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlayerDrinkModel.ToListAsync());
        }

        // GET: PlayerDrinkModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDrinkModel = await _context.PlayerDrinkModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerDrinkModel == null)
            {
                return NotFound();
            }

            return View(playerDrinkModel);
        }

        // GET: PlayerDrinkModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayerDrinkModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerID,DrinkName,AlcoholPercentage,DrinkQuantity,Points")] PlayerDrinkModel playerDrinkModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerDrinkModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playerDrinkModel);
        }

        // GET: PlayerDrinkModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDrinkModel = await _context.PlayerDrinkModel.FindAsync(id);
            if (playerDrinkModel == null)
            {
                return NotFound();
            }
            return View(playerDrinkModel);
        }

        // POST: PlayerDrinkModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerID,DrinkName,AlcoholPercentage,DrinkQuantity,Points")] PlayerDrinkModel playerDrinkModel)
        {
            if (id != playerDrinkModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerDrinkModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerDrinkModelExists(playerDrinkModel.Id))
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
            return View(playerDrinkModel);
        }

        // GET: PlayerDrinkModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDrinkModel = await _context.PlayerDrinkModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerDrinkModel == null)
            {
                return NotFound();
            }

            return View(playerDrinkModel);
        }

        // POST: PlayerDrinkModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerDrinkModel = await _context.PlayerDrinkModel.FindAsync(id);
            _context.PlayerDrinkModel.Remove(playerDrinkModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerDrinkModelExists(int id)
        {
            return _context.PlayerDrinkModel.Any(e => e.Id == id);
        }
    }
}
