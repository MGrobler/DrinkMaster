﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkMaster.Models;

namespace DrinkMaster.Controllers
{
    public class PlayerModelsController : Controller
    {
        private readonly DrinkMasterContext _context;

        public PlayerModelsController(DrinkMasterContext context)
        {
            _context = context;
        }

        // GET: PlayerModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlayerModel.ToListAsync());
        }

        // GET: PlayerModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.PlayerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerModel == null)
            {
                return NotFound();
            }

            return View(playerModel);
        }

        // GET: PlayerModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerName,TotalPoints")] PlayerModel playerModel)
        {
            if (ModelState.IsValid)
            {
                var modell = await _context.GameStateModel.Include(c => c.listOfPlayers).ThenInclude(c => c.playerDrinks).ToListAsync();
                var temp = modell.First();

                temp.listOfPlayers.Add(playerModel);
                temp.listOfPlayers.ForEach(element =>
               {
                   if (element.playerDrinks == null)
                   {
                       element.playerDrinks = new List<PlayerDrinkModel>();
                   } 
               });
                /*var playerDrink1 = new PlayerDrinkModel();
                playerDrink1.Points = 2;
                playerDrink1.Name = "harde hout";
                playerDrink1.DrinkQuantity = 3;
                temp.listOfPlayers[0].playerDrinks.Add(playerDrink1);
                _context.PlayerDrinkModel.Add(playerDrink1);*/
                _context.Add(playerModel);
                _context.GameStateModel.Update(temp);
                await _context.SaveChangesAsync();

                if (temp.listOfPlayers.Count < temp.MaxPlayerCount)
                {
                    return RedirectToAction("Create", "PlayerModels");
                } else
                {
                    return RedirectToAction("Game", "GameStateModels");
                }
            }
            return View(playerModel);
        }

        // GET: PlayerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.PlayerModel.FindAsync(id);
            if (playerModel == null)
            {
                return NotFound();
            }
            return View(playerModel);
        }

        // POST: PlayerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerName,TotalPoints")] PlayerModel playerModel)
        {
            if (id != playerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerModelExists(playerModel.Id))
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
            return View(playerModel);
        }

        // GET: PlayerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.PlayerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerModel == null)
            {
                return NotFound();
            }

            return View(playerModel);
        }

        // POST: PlayerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerModel = await _context.PlayerModel.FindAsync(id);
            _context.PlayerModel.Remove(playerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerModelExists(int id)
        {
            return _context.PlayerModel.Any(e => e.Id == id);
        }
    }
}
