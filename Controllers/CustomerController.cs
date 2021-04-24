using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixedModules.Data;
using FixedModules.Models;
using FixedAssets.Models;

namespace FixedModules.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MyDbContext _context;

        public CustomerController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
            var test = await _context.Customer.ToListAsync();

            foreach (var item in test)
            {
                int idd = int.Parse(item.ChartOfAccount);
                item.ChartOfAccount = _context.ChartOfAccounts.Where(x => x.Id == idd).Select(y => y.Name).FirstOrDefault();
                customers.Add(item);
            }
            return View(customers);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companies == null)
            {
                return NotFound();
            }

            return View(companies);
        }

        public List<Customer> Search(string code, string name, string active)
        {
            List<Customer> res = null;
            string[] statuses = { "1", "0", "-1" };
            if (!statuses.Contains(active))
            {
                return null;
            }

            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.Customer.ToList().OrderBy(m => m.CreatedDatetime).ToList();

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

                if (!active.Equals("-1"))
                {
                    bool isActive = active.Equals("1") ? true : false;
                    res = res.Where(m => m.Status.Equals(isActive)).ToList();
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

        public JsonResult GetStates(int id)
        {
            
            var stateList = _context.tblState.Where(x => x.CountryId == id).ToList();
            //ViewData["stateList"] = new SelectList(stateList, "StateName", "StateName");

            return Json(stateList);
        }


        // GET: Customer/Create
        public IActionResult Create()
        {
            var countryList =  _context.tblCountry.OrderBy(x => x.CountryId).ToList();
            ViewData["countryList"] = new SelectList(countryList, "CountryId", "CountryName");

            var stateList = _context.tblState.Where(x => x.CountryId == 1).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateId", "StateName");

            var coa = _context.ChartOfAccounts.ToList();
            ViewData["coa"] = new SelectList(coa, "Id", "Name");

            //var TaxCode = _context.TaxCode.ToList();
            //ViewData["TaxCode"] = new SelectList(TaxCode, "Id", "Name");

            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,ChartOfAccount,Remark,Status,RegistrationNo,ContactPerson,Address,City,PostCode,Country,State,Phone,Fax,Email,Website,TaxCode,TaxNumber,TaxExpiry,CreatedBy,CreatedDatetime,ModifiedBy,ModifeidDatetime")] Customer companies)
        {
            //if (ModelState.IsValid)
            //{
            companies.TaxExpiry = DateTime.UtcNow;
            companies.CreatedBy = "Kelvin";
            companies.CreatedDatetime = DateTime.UtcNow;
                _context.Add(companies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(companies);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Customer.FindAsync(id);

            var coa = _context.ChartOfAccounts.ToList();
            ViewData["coa"] = new SelectList(coa, "Id", "Name");

            var countryList = _context.tblCountry.OrderBy(x => x.CountryName == companies.Country).ToList();
            ViewData["countryList"] = new SelectList(countryList, "CountryId", "CountryName");

            var stateList = _context.tblState.ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateId", "StateName");
            
            if (companies == null)
            {
                return NotFound();
            }
              return View(companies);
        }
        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,ChartOfAccount,Remark,Status,RegistrationNo,ContactPerson,Address,City,PostCode,Country,State,Phone,Fax,Email,Website,TaxCode,TaxNumber,TaxExpiry,CreatedBy,CreatedDatetime,ModifiedBy,ModifeidDatetime")] Customer companies)
        {
            if (id != companies.Id)
            {
                return NotFound();
            }
            var com = _context.Customer.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
                try
                {
                companies.CreatedBy = com.CreatedBy;
                companies.CreatedDatetime = com.CreatedDatetime;
                companies.TaxExpiry = DateTime.UtcNow;
                companies.ModifiedBy = "Kelvin";
                companies.ModifiedDatetime = DateTime.UtcNow;
                _context.Entry(companies).State = EntityState.Modified;
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

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companies == null)
            {
                return NotFound();
            }

            return View(companies);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companies = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(companies);
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
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
