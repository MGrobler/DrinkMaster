using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrinkMaster.Models;

namespace DrinkMaster.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Join()
        {
            return View();
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
