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
            return View(await _context.GameStateModel.ToListAsync());
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
                _context.Add(gameStateModel);
                await _context.SaveChangesAsync();
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
    }
}
