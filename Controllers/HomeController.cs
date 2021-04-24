using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FixedModules.Models;
using Microsoft.AspNetCore.Authorization;
using FixedModules.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FixedModules.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userList = _context.Companies.ToList();
            ViewData["companies"] = new SelectList(userList, "Id", "Name");
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginSubmit([FromForm] string email, string pass)
        {
            if ((email == "kkvin96@gmail.com") && (pass == "admin@123") ==true  )
            {
                return RedirectToAction("Index", "Home");
            }
            return BadRequest();
        }

        public IActionResult Privacy()
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
