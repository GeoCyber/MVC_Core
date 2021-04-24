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
    public class FixedAssetProfileEditorController : Controller
    {
        private readonly MyDbContext _context;

        public FixedAssetProfileEditorController(MyDbContext context)
        {
            _context = context;
        }

        // GET: FixedAssetProfileEditor
        public async Task<IActionResult> Index()
        {
            return View(await _context.FixedAssetProfileEditor.ToListAsync());
        }

        // GET: FixedAssetProfileEditor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetProfileEditor = await _context.FixedAssetProfileEditor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetProfileEditor == null)
            {
                return NotFound();
            }

            return View(fixedAssetProfileEditor);
        }

        // GET: FixedAssetProfileEditor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FixedAssetProfileEditor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssetCode,AssetName,AssetType,AssetCategory,AssetModel,AssetBrand,TransactionDate,Remarks,SupplierID,PurchaseDate,InvoiceNumber,TotalUnit,UnitPrice,CapitalizeAmount,DepreciationStartDate,ResidualAmount,NetBookValue,UtilizeLife,DepreciationpercentagePer,DepreciationEndDate,FixedAssetAccountID,PL_DepreciationAccount,PS_DepreciationAccount,DisposalGainAccount,DisposalLossAccount,writeOfAccount,AssetSubCode,RegistrationNumber,SerialNumber,Department,Location,Asset_UnitPrice,AllocationValue,Status,CreationStatus")] FixedAssetProfileEditor fixedAssetProfileEditor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fixedAssetProfileEditor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fixedAssetProfileEditor);
        }

        // GET: FixedAssetProfileEditor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetProfileEditor = await _context.FixedAssetProfileEditor.FindAsync(id);
            if (fixedAssetProfileEditor == null)
            {
                return NotFound();
            }
            return View(fixedAssetProfileEditor);
        }

        // POST: FixedAssetProfileEditor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssetCode,AssetName,AssetType,AssetCategory,AssetModel,AssetBrand,TransactionDate,Remarks,SupplierID,PurchaseDate,InvoiceNumber,TotalUnit,UnitPrice,CapitalizeAmount,DepreciationStartDate,ResidualAmount,NetBookValue,UtilizeLife,DepreciationpercentagePer,DepreciationEndDate,FixedAssetAccountID,PL_DepreciationAccount,PS_DepreciationAccount,DisposalGainAccount,DisposalLossAccount,writeOfAccount,AssetSubCode,RegistrationNumber,SerialNumber,Department,Location,Asset_UnitPrice,AllocationValue,Status,CreationStatus")] FixedAssetProfileEditor fixedAssetProfileEditor)
        {
            if (id != fixedAssetProfileEditor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fixedAssetProfileEditor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FixedAssetProfileEditorExists(fixedAssetProfileEditor.Id))
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
            return View(fixedAssetProfileEditor);
        }

        // GET: FixedAssetProfileEditor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetProfileEditor = await _context.FixedAssetProfileEditor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetProfileEditor == null)
            {
                return NotFound();
            }

            return View(fixedAssetProfileEditor);
        }

        // POST: FixedAssetProfileEditor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixedAssetProfileEditor = await _context.FixedAssetProfileEditor.FindAsync(id);
            _context.FixedAssetProfileEditor.Remove(fixedAssetProfileEditor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixedAssetProfileEditorExists(int id)
        {
            return _context.FixedAssetProfileEditor.Any(e => e.Id == id);
        }
    }
}
