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
    public class PlayerTrackerModelsController : Controller
    {
        private readonly DrinkMasterContext _context;

        public PlayerTrackerModelsController(DrinkMasterContext context)
        {
            _context = context;
        }

        // GET: PlayerTrackerModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlayerTrackerModel.ToListAsync());
        }

        // GET: PlayerTrackerModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerTrackerModel = await _context.PlayerTrackerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerTrackerModel == null)
            {
                return NotFound();
            }

            return View(playerTrackerModel);
        }

        // GET: PlayerTrackerModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayerTrackerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] PlayerTrackerModel playerTrackerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerTrackerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playerTrackerModel);
        }

        // GET: PlayerTrackerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerTrackerModel = await _context.PlayerTrackerModel.FindAsync(id);
            if (playerTrackerModel == null)
            {
                return NotFound();
            }
            return View(playerTrackerModel);
        }

        // POST: PlayerTrackerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] PlayerTrackerModel playerTrackerModel)
        {
            if (id != playerTrackerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerTrackerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerTrackerModelExists(playerTrackerModel.Id))
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
            return View(playerTrackerModel);
        }

        // GET: PlayerTrackerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerTrackerModel = await _context.PlayerTrackerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerTrackerModel == null)
            {
                return NotFound();
            }

            return View(playerTrackerModel);
        }

        // POST: PlayerTrackerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerTrackerModel = await _context.PlayerTrackerModel.FindAsync(id);
            _context.PlayerTrackerModel.Remove(playerTrackerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerTrackerModelExists(int id)
        {
            return _context.PlayerTrackerModel.Any(e => e.Id == id);
        }
    }
}
