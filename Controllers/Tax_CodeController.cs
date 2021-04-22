using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixedModules.Data;
using FixedModules.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using FixedModules.Services;
using System.Security.Claims;
using Newtonsoft.Json;

namespace FixedModules.Controllers
{
    public class Tax_CodeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        public Tax_CodeController(MyDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, IDataAccessService dataAccessService)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _dataAccessService = dataAccessService;
        }

        // GET: TaxCode
        public async Task<IActionResult> Index(string error = null)
        {
            
            var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);
            //var roleIds = await GetUserRoleIds(HttpContext.User);
            var COAData = _context.ChartOfAccounts;
            var controller = items.Where(x => x.ControllerName == ControllerContext.ActionDescriptor.ControllerName).ToList();
            var TempError = TempData["mesage"];

            var COAList = _context.ChartOfAccounts.ToList();
            ViewData["COAList"] = new SelectList(COAList, "Id", "Name");

            if (controller != null)
            {
                var md = await _context.TaxCode
                    .Join(
                            COAData,
                            tax => Convert.ToInt32(tax.ChartOfAccount),
                            COA => COA.Id,
                            (tax, COA) => new { COACdeList = COA, TAXList = tax })
                    .Select(m => new TaxCodeVM
                    {
                        Id = m.TAXList.Id,
                        Code = m.TAXList.Code,
                        Name = m.TAXList.Name,
                        ChartOfAccount = m.COACdeList.Name,
                        Rate = m.TAXList.Rate,
                        Remark = m.TAXList.Remark,
                        Status = m.TAXList.Status,
                        CompanyId = m.TAXList.CompanyId,
                        IsUsed = m.TAXList.IsUsed,
                        CreatedBy = m.TAXList.CreatedBy,
                        CreatedDatetime = m.TAXList.CreatedDatetime,
                        ModifiedBy = m.TAXList.ModifiedBy,
                        ModifiedDatetime = m.TAXList.ModifiedDatetime,
                        StatusId = controller.Select(x => x.StatusId).ToList()
                    }).OrderBy(m => m.CreatedDatetime).ToListAsync();

                ViewBag.error = string.Empty;
                if (error != null)
                {
                    ViewBag.error = error;
                }

                if (TempError != null)
                {
                    ViewBag.error = TempError;
                }

                return View(md);
            }

            ViewBag.error = string.Empty;

            if (error != null)
            {
                ViewBag.error = error;
            }

            if (TempError != null)
            {
                ViewBag.error = TempError;
            }

            var currentuser = await _userManager.GetRolesAsync(await _userManager.GetUserAsync(HttpContext.User));

            foreach (var user in currentuser)
            {
                var role = _context.RoleMenuPermission.Include(x => x.RolesStatus).Where(x => x.RoleId == user).SingleOrDefaultAsync();
            }

            ViewBag.role = currentuser;

            return View(await _context.TaxCode.OrderBy(m => m.CreatedDatetime).ToListAsync());
        }

        private async Task<List<string>> GetUserRoleIds(ClaimsPrincipal ctx)
        {
            var userId = GetUserId(ctx);
            var data = await (from role in _context.UserRoles
                              where role.UserId == userId
                              select role.RoleId).ToListAsync();

            return data;
        }

        private string GetUserId(ClaimsPrincipal user)
        {
            return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public IActionResult Create()
        {
            var COAList = _context.ChartOfAccounts.ToList();
            ViewData["COAList"] = new SelectList(COAList, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TaxCode taxCode)
        {
            if (ModelState.IsValid)
            {
                bool isCodeExist = _context.TaxCode.Any(m => m.Code.Equals(taxCode.Code));
                if (isCodeExist)
                {
                    ModelState.AddModelError("Code", "Code Already Exists!");
                    return View(taxCode);
                    //return RedirectToAction(nameof(Create), new { error = "Code exists" });
                }

                string newJson = JsonConvert.SerializeObject(taxCode);
                bool isCodeExists = IsCodeExists(taxCode.Code);
                taxCode.CompanyId = 123;
                taxCode.ChartOfAccount = taxCode.ChartOfAccount;
                taxCode.IsUsed = true;
                taxCode.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                taxCode.CreatedDatetime = DateTime.UtcNow;
                _context.Add(taxCode);
                await _context.SaveChangesAsync();
                AuditService.InsertActionLog(taxCode.CompanyId, taxCode.CreatedBy, "Create", "Tax Code", null, newJson);
                return RedirectToAction(nameof(Index));
            }
            return View(taxCode);
        }

        public List<TaxCode> Search(string code, string name, string coa, string active)
        {
            string[] statuses = { "1", "0", "-1" };
            var COAData = _context.ChartOfAccounts;
            if (!statuses.Contains(active))
            {
                return null;
            }

            List<TaxCode> res = null;
            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.TaxCode.ToList().OrderBy(m => m.CreatedDatetime).ToList();

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

                if (!string.IsNullOrEmpty(coa))
                {
                    coa = coa.Trim();
                    res = res.Where(m => m.ChartOfAccount.ToLower().Equals(coa)).ToList();
                }

                if (!active.Equals("-1"))
                {
                    bool isActive = active.Equals("1") ? true : false;
                    res = res.Where(m => m.Status.Equals(isActive)).ToList();
                }

                res = res
               .Join(
                           COAData,
                           tax => Convert.ToInt32(tax.ChartOfAccount),
                           COA => COA.Id,
                           (tax, COA) => new { COACdeList = COA, TAXList = tax })
               .Select(m => new TaxCode
               {
                   Id = m.TAXList.Id,
                   Code = m.TAXList.Code,
                   Name = m.TAXList.Name,
                   ChartOfAccount = m.COACdeList.Name,
                   Rate = m.TAXList.Rate,
                   Remark = m.TAXList.Remark,
                   Status = m.TAXList.Status,
                   CompanyId = m.TAXList.CompanyId,
                   IsUsed = m.TAXList.IsUsed,
                   CreatedBy = m.TAXList.CreatedBy,
                   CreatedDatetime = m.TAXList.CreatedDatetime,
                   ModifiedBy = m.TAXList.ModifiedBy,
                   ModifiedDatetime = m.TAXList.ModifiedDatetime
               }).OrderBy(m => m.CreatedDatetime).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }

            return res;
        }

        public async Task<IActionResult> Edit(string id)
        {
            int depId = 0;
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out depId))
            {
                return NotFound();
            }

            var COAList = _context.ChartOfAccounts.ToList();
            ViewData["COAList"] = new SelectList(COAList, "Id", "Name");

            var taxCode = await _context.TaxCode.FindAsync(depId);
            if (taxCode == null)
            {
                return NotFound();
            }
            return View(taxCode);
        }

        public JsonResult CheckIfExists(string code, int? id)
        {

            var model = _context.TaxCode;

            var existing = _context.TaxCode.Where(x => x.Id == id && x.Code == code).SingleOrDefault();

            if (existing != null)
            {
                return Json(true);
            }

            var valid = !model.Any(x => x.Code == code);

            if (!valid)
            {
                ModelState.AddModelError("Code", "Code Already Exist!");
                return Json("Code Already Exist!");
            }
            // Returns "false" (i.e., "not valid") if a user with the specified email address already exists.
            return Json(true);
        }
        public bool IsCodeExists(string code)
        {
            bool isExists = false;
            if (string.IsNullOrEmpty(code))
            {
                isExists = false;
            }
            else
            {
                string dbCode = _context.TaxCode.FirstOrDefault(m => m.Code.Equals(code))?.Code;
                if (!string.IsNullOrEmpty(dbCode))
                {
                    isExists = true;
                }
            }

            return isExists;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] TaxCode taxCode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isCodeExist = _context.TaxCode.Any(m => m.Code.Equals(taxCode.Code) && !m.Id.Equals(id));
                    if (isCodeExist)
                    {
                        ModelState.AddModelError("Code", "Code Already Exists!");
                        return View(taxCode);
                        //return RedirectToAction(nameof(Index), new { error = "Code exists" });
                    }

                    TaxCode db_taxCode = _context.TaxCode.FirstOrDefault(m => m.Id.Equals(taxCode.Id));
                    if (taxCode == null || id != taxCode.Id)
                    {
                        return NotFound();
                    }

                    OldDataTax oldData = new OldDataTax();
                    oldData.Code = db_taxCode.Code;
                    oldData.Name = db_taxCode.Name;
                    oldData.ChartOfAccount = db_taxCode.ChartOfAccount;
                    oldData.Rate = Convert.ToString(db_taxCode.Rate);
                    oldData.Remark = db_taxCode.Remark;
                    oldData.Status = db_taxCode.Status;

                    string oldJson = JsonConvert.SerializeObject(oldData);
                    string newJson = JsonConvert.SerializeObject(taxCode);

                    db_taxCode.Code = taxCode.Code;
                    db_taxCode.Name = taxCode.Name;
                    db_taxCode.ChartOfAccount = taxCode.ChartOfAccount;
                    db_taxCode.Rate = taxCode.Rate;
                    db_taxCode.Status = taxCode.Status;
                    db_taxCode.Remark = taxCode.Remark;
                    db_taxCode.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                    db_taxCode.ModifiedDatetime = DateTime.UtcNow;
                    AuditService.InsertActionLog(db_taxCode.CompanyId, db_taxCode.CreatedBy, "Edit", "Tax Code", oldJson, newJson);

                    _context.Update(db_taxCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Tax_CodeExists(taxCode.Id))
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
            return View(taxCode);
        }

        [HttpPost]
        public async Task<FileResult> Export()
        {
            string[] names = typeof(TaxCode).GetProperties().Select(property => property.Name).ToArray();

            string[] columnNames = new string[] { "Code", "Name", "ChartOfAccount", "Rate", "Remark", "Status" };

            TaxCode entities = new TaxCode();

            var customers = await _context.TaxCode.ToListAsync();

            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            bool IsStatus = false;

            foreach (string columnName in columnNames)
            {
                //Add the Header row for CSV file.
                csv += columnName + ',';
            }

            //remove symbol "," behind
            csv = csv.Remove(csv.Length - 1, 1);

            //Add new line.
            csv += "\r\n";

            foreach (var customer in customers)
            {
                //Add the Data rows.
                csv += customer.Code.Replace(",", ";") + ',';
                csv += customer.Name.Replace(",", ";") + ',';
                csv += customer.ChartOfAccount.Replace(",", ";") + ',';
                csv += customer.Rate.ToString().Replace(",", ";") + ',';

                if (customer.Remark != null)
                {
                    csv += customer.Remark.Replace(",", ";") + ',';
                }
                else
                { 
                    customer.Remark = "";
                    csv += customer.Remark.Replace(",", ";") + ',';
                }

                if (customer.Status != false)
                {
                    string Status = "YES";
                    csv += Status.Replace(",", ";");
                }
                if (customer.Status != true)
                {
                    string Status = "NO";
                    csv += Status.Replace(",", ";");
                }

                //Add new line.
                csv += "\r\n";
            }

            var file = "TaxCode.csv";
            //var file = ViewData;
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", file);
        }


        private bool CreateDm(string code, string name, string chartofaccount, string rate, string remark, bool status)
        {
            bool res = false;
            TaxCode taxCode = new TaxCode();

            try
            {
                NewDataTax newData = new NewDataTax();
                newData.Code = code;
                newData.Name = name;
                newData.ChartOfAccount = chartofaccount;
                newData.Rate = rate;
                newData.Remark = remark;
                newData.Status = status;

                string newJson = JsonConvert.SerializeObject(newData);

                taxCode.Code = code;
                taxCode.Name = name;
                taxCode.ChartOfAccount = chartofaccount;
                taxCode.Rate = Convert.ToDecimal(rate);
                taxCode.Remark = remark;
                taxCode.CompanyId = 123;
                taxCode.IsUsed = true;
                taxCode.Status = status;
                taxCode.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                taxCode.CreatedDatetime = DateTime.UtcNow;
                _context.Add(taxCode);
                AuditService.InsertActionLog(taxCode.CompanyId, taxCode.CreatedBy, "Create", "Tax Code", null, newJson);

                _context.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        private bool UpdateDm(string code, string name, string chartofaccount, string rate, string remark, bool status)
        {
            bool res = false;

            try
            {
                TaxCode taxCode = _context.TaxCode.FirstOrDefault(m => m.Code.Equals(code));

                OldDataTax oldData = new OldDataTax();
                oldData.Code = taxCode.Code;
                oldData.Name = taxCode.Name;
                oldData.ChartOfAccount = chartofaccount;
                oldData.Rate = rate;
                oldData.Remark = taxCode.Remark;
                oldData.Status = taxCode.Status;

                string oldJson = JsonConvert.SerializeObject(oldData);
                string newJson = JsonConvert.SerializeObject(taxCode);

                if (taxCode != null)
                {
                    taxCode.Name = name;
                    taxCode.Code = code;
                    taxCode.ChartOfAccount = chartofaccount;
                    taxCode.Rate = Convert.ToDecimal(rate);
                    taxCode.Status = status;
                    taxCode.Remark = remark;
                    taxCode.ModifiedDatetime = DateTime.UtcNow;
                    taxCode.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Modifiedby");
                    AuditService.InsertActionLog(taxCode.CompanyId, taxCode.CreatedBy, "Edit", "Tax Code", oldJson, newJson);
                    _context.SaveChanges();
                    res = true;
                }
            }
            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        [HttpPost]
        public IActionResult Import([FromForm]IFormFile importfilesetting)
        {
          
            using (var reader = new StreamReader(importfilesetting.OpenReadStream()))
            {
                int ln = 1;
                while (!reader.EndOfStream)
                {
                    bool IsStatus = false;
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    try
                    {
                        if (ln != 1)
                        {
                            string code = values[0];
                            string name = values[1];
                            string coa = values[2];
                            string rate = values[3];
                            string remark = values[4];
                            string status = values[5];

                            if (string.IsNullOrEmpty(remark))
                            {
                                remark = null;
                            }
                            if (status.ToUpper() == "YES")
                            {
                                IsStatus = true;
                            }

                            bool isCodeExists = IsCodeExists(code);
                            if ((!string.IsNullOrEmpty(code)) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(status)))
                            {
                                if (isCodeExists)
                                {
                                    UpdateDm(code, name, coa, rate, remark, IsStatus);
                                }
                                else
                                {
                                    CreateDm(code, name, coa, rate, remark, IsStatus);
                                }
                            }
                            else
                            {
                                TempData["mesage"] = "File Error";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["mesage"] = "File Error";
                    }

                    ln++;
                }
            }

            return RedirectToAction("Index", "Tax_Code");

        }

        public void Dispose()
        {
        
        }

        private bool Tax_CodeExists(int id)
        {
            return _context.TaxCode.Any(e => e.Id == id);
        }

    }
}
