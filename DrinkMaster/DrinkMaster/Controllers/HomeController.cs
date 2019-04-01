using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrinkMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinkMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly DrinkMasterContext _context;

        public HomeController(DrinkMasterContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Create()
        {
            //return View("~/Views/GameInstanceModels/Create.cshtml");
            return RedirectToAction("Create", "GameStateModels");
        }

        public async Task<IActionResult> Join()
        {
            return View(await _context.DrinksModel.ToListAsync());
        }

        public IActionResult Lobby()
        {
            return View();
        }

        public IActionResult Load()
        {
            return View();
        }

        public IActionResult Game()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
