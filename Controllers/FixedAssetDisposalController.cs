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
    public class FixedAssetDisposalController : Controller
    {
        private readonly MyDbContext _context;

        public FixedAssetDisposalController(MyDbContext context)
        {
            _context = context;
        }

        // GET: FixedAssetDisposal
        public async Task<IActionResult> Index()
        {
            return View(await _context.FixedAssetDisposal.ToListAsync());
        }

        // GET: FixedAssetDisposal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetDisposal = await _context.FixedAssetDisposal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetDisposal == null)
            {
                return NotFound();
            }

            return View(fixedAssetDisposal);
        }

        // GET: FixedAssetDisposal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FixedAssetDisposal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransDate,DocumentCode,Status,Remark,Category,AssetSubCode,SerialNumber,Supplier,PaymentMode,PaymentReference,SellAmount,TaxCode,UnitAmount,AccumatedDepreciation,NetBookValue,GainLoss,DetailRemark,CreatedBy,CreatedDatetime,ModifiedBy,ModifiedDatetime")] FixedAssetDisposal fixedAssetDisposal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fixedAssetDisposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fixedAssetDisposal);
        }

        // GET: FixedAssetDisposal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetDisposal = await _context.FixedAssetDisposal.FindAsync(id);
            if (fixedAssetDisposal == null)
            {
                return NotFound();
            }
            return View(fixedAssetDisposal);
        }

        // POST: FixedAssetDisposal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransDate,DocumentCode,Status,Remark,Category,AssetSubCode,SerialNumber,Supplier,PaymentMode,PaymentReference,SellAmount,TaxCode,UnitAmount,AccumatedDepreciation,NetBookValue,GainLoss,DetailRemark,CreatedBy,CreatedDatetime,ModifiedBy,ModifiedDatetime")] FixedAssetDisposal fixedAssetDisposal)
        {
            if (id != fixedAssetDisposal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fixedAssetDisposal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FixedAssetDisposalExists(fixedAssetDisposal.Id))
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
            return View(fixedAssetDisposal);
        }

        // GET: FixedAssetDisposal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetDisposal = await _context.FixedAssetDisposal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetDisposal == null)
            {
                return NotFound();
            }

            return View(fixedAssetDisposal);
        }

        // POST: FixedAssetDisposal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixedAssetDisposal = await _context.FixedAssetDisposal.FindAsync(id);
            _context.FixedAssetDisposal.Remove(fixedAssetDisposal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixedAssetDisposalExists(int id)
        {
            return _context.FixedAssetDisposal.Any(e => e.Id == id);
        }
    }
}
