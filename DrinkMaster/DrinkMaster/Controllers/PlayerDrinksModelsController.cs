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
    public class PlayerDrinksModelsController : Controller
    {
        private readonly DrinkMasterContext _context;

        public PlayerDrinksModelsController(DrinkMasterContext context)
        {
            _context = context;
        }

        // GET: PlayerDrinksModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlayerDrinksModel.ToListAsync());
        }

        // GET: PlayerDrinksModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDrinksModel = await _context.PlayerDrinksModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerDrinksModel == null)
            {
                return NotFound();
            }

            return View(playerDrinksModel);
        }

        // GET: PlayerDrinksModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayerDrinksModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerID,DrinkID,DrinkQuantity,Points")] PlayerDrinksModel playerDrinksModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerDrinksModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playerDrinksModel);
        }

        // GET: PlayerDrinksModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDrinksModel = await _context.PlayerDrinksModel.FindAsync(id);
            if (playerDrinksModel == null)
            {
                return NotFound();
            }
            return View(playerDrinksModel);
        }

        // POST: PlayerDrinksModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerID,DrinkID,DrinkQuantity,Points")] PlayerDrinksModel playerDrinksModel)
        {
            if (id != playerDrinksModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerDrinksModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerDrinksModelExists(playerDrinksModel.Id))
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
            return View(playerDrinksModel);
        }

        // GET: PlayerDrinksModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerDrinksModel = await _context.PlayerDrinksModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerDrinksModel == null)
            {
                return NotFound();
            }

            return View(playerDrinksModel);
        }

        // POST: PlayerDrinksModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerDrinksModel = await _context.PlayerDrinksModel.FindAsync(id);
            _context.PlayerDrinksModel.Remove(playerDrinksModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerDrinksModelExists(int id)
        {
            return _context.PlayerDrinksModel.Any(e => e.Id == id);
        }
    }
}
