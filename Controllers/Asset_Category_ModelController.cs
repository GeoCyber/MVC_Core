using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixedModules.Data;
using FixedModules.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;
using FixedModules.Services;
using System.Security.Claims;
using Newtonsoft.Json;

namespace FixedModules.Controllers
{
    public class Asset_Category_MasterController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        public Asset_Category_MasterController(MyDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, IDataAccessService dataAccessService)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _dataAccessService = dataAccessService;
        }


        // GET: MasterAssetCategory
        public async Task<IActionResult> Index(string error = null)
        {
            var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);

            //var roleIds = await GetUserRoleIds(HttpContext.User);

            var controller = items.Where(x => x.ControllerName == ControllerContext.ActionDescriptor.ControllerName).ToList();

            if (controller != null)
            {
                var md = await _context.MasterAssetCategory.Select(m => new Asset_Category_MasterVM
                {
                    Id = m.Id,
                    Code = m.Code,
                    Name = m.Name,
                    UtilizeL = m.UtilizeL,
                    CodeFormat = m.CodeFormat,
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

            return View(await _context.MasterAssetCategory.OrderBy(m => m.CreatedDatetime).ToListAsync());
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

        // GET: MasterAssetCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MasterAssetCategory = await _context.MasterAssetCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (MasterAssetCategory == null)
            {
                return NotFound();
            }

            return View(MasterAssetCategory);
        }

        // GET: MasterAssetCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MasterAssetCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] MasterAssetCategory MasterAssetCategory)
        {
            if (ModelState.IsValid)
            {
                bool isCodeExist = _context.MasterAssetCategory.Any(m => m.Code.Equals(MasterAssetCategory.Code));
                if (isCodeExist)
                {
                    ModelState.AddModelError("Code", "Code Already Exists!");
                    return View(MasterAssetCategory);
                    //return RedirectToAction(nameof(Create), new { error = "Code exists" });
                }

                string newJson = JsonConvert.SerializeObject(MasterAssetCategory);

                MasterAssetCategory.CompanyId = 123;
                MasterAssetCategory.IsUsed = true;
                MasterAssetCategory.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                MasterAssetCategory.CreatedDatetime = DateTime.UtcNow;
                _context.Add(MasterAssetCategory);
                await _context.SaveChangesAsync();
                AuditService.InsertActionLog(MasterAssetCategory.CompanyId, MasterAssetCategory.CreatedBy, "Create", "Master Asset Category", null, newJson);

                return RedirectToAction(nameof(Index));
            }
            return View(MasterAssetCategory);
        }

        // GET: MasterAssetCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MasterAssetCategory = await _context.MasterAssetCategory.FindAsync(id);
            if (MasterAssetCategory == null)
            {
                return NotFound();
            }
            return View(MasterAssetCategory);
        }

        public List<MasterAssetCategory> Search(string code, string name, string active)
        {
            string[] statuses = { "1", "0", "-1" };
            if (!statuses.Contains(active))
            {
                return null;
            }

            List<MasterAssetCategory> res = null;
            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.MasterAssetCategory.ToList().OrderBy(m => m.CreatedDatetime).ToList();

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

        // POST: MasterAssetCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] MasterAssetCategory MasterAssetCategory)
        {
            if (id != MasterAssetCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool isCodeExist = _context.MasterAssetCategory.Any(m => m.Code.Equals(MasterAssetCategory.Code) && !m.Id.Equals(id));
                    if (isCodeExist)
                    {
                        ModelState.AddModelError("Code", "Code Already Exists!");
                        return View(MasterAssetCategory);
                        //return RedirectToAction(nameof(Index), new { error = "Code exists" });
                    }

                    MasterAssetCategory db_Asset_Category_Master = _context.MasterAssetCategory.FirstOrDefault(m => m.Id.Equals(MasterAssetCategory.Id));

                    OldData oldData = new OldData();
                    oldData.Code = db_Asset_Category_Master.Code;
                    oldData.Name = db_Asset_Category_Master.Name;
                    oldData.Remark = db_Asset_Category_Master.Remark;
                    oldData.Status = db_Asset_Category_Master.Status;

                    string oldJson = JsonConvert.SerializeObject(oldData);
                    string newJson = JsonConvert.SerializeObject(db_Asset_Category_Master);

                    db_Asset_Category_Master.Code = MasterAssetCategory.Code;
                    db_Asset_Category_Master.Name = MasterAssetCategory.Name;
                    db_Asset_Category_Master.UtilizeL = MasterAssetCategory.UtilizeL;
                    db_Asset_Category_Master.Status = MasterAssetCategory.Status;
                    db_Asset_Category_Master.Remark = MasterAssetCategory.Remark;
                    db_Asset_Category_Master.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                    db_Asset_Category_Master.ModifiedDatetime = DateTime.UtcNow;
                    AuditService.InsertActionLog(db_Asset_Category_Master.CompanyId, db_Asset_Category_Master.CreatedBy, "Edit", "Master Asset Category", oldJson, newJson);

                    _context.Update(db_Asset_Category_Master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Asset_Category_MasterExists(MasterAssetCategory.Id))
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
            return View(MasterAssetCategory);
        }

        // GET: MasterAssetCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MasterAssetCategory = await _context.MasterAssetCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (MasterAssetCategory == null)
            {
                return NotFound();
            }

            return View(MasterAssetCategory);
        }

        // POST: MasterAssetCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var MasterAssetCategory = await _context.MasterAssetCategory.FindAsync(id);
            _context.MasterAssetCategory.Remove(MasterAssetCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Asset_Category_MasterExists(int id)
        {
            return _context.MasterAssetCategory.Any(e => e.Id == id);
        }


        [HttpPost]
        public async Task<FileResult> Export()
        {
            string[] names = typeof(MasterAssetCategory).GetProperties().Select(property => property.Name).ToArray();

            string[] columnNames = new string[] { "Code", "Name","Utilize Life","Asset Code Format", "Remark", "Status" };

            //Asset_Location_Master entities = new Asset_Location_Master();

            var customers = await _context.MasterAssetCategory.ToListAsync();

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
                csv += customer.UtilizeL.Replace(",", ";") + ',';
                csv += customer.CodeFormat.Replace(",", ";") + ',';
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

            var file = "AssetCategoryMaster.csv";
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", file);
        }

        [HttpPost]
        public IActionResult Import([FromForm] IFormFile importfilesetting)
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
                            string utilizeLife = values[2];
                            string codeFormat = values[3];
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
                            if ((!string.IsNullOrEmpty(code)) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(status)) && (!string.IsNullOrEmpty(utilizeLife)) && (!string.IsNullOrEmpty(codeFormat)))
                            {
                                bool ReturnStatus = false;

                                if (isCodeExists)
                                {
                                    ReturnStatus = UpdateDm(code, name, remark, utilizeLife, codeFormat, IsStatus);
                                }
                                else
                                {
                                    ReturnStatus = CreateDm(code, name, remark, utilizeLife, codeFormat, IsStatus);
                                }

                                if (ReturnStatus == false)
                                {
                                    TempData["mesage"] = "File Error";
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

            return RedirectToAction("Index", "Asset_Category_Master");
        }

        public JsonResult CheckIfExists(string code, int? id)
        {

            var model = _context.MasterAssetCategory;

            var existing = _context.MasterAssetCategory.Where(x => x.Id == id && x.Code == code).SingleOrDefault();

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
                string dbCode = _context.MasterAssetCategory.FirstOrDefault(m => m.Code.Equals(code))?.Code;
                if (!string.IsNullOrEmpty(dbCode))
                {
                    isExists = true;
                }
            }

            return isExists;
        }


        private bool CreateDm(string code, string name, string remark, string utilizeLife, string codeFormat, bool status)
        {
            bool res = false;
            MasterAssetCategory asset_Category_Master = new MasterAssetCategory();

            try
            {

                NewData newData = new NewData();
                newData.Code = code;
                newData.Name = name;
                newData.Remark = remark;
                newData.Status = status;

                string newJson = JsonConvert.SerializeObject(newData);

                asset_Category_Master.Code = code;
                asset_Category_Master.Name = name;
                asset_Category_Master.Remark = remark;
                asset_Category_Master.UtilizeL = utilizeLife;
                asset_Category_Master.CodeFormat = codeFormat;
                asset_Category_Master.CompanyId = 123;
                asset_Category_Master.IsUsed = true;
                asset_Category_Master.Status = status;
                asset_Category_Master.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                asset_Category_Master.CreatedDatetime = DateTime.UtcNow;
                _context.Add(asset_Category_Master);
                AuditService.InsertActionLog(asset_Category_Master.CompanyId, asset_Category_Master.CreatedBy, "Create", "Master Asset Category", null, newJson);
            _context.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        private bool UpdateDm(string code, string name, string remark, string utilizeLife, string codeFormat, bool status)
        {
            bool res = false;

            try
            {

                MasterAssetCategory asset_Category_Master = _context.MasterAssetCategory.FirstOrDefault(m => m.Code.Equals(code));
                OldData oldData = new OldData();
                oldData.Code = asset_Category_Master.Code;
                oldData.Name = asset_Category_Master.Name;
                oldData.Remark = asset_Category_Master.Remark;
                oldData.Status = asset_Category_Master.Status;

                string oldJson = JsonConvert.SerializeObject(oldData);
                string newJson = JsonConvert.SerializeObject(asset_Category_Master);

                if (asset_Category_Master != null)
                {
                    asset_Category_Master.Name = name;
                    asset_Category_Master.Remark = remark;
                    asset_Category_Master.Status = status;
                    asset_Category_Master.UtilizeL = utilizeLife;
                    asset_Category_Master.CodeFormat = codeFormat;
                    asset_Category_Master.ModifiedDatetime = DateTime.UtcNow;
                    asset_Category_Master.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Modifiedby");
                    AuditService.InsertActionLog(asset_Category_Master.CompanyId, asset_Category_Master.CreatedBy, "Edit", "Master Asset Category", oldJson, newJson);
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
    }
}
