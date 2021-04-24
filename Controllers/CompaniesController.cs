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
    public class CompaniesController : Controller
    {
        private readonly MyDbContext _context;

        public CompaniesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companies == null)
            {
                return NotFound();
            }

            return View(companies);
        }

        public List<Companies> Search(string code, string name)
        {
            List<Companies> res = null;
            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.Companies.ToList().OrderBy(m => m.Id).ToList();

                if (!string.IsNullOrEmpty(code))
                {
                    code = code.Trim();
                    res = res.Where(m => m.Code.ToLower().Contains(code)).ToList();
                }

                if (!string.IsNullOrEmpty(name))
                {
                    name = name.Trim();
                    res = res.Where(m => m.Name.ToLower().Contains(name)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return res;
        }

        public IActionResult getState(string name)
        {
           var coun = _context.tblCountry.Where(x => x.CountryName == name).FirstOrDefault();

            var stateList = _context.tblState.Where(x => x.CountryId == coun.CountryId).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateName", "StateName");

            return View();
        }

        public JsonResult getStates(string name)
        {
            ViewData.Clear();
            var coun = _context.tblCountry.Where(x => x.CountryName == name).FirstOrDefault();

            var stateList = _context.tblState.Where(x => x.CountryId == coun.CountryId).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateName", "StateName");

            return Json(stateList);
        }


        // GET: Companies/Create
        public IActionResult Create()
        {
            var countryList =  _context.tblCountry.OrderBy(x => x.CountryId).ToList();
            ViewData["countryList"] = new SelectList(countryList, "CountryName", "CountryName");

            var stateList = _context.tblState.Where(x => x.CountryId == 1).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateName", "StateName");

            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Group,Code,Name,TimeZone,Status,RegistrationNumber,GST,TinNo,SST,Address,City,PostCode,Country,State,Email,PhoneNumber,Fax,CreatedBy,CreatedDatetime,ModifiedBy,ModifeidDatetime")] Companies companies)
        {
            //if (ModelState.IsValid)
            //{
           
            companies.CreatedBy = "Kelvin";
            companies.CreatedDatetime = DateTime.UtcNow;
                _context.Add(companies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(companies);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Companies.FindAsync(id);

            var countryList = _context.tblCountry.OrderBy(x => x.CountryName == companies.Country).ToList();
            ViewData["countryList"] = new SelectList(countryList, "CountryName", "CountryName");

            var stateList = _context.tblState.Where(x => x.StateName == companies.State).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateName", "StateName");
            
            if (companies == null)
            {
                return NotFound();
            }
              return View(companies);
        }

        public async Task<IActionResult> Copy(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Companies.FindAsync(id);
            var countryList = _context.tblCountry.OrderBy(x => x.CountryId).ToList();
            ViewData["countryList"] = new SelectList(countryList, "CountryName", "CountryName");

            var stateList = _context.tblState.Where(x => x.CountryId == 1).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateName", "StateName");
          
            if (companies == null)
            {
                return NotFound();
            }
            return View(companies);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Group,Code,Name,TimeZone,Status,RegistrationNumber,GST,TinNo,SST,Address,City,PostCode,Country,State,Email,PhoneNumber,Fax,CreatedBy,CreatedDatetime,ModifiedBy,ModifeidDatetime")] Companies companies)
        {
            if (id != companies.Id)
            {
                return NotFound();
            }
            var com = _context.Companies.Where(x => x.Id == id).FirstOrDefault();
                try
                {
                com.ModifiedBy = "Kelvin";
                com.ModifiedDatetime = DateTime.UtcNow;
                _context.Update(com);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniesExists(companies.Id))
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Copy(int id, [Bind("Id,Group,Code,Name,TimeZone,Status,RegistrationNumber,GST,TinNo,SST,Address,City,PostCode,Country,State,Email,PhoneNumber,Fax,CreatedBy,CreatedDatetime,ModifiedBy,ModifeidDatetime")] Companies companies)
        {
            if (id != companies.Id)
            {
                return NotFound();
            }

           
                try
                {
                companies.CreatedBy = "Kelvin";
                companies.CreatedDatetime = DateTime.UtcNow;
                _context.Add(companies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniesExists(companies.Id))
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

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companies == null)
            {
                return NotFound();
            }

            return View(companies);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companies = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(companies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult getState(int id)
        {
            var stateList = _context.tblState.Where(x => x.CountryId == id).OrderByDescending(x => x.StateName).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateId", "StateName");
            return Json(ViewData["stateList"]);

        }
        private bool CompaniesExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
