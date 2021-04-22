using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixedAssets.Models;
using FixedModules.Data;

namespace FixedAssets.Controllers
{
    public class FixedAssetWriteOffController : Controller
    {
        private readonly MyDbContext _context;

        public FixedAssetWriteOffController(MyDbContext context)
        {
            _context = context;
        }

        // GET: FixedAssetWriteOffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.FixedAssetWriteOff.ToListAsync());
        }

        // GET: FixedAssetWriteOffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetWriteOff = await _context.FixedAssetWriteOff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetWriteOff == null)
            {
                return NotFound();
            }

            return View(fixedAssetWriteOff);
        }

        // GET: FixedAssetWriteOffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FixedAssetWriteOffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransDate,Remark,Category,AssetSubCode,CreatedBy,CreatedDatetime,ModifiedBy,ModifiedDatetime")] FixedAssetWriteOff fixedAssetWriteOff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fixedAssetWriteOff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fixedAssetWriteOff);
        }

        // GET: FixedAssetWriteOffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetWriteOff = await _context.FixedAssetWriteOff.FindAsync(id);
            if (fixedAssetWriteOff == null)
            {
                return NotFound();
            }
            return View(fixedAssetWriteOff);
        }

        // POST: FixedAssetWriteOffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransDate,Remark,Category,AssetSubCode,CreatedBy,CreatedDatetime,ModifiedBy,ModifiedDatetime")] FixedAssetWriteOff fixedAssetWriteOff)
        {
            if (id != fixedAssetWriteOff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fixedAssetWriteOff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FixedAssetWriteOffExists(fixedAssetWriteOff.Id))
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
            return View(fixedAssetWriteOff);
        }

        // GET: FixedAssetWriteOffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetWriteOff = await _context.FixedAssetWriteOff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetWriteOff == null)
            {
                return NotFound();
            }

            return View(fixedAssetWriteOff);
        }

        // POST: FixedAssetWriteOffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixedAssetWriteOff = await _context.FixedAssetWriteOff.FindAsync(id);
            _context.FixedAssetWriteOff.Remove(fixedAssetWriteOff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixedAssetWriteOffExists(int id)
        {
            return _context.FixedAssetWriteOff.Any(e => e.Id == id);
        }
    }
}
