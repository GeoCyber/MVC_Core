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
    public class FormatController : Controller
    {
        private readonly MyDbContext _context;

        public FormatController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Format
        public async Task<IActionResult> Index()
        {
            var _view = await _context.Format.ToListAsync();

            return View(_view);
        }

        // GET: Format/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var format = await _context.Format
                .FirstOrDefaultAsync(m => m.Id == id);
            if (format == null)
            {
                return NotFound();
            }

            return View(format);
        }

        // GET: Format/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Format/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateFormat,TimeFormat,FAMonthDepreciationFormat,FAWriteOffFormat,FADisposalFormat,FATransferFormat,FAProfileEditorFormat,FARelavueFormat,FAAdjustmentFormat,FAReversalFormat,Createdby,CreatedDatetime,ModifiedBy,ModifiedDatetime")] Format format)
        {
            if (ModelState.IsValid)
            {
                _context.Add(format);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(format);
        }

        // GET: Format/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var format = await _context.Format.FindAsync(id);
            if (format == null)
            {
                return NotFound();
            }
            return View(format);
        }

        // POST: Format/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateFormat,TimeFormat,FAMonthDepreciationFormat,FAWriteOffFormat,FADisposalFormat,FATransferFormat,FAProfileEditorFormat,FARelavueFormat,FAAdjustmentFormat,FAReversalFormat,Createdby,CreatedDatetime,ModifiedBy,ModifiedDatetime")] Format format)
        {
            if (id != format.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(format);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormatExists(format.Id))
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
            return View(format);
        }

        // GET: Format/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var format = await _context.Format
                .FirstOrDefaultAsync(m => m.Id == id);
            if (format == null)
            {
                return NotFound();
            }

            return View(format);
        }

        // POST: Format/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var format = await _context.Format.FindAsync(id);
            _context.Format.Remove(format);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormatExists(int id)
        {
            return _context.Format.Any(e => e.Id == id);
        }
    }
}
