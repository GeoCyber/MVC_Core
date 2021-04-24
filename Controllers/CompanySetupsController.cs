using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixedModules.Data;
using FixedModules.Models;

namespace FixedModules.Controllers
{
    public class CompanySetupsController : Controller
    {
        private readonly MyDbContext _context;

        public CompanySetupsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: CompanySetups
        public async Task<IActionResult> Index()
        {
            var setup = await _context.Setup.SingleOrDefaultAsync();

            if(setup != null)
            {
                ViewBag.ul = "-1";
                if (setup.Daily_UL)
                {
                    ViewBag.ul = "d";
                }
                else if (setup.Monthy_UL)
                {
                    ViewBag.ul = "m";
                }
                else if (setup.Reducing_Balance_UL)
                {
                    ViewBag.ul = "rb";
                }
                ViewBag.value = setup.Value;
                ViewBag.id = setup.Id;
                ViewBag.test = setup;
            }

            return View(setup);
        }

        public string GetUtilizeVal()
        {
            string res = string.Empty;
            try
            {
                res = _context.Setup.FirstOrDefault()?.Value;
            }
            catch (Exception ex) {
                res = ex.Message;
            }

            return res;
        }

        // GET: CompanySetups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setup = await _context.Setup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setup == null)
            {
                return NotFound();
            }

            return View(setup);
        }

        // GET: CompanySetups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanySetups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Setup_Id,Monthy_UL,Daily_UL,Reducing_Balance_UL,Value,CompanyId,IsUsed,CreatedBy,CreatedDatetime")] Setup setup, string myselect, string myselect2, string myselect3)
        {
            if (ModelState.IsValid)
            {
                var check = await _context.Setup.SingleOrDefaultAsync();

                if(check != null)
                {
                    if(check.Daily_UL == true)
                    {
                        check.Daily_UL = false;
                    } else if(check.Monthy_UL == true)
                    {
                        check.Monthy_UL = false;
                    } else if(check.Reducing_Balance_UL == true)
                    {
                        check.Reducing_Balance_UL = false;
                    }

                    if (myselect != null)
                    {
                        check.Monthy_UL = true;
                        check.Value = myselect;
                    }
                    else if (myselect2 != null)
                    {
                        check.Daily_UL = true;
                        check.Value = myselect2;
                    }
                    else if (myselect3 != null)
                    {
                        check.Reducing_Balance_UL = true;
                        check.Value = myselect3;
                    }
                    _context.Update(check);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                } else
                {
                    setup.CreatedBy = Guid.NewGuid().ToString();
                    setup.CreatedDatetime = DateTime.Now.Date.ToUniversalTime();
                    if (myselect != null)
                    {
                        setup.Monthy_UL = true;
                        setup.Value = myselect;
                    }
                    else if (myselect2 != null)
                    {
                        setup.Daily_UL = true;
                        setup.Value = myselect2;
                    }
                    else if (myselect3 != null)
                    {
                        setup.Reducing_Balance_UL = true;
                        setup.Value = myselect3;
                    }
                    _context.Add(setup);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(setup);
        }

        // GET: CompanySetups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setup = await _context.Setup.FindAsync(id);
            if (setup == null)
            {
                return NotFound();
            }
            return View(setup);
        }

        // POST: CompanySetups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Setup_Id,Monthy_UL,Daily_UL,Reducing_Balance_UL,Value,CompanyId,IsUsed,CreatedBy,CreatedDatetime")] Setup setup)
        {
            if (id != setup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetupExists(setup.Id))
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
            return View(setup);
        }

        // GET: CompanySetups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setup = await _context.Setup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setup == null)
            {
                return NotFound();
            }

            return View(setup);
        }

        // POST: CompanySetups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var setup = await _context.Setup.FindAsync(id);
            _context.Setup.Remove(setup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetupExists(int id)
        {
            return _context.Setup.Any(e => e.Id == id);
        }
    }
}
