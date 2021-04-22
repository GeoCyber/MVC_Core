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
    public class Analysis_CodeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        public Analysis_CodeController(MyDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, IDataAccessService dataAccessService)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _dataAccessService = dataAccessService;
        }


        public IActionResult Create()
        {
            return View();
        }

        // GET: AnalysisCode
        public async Task<IActionResult> Index(string error = null)
        {
            var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);
            var TempError = TempData["mesage"];
            //var roleIds = await GetUserRoleIds(HttpContext.User);

            var controller = items.Where(x => x.ControllerName == ControllerContext.ActionDescriptor.ControllerName).ToList();

            if (controller != null)
            {
                var md = await _context.AnalysisCode.Select(m => new Analysis_CodeVM
                {
                    Id = m.Id,
                    Code = m.Code,
                    Name = m.Name,
                    Remark = m.Remark,
                    AnalysisNumber = m.AnalysisNumber,
                    Status = m.Status,
                    CompanyId = m.CompanyId,
                    IsUsed = m.IsUsed,
                    CreatedBy = m.CreatedBy,
                    CreatedDatetime = m.CreatedDatetime,
                    ModifiedBy = m.ModifiedBy,
                    ModifiedDatetime = m.ModifiedDatetime,
                    //StatusId = controller.Select(x => x.StatusId).ToList()
                    StatusId = controller.Select(x => 5).ToList()
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

            return View(await _context.AnalysisCode.OrderBy(m => m.CreatedDatetime).ToListAsync());
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AnalysisCode analysis_Code)
        {
            if (ModelState.IsValid)
            {
                bool isCodeExist = _context.AnalysisCode.Any(m => m.Code.Equals(analysis_Code.Code));
                if (isCodeExist)
                {
                    ModelState.AddModelError("Code", "Code Already Exists!");
                    return View(analysis_Code);
                    //return RedirectToAction(nameof(Create), new { error = "Code exists" });
                }

                string newJson = JsonConvert.SerializeObject(analysis_Code);
                bool isCodeExists = IsCodeExists(analysis_Code.Code);
                analysis_Code.CompanyId = 123;
                analysis_Code.IsUsed = true;
                analysis_Code.Status = analysis_Code.Status;
                analysis_Code.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                analysis_Code.CreatedDatetime = DateTime.UtcNow;
                _context.Add(analysis_Code);
                await _context.SaveChangesAsync();
                AuditService.InsertActionLog(analysis_Code.CompanyId, analysis_Code.CreatedBy, "Create", "Analysis Code", null, newJson);
                return RedirectToAction(nameof(Index));
            }
            return View(analysis_Code);
        }

        public List<AnalysisCode> Search(string code, string name, string analysisNo, string active)
        {
            string[] statuses = { "1", "0", "-1" };
            if (!statuses.Contains(active))
            {
                return null;
            }

            List<AnalysisCode> res = null;
            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.AnalysisCode.ToList().OrderBy(m => m.CreatedDatetime).ToList();

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

                if (analysisNo != "0")
                {
                    analysisNo = analysisNo.Trim();
                    res = res.Where(m => m.AnalysisNumber.Equals(Convert.ToInt32(analysisNo))).ToList();
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

        public async Task<IActionResult> Edit(string id)
        {
            int depId = 0;
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out depId))
            {
                return NotFound();
            }


            var analysis_Code = await _context.AnalysisCode.FindAsync(depId);
            if (analysis_Code == null)
            {
                return NotFound();
            }
            return View(analysis_Code);
        }

        public JsonResult CheckIfExists(string code, int? id)
        {

            var model = _context.AnalysisCode;

            var existing = _context.AnalysisCode.Where(x => x.Id == id && x.Code == code).SingleOrDefault();

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
                string dbCode = _context.AnalysisCode.FirstOrDefault(m => m.Code.Equals(code))?.Code;
                if (!string.IsNullOrEmpty(dbCode))
                {
                    isExists = true;
                }
            }

            return isExists;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] AnalysisCode analysis_Code)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isCodeExist = _context.AnalysisCode.Any(m => m.Code.Equals(analysis_Code.Code) && !m.Id.Equals(id));
                    if (isCodeExist)
                    {
                        ModelState.AddModelError("Code", "Code Already Exists!");
                        return View(analysis_Code);
                        //return RedirectToAction(nameof(Index), new { error = "Code exists" });
                    }

                    AnalysisCode db_analysis_Code = _context.AnalysisCode.FirstOrDefault(m => m.Id.Equals(analysis_Code.Id));
                    if (analysis_Code == null || id != analysis_Code.Id)
                    {
                        return NotFound();
                    }

                    OldDataAnalysis oldData = new OldDataAnalysis();
                    oldData.Code = db_analysis_Code.Code;
                    oldData.Name = db_analysis_Code.Name;
                    oldData.Remark = db_analysis_Code.Remark;
                    oldData.AnalysisNumber = db_analysis_Code.AnalysisNumber;
                    oldData.Status = db_analysis_Code.Status;

                    string oldJson = JsonConvert.SerializeObject(oldData);
                    string newJson = JsonConvert.SerializeObject(analysis_Code);

                    db_analysis_Code.Code = analysis_Code.Code;
                    db_analysis_Code.Name = analysis_Code.Name;
                    db_analysis_Code.Status = analysis_Code.Status;
                    db_analysis_Code.Remark = analysis_Code.Remark;
                    db_analysis_Code.AnalysisNumber = analysis_Code.AnalysisNumber;
                    db_analysis_Code.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                    db_analysis_Code.ModifiedDatetime = DateTime.UtcNow;
                    AuditService.InsertActionLog(db_analysis_Code.CompanyId, db_analysis_Code.CreatedBy, "Edit", "Analysis Code", oldJson, newJson);


                    _context.Update(db_analysis_Code);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Analysis_CodeExists(analysis_Code.Id))
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
            return View(analysis_Code);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysis_Code = await _context.AnalysisCode
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analysis_Code == null)
            {
                return NotFound();
            }

            return View(analysis_Code);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var analysis_Code = await _context.AnalysisCode.FindAsync(id);
            _context.AnalysisCode.Remove(analysis_Code);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<FileResult> Export()
        {
            string[] names = typeof(AnalysisCode).GetProperties().Select(property => property.Name).ToArray();

            string[] columnNames = new string[] { "Code", "Name", "AnalysisNumber", "Remark", "Status" };

            AnalysisCode entities = new AnalysisCode();

            var customers = await _context.AnalysisCode.ToListAsync();

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
                csv += customer.AnalysisNumber.ToString().Replace(",", ";") + ',';

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

            var file = "AnalysisCode.csv";
            //var file = ViewData;
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", file);
        }


        private bool CreateDm(string code, string name, string analysisNo, string remark, bool status)
        {
            bool res = false;
            AnalysisCode analysis_Code = new AnalysisCode();

            try
            {
                NewDataAnalysis newData = new NewDataAnalysis();
                newData.Code = code;
                newData.Name = name;
                newData.AnalysisNumber = Convert.ToInt32(analysisNo);
                newData.Remark = remark;
                newData.Status = status;

                string newJson = JsonConvert.SerializeObject(newData);

                analysis_Code.Code = code;
                analysis_Code.Name = name;
                analysis_Code.AnalysisNumber = Convert.ToInt32(analysisNo);
                analysis_Code.Remark = remark;
                analysis_Code.CompanyId = 123;
                analysis_Code.IsUsed = true;
                analysis_Code.Status = status;
                analysis_Code.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                analysis_Code.CreatedDatetime = DateTime.UtcNow;
                _context.Add(analysis_Code);
                AuditService.InsertActionLog(analysis_Code.CompanyId, analysis_Code.CreatedBy, "Create", "Analysis Code", null, newJson);

                _context.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        private bool UpdateDm(string code, string name, string analysisNo, string remark, bool status)
        {
            bool res = false;

            try
            {
                AnalysisCode analysis_Code = _context.AnalysisCode.FirstOrDefault(m => m.Code.Equals(code));

                OldDataAnalysis oldData = new OldDataAnalysis();
                oldData.Code = analysis_Code.Code;
                oldData.Name = analysis_Code.Name;
                oldData.AnalysisNumber = analysis_Code.AnalysisNumber;
                oldData.Remark = analysis_Code.Remark;
                oldData.Status = analysis_Code.Status;

                string oldJson = JsonConvert.SerializeObject(oldData);
                string newJson = JsonConvert.SerializeObject(analysis_Code);

                if (analysis_Code != null)
                {
                    analysis_Code.Name = name;
                    analysis_Code.Code = code;
                    analysis_Code.AnalysisNumber = Convert.ToInt32(analysisNo);
                    analysis_Code.Status = status;
                    analysis_Code.Remark = remark;
                    analysis_Code.ModifiedDatetime = DateTime.UtcNow;
                    analysis_Code.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Modifiedby");
                    AuditService.InsertActionLog(analysis_Code.CompanyId, analysis_Code.CreatedBy, "Edit", "Analysis Code", oldJson, newJson);
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
                            string analysisNo = values[2];
                            string remark = values[3];
                            string status = values[4];

                            if (string.IsNullOrEmpty(remark))
                            {
                                remark = null;
                            }
                            if (status.ToUpper() == "TRUE")
                            {
                                IsStatus = true;
                            }

                            if(Convert.ToInt32(analysisNo)>10)
                            {
                                analysisNo = null;
                            }

                            bool isCodeExists = IsCodeExists(code);
                            if ((!string.IsNullOrEmpty(code)) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(status)) && (!string.IsNullOrEmpty(analysisNo)))
                            {
                                if (isCodeExists)
                                {
                                    UpdateDm(code, name, analysisNo, remark, IsStatus);
                                }
                                else
                                {
                                    CreateDm(code, name, analysisNo, remark, IsStatus);
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

            return RedirectToAction("Index", "Analysis_Code");

        }

        public void Dispose()
        {
        
        }

        private bool Analysis_CodeExists(int id)
        {
            return _context.AnalysisCode.Any(e => e.Id == id);
        }

    }
}
