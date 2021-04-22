using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FixedModules.Data;
using FixedModules.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FixedModules.Controllers
{
    public class FixedAssetMDepreciationController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IImportService _import;
        public FixedAssetMDepreciationController(MyDbContext context, IWebHostEnvironment env, IImportService import)
        {
            _context = context;
            _env = env;
            _import = import;
        }
        // GET: FixedAssetMDepreciation
        public IActionResult Index() => View(_context.FixedAssetMDepreciation.ToList());

        // GET: FixedAssetMDepreciation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FixedAssetMDepreciation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FixedAssetMDepreciation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FixedAssetMDepreciation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FixedAssetMDepreciation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FixedAssetMDepreciation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FixedAssetMDepreciation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}