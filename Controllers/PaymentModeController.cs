using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FixedAssets.Models;
using FixedModules.Data;
using FixedModules.Models;
using FixedModules.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FixedAssets.Controllers
{
    public class PaymentModeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataAccessService _dataAccessService;
        public PaymentModeController(MyDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, IDataAccessService dataAccessService)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _dataAccessService = dataAccessService;
        }

        // GET: PaymentMode
        public async Task<IActionResult> Index(string error = null)
        {
            var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);

            //var roleIds = await GetUserRoleIds(HttpContext.User);

            var controller = items.Where(x => x.ControllerName == ControllerContext.ActionDescriptor.ControllerName).ToList();

            if (controller != null)
            {
                var md = await _context.PaymentMode.Select(m => new PaymentModeVM
                {
                    Id = m.Id,
                    Code = m.Code,
                    Name = m.Name,
                    Remark = m.Remark,
                    Status = m.Status,
                    CompanyId = m.CompanyId,
                    IsUsed = m.IsUsed,
                    CreatedBy = m.CreatedBy,
                    CreatedDatetime = m.CreatedDatetime,
                    ModifiedBy = m.ModifiedBy,
                    ModifiedDatetime = m.ModifiedDatetime,
                    StatusId = controller.Select(x => x.StatusId).ToList()
                }).OrderBy(m => m.CreatedDatetime).ToListAsync();

                ViewBag.error = string.Empty;
                if (error != null)
                {
                    ViewBag.error = error;
                }

                return View(md);
            }

            ViewBag.error = string.Empty;
            var TempError = TempData["mesage"];

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

            return View(await _context.PaymentMode.OrderBy(m => m.CreatedDatetime).ToListAsync());
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

        // GET: MasterDepartment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMode = await _context.PaymentMode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMode == null)
            {
                return NotFound();
            }

            return View(paymentMode);
        }

        // GET: MasterDepartment/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditA([FromForm] PaymentMode paymentMode)
        {

            return View(paymentMode);
        }

        // POST: MasterDepartment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PaymentMode paymentMode)
        {
            if (ModelState.IsValid)
            {
                bool isCodeExist = _context.PaymentMode.Any(m => m.Code.Equals(paymentMode.Code));
                if (isCodeExist)
                {
                    ModelState.AddModelError("Code", "Code Already Exists!");
                    return View(paymentMode);
                }
                string newJson = JsonConvert.SerializeObject(paymentMode);

                paymentMode.CompanyId = 123;
                paymentMode.IsUsed = true;
                paymentMode.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                paymentMode.CreatedDatetime = DateTime.UtcNow;
                _context.PaymentMode.Add(paymentMode);
                AuditService.InsertActionLog(paymentMode.CompanyId, paymentMode.CreatedBy, "Create", "Payment Mode", null, newJson);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(paymentMode);
        }

        public JsonResult CheckIfExists(string code, int? id)
        {

            var model = _context.PaymentMode;

            var existing = _context.PaymentMode.Where(x => x.Id == id && x.Code == code).SingleOrDefault();

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

        public List<PaymentMode> Search(string code, string name, string active)
        {
            string[] statuses = { "1", "0", "-1" };
            if (!statuses.Contains(active))
            {
                return null;
            }

            List<PaymentMode> res = null;
            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.PaymentMode.ToList().OrderBy(m => m.CreatedDatetime).ToList();

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

        // GET: MasterDepartment/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            int depId = 0;
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out depId))
            {
                return NotFound();
            }


            var paymentMode = await _context.PaymentMode.FindAsync(depId);
            if (paymentMode == null)
            {
                return NotFound();
            }
            return View(paymentMode);
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
                string dbCode = _context.PaymentMode.FirstOrDefault(m => m.Code.Equals(code))?.Code;
                if (!string.IsNullOrEmpty(dbCode))
                {
                    isExists = true;
                }
            }

            return isExists;
        }

        // POST: MasterDepartment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] PaymentMode paymentMode)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isCodeExist = _context.PaymentMode.Any(m => m.Code.Equals(paymentMode.Code) && !m.Id.Equals(id));
                    if (isCodeExist)
                    {
                        ModelState.AddModelError("Code", "Code Already Exists!");
                        return View(paymentMode);
                        //return RedirectToAction(nameof(Index), new { error = "Code exists" });
                    }
                    PaymentMode db_paymentMode = _context.PaymentMode.FirstOrDefault(m => m.Id.Equals(paymentMode.Id));
                    if (db_paymentMode == null || id != paymentMode.Id)
                    {
                        return NotFound();
                    }

                    OldData oldData = new OldData();
                    oldData.Code = db_paymentMode.Code;
                    oldData.Name = db_paymentMode.Name;
                    oldData.Remark = db_paymentMode.Remark;
                    oldData.Status = db_paymentMode.Status;

                    string oldJson = JsonConvert.SerializeObject(oldData);
                    string newJson = JsonConvert.SerializeObject(paymentMode);

                    db_paymentMode.Code = paymentMode.Code;
                    db_paymentMode.Name = paymentMode.Name;
                    db_paymentMode.Remark = paymentMode.Remark;
                    db_paymentMode.Status = paymentMode.Status;
                    db_paymentMode.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                    db_paymentMode.ModifiedDatetime = DateTime.UtcNow;

                    _context.PaymentMode.Update(db_paymentMode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Department_MasterExists(paymentMode.Id))
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
            return View(paymentMode);
        }

        // GET: MasterDepartment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMode = await _context.PaymentMode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMode == null)
            {
                return NotFound();
            }

            return View(paymentMode);
        }

        // POST: MasterDepartment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paymentMode = await _context.PaymentMode.FindAsync(id);
            _context.PaymentMode.Remove(paymentMode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<FileResult> Export()
        {
            string[] names = typeof(PaymentMode).GetProperties().Select(property => property.Name).ToArray();

            string[] columnNames = new string[] { "Code", "Name", "Remark", "Status" };

            PaymentMode entities = new PaymentMode();

            var customers = await _context.PaymentMode.ToListAsync();

            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

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



            var file = "PaymentMode.csv";
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", file);
        }
        private bool CreateDm(string code, string name, string remark, bool status)
        {
            bool res = false;
            PaymentMode paymentMode = new PaymentMode();

            try
            {
                paymentMode.Code = code;
                paymentMode.Name = name;
                paymentMode.Remark = remark;
                //department_Master.Id = ;
                paymentMode.CompanyId = 123;
                paymentMode.IsUsed = true;
                paymentMode.Status = status;
                paymentMode.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                paymentMode.CreatedDatetime = DateTime.UtcNow;
                _context.Add(paymentMode);
                _context.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        private bool UpdateDm(string code, string name, string remark, bool status)
        {
            bool res = false;

            try
            {
                PaymentMode paymentMode = _context.PaymentMode.FirstOrDefault(m => m.Code.Equals(code));
                if (paymentMode != null)
                {
                    paymentMode.Name = name;
                    paymentMode.Remark = remark;
                    paymentMode.Status = status;
                    paymentMode.ModifiedDatetime = DateTime.UtcNow;
                    paymentMode.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Modifiedby");
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
                            string remark = values[2];
                            string status = values[3];

                            if (string.IsNullOrEmpty(remark))
                            {
                                remark = null;
                            }
                            if (status.ToUpper() == "YES" || status == "1")
                            {
                                IsStatus = true;
                            }


                            bool isCodeExists = IsCodeExists(code);
                            if ((!string.IsNullOrEmpty(code)) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(status)))
                            {
                                if (isCodeExists)
                                {
                                    UpdateDm(code, name, remark, IsStatus);
                                }
                                else
                                {
                                    CreateDm(code, name, remark, IsStatus);
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

            return RedirectToAction("Index", "PaymentMode");

        }


        private bool Department_MasterExists(int id)
        {
            return _context.PaymentMode.Any(e => e.Id == id);
        }
    }
}