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
    public class Asset_Brand_MasterController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        public Asset_Brand_MasterController(MyDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, IDataAccessService dataAccessService)
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

        // GET: MasterAssetBrand
        public async Task<IActionResult> Index(string error = null)
        {
            var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);

            //var roleIds = await GetUserRoleIds(HttpContext.User);

            var controller = items.Where(x => x.ControllerName == ControllerContext.ActionDescriptor.ControllerName).ToList();

            if (controller != null)
            {
                var md = await _context.MasterAssetBrand.Select(m => new Asset_Brand_MasterVM
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

            return View(await _context.MasterAssetBrand.OrderBy(m => m.CreatedDatetime).ToListAsync());
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
    

        // GET: MasterAssetBrand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_Brand_Master = await _context.MasterAssetBrand
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset_Brand_Master == null)
            {
                return NotFound();
            }

            return View(asset_Brand_Master);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MasterAssetBrand asset_Brand_Master)
        {
            if (ModelState.IsValid)
            {
                bool isCodeExist = _context.MasterAssetBrand.Any(m => m.Code.Equals(asset_Brand_Master.Code));
                if (isCodeExist)
                {
                    ModelState.AddModelError("Code", "Code Already Exists!");
                    return View(asset_Brand_Master);
                    //return RedirectToAction(nameof(Create), new { error = "Code exists" });
                }

                string newJson = JsonConvert.SerializeObject(asset_Brand_Master);
                bool isCodeExists = IsCodeExists(asset_Brand_Master.Code);
                asset_Brand_Master.CompanyId = 123;
                asset_Brand_Master.IsUsed = true;
                asset_Brand_Master.Status = asset_Brand_Master.Status;
                asset_Brand_Master.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                asset_Brand_Master.CreatedDatetime = DateTime.UtcNow;
                _context.Add(asset_Brand_Master);
                await _context.SaveChangesAsync();
                AuditService.InsertActionLog(asset_Brand_Master.CompanyId, asset_Brand_Master.CreatedBy, "Create", "Master Asset Brand", null, newJson);
                return RedirectToAction(nameof(Index));
            }
            return View(asset_Brand_Master);
        }

        public List<MasterAssetBrand> Search(string code, string name, string active)
        {
            string[] statuses = { "1", "0", "-1" };
            if (!statuses.Contains(active))
            {
                return null;
            }

            List<MasterAssetBrand> res = null;
            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.MasterAssetBrand.ToList().OrderBy(m => m.CreatedDatetime).ToList();

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

        public async Task<IActionResult> Edit(string id)
        {
            int depId = 0;
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out depId))
            {
                return NotFound();
            }


            var asset_Brand_Master = await _context.MasterAssetBrand.FindAsync(depId);
            if (asset_Brand_Master == null)
            {
                return NotFound();
            }
            return View(asset_Brand_Master);
        }

        public JsonResult CheckIfExists(string code, int? id)
        {

            var model = _context.MasterAssetBrand;

            var existing = _context.MasterAssetBrand.Where(x => x.Id == id && x.Code == code).SingleOrDefault();

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
                string dbCode = _context.MasterAssetBrand.FirstOrDefault(m => m.Code.Equals(code))?.Code;
                if (!string.IsNullOrEmpty(dbCode))
                {
                    isExists = true;
                }
            }

            return isExists;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] MasterAssetBrand asset_Brand_Master)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isCodeExist = _context.MasterAssetBrand.Any(m => m.Code.Equals(asset_Brand_Master.Code) && !m.Id.Equals(id));
                    if (isCodeExist)
                    {
                        ModelState.AddModelError("Code", "Code Already Exists!");
                        return View(asset_Brand_Master);
                        //return RedirectToAction(nameof(Index), new { error = "Code exists" });
                    }

                    MasterAssetBrand db_asset_Brand_Master = _context.MasterAssetBrand.FirstOrDefault(m => m.Id.Equals(asset_Brand_Master.Id));
                    if (asset_Brand_Master == null || id != asset_Brand_Master.Id)
                    {
                        return NotFound();
                    }

                    OldData oldData = new OldData();
                    oldData.Code = db_asset_Brand_Master.Code;
                    oldData.Name = db_asset_Brand_Master.Name;
                    oldData.Remark = db_asset_Brand_Master.Remark;
                    oldData.Status = db_asset_Brand_Master.Status;

                    string oldJson = JsonConvert.SerializeObject(oldData);
                    string newJson = JsonConvert.SerializeObject(asset_Brand_Master);

                    db_asset_Brand_Master.Code = asset_Brand_Master.Code;
                    db_asset_Brand_Master.Name = asset_Brand_Master.Name;
                    db_asset_Brand_Master.Status = asset_Brand_Master.Status;
                    db_asset_Brand_Master.Remark = asset_Brand_Master.Remark;
                    db_asset_Brand_Master.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                    db_asset_Brand_Master.ModifiedDatetime = DateTime.UtcNow;
                    AuditService.InsertActionLog(db_asset_Brand_Master.CompanyId, db_asset_Brand_Master.CreatedBy, "Edit", "Master Asset Brand", oldJson, newJson);


                    _context.Update(db_asset_Brand_Master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Asset_Brand_MasterExists(asset_Brand_Master.Id))
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
            return View(asset_Brand_Master);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset_Brand_Master = await _context.MasterAssetBrand
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset_Brand_Master == null)
            {
                return NotFound();
            }

            return View(asset_Brand_Master);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var asset_Brand_Master = await _context.MasterAssetBrand.FindAsync(id);
            _context.MasterAssetBrand.Remove(asset_Brand_Master);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<FileResult> Export()
        {
            string[] names = typeof(MasterAssetBrand).GetProperties().Select(property => property.Name).ToArray();

            string[] columnNames = new string[] { "Code", "Name", "Remark", "Status" };

            MasterAssetBrand entities = new MasterAssetBrand();

            var customers = await _context.MasterAssetBrand.ToListAsync();

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

            var file = "AssetBrandMaster.csv";
            //var file = ViewData;
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", file);
        }


        private bool CreateDm(string code, string name, string remark, bool status)
        {
            bool res = false;
            MasterAssetBrand asset_Brand_Master = new MasterAssetBrand();

            try
            {
                NewData newData = new NewData();
                newData.Code = code;
                newData.Name = name;
                newData.Remark = remark;
                newData.Status = status;

                string newJson = JsonConvert.SerializeObject(newData);

                asset_Brand_Master.Code = code;
                asset_Brand_Master.Name = name;
                asset_Brand_Master.Remark = remark;
                asset_Brand_Master.CompanyId = 123;
                asset_Brand_Master.IsUsed = true;
                asset_Brand_Master.Status = status;
                asset_Brand_Master.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                asset_Brand_Master.CreatedDatetime = DateTime.UtcNow;
                _context.Add(asset_Brand_Master);
                AuditService.InsertActionLog(asset_Brand_Master.CompanyId, asset_Brand_Master.CreatedBy, "Create", "Master Asset Brand", null, newJson);

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
                MasterAssetBrand asset_Brand_Master = _context.MasterAssetBrand.FirstOrDefault(m => m.Code.Equals(code));

                OldData oldData = new OldData();
                oldData.Code = asset_Brand_Master.Code;
                oldData.Name = asset_Brand_Master.Name;
                oldData.Remark = asset_Brand_Master.Remark;
                oldData.Status = asset_Brand_Master.Status;

                string oldJson = JsonConvert.SerializeObject(oldData);
                string newJson = JsonConvert.SerializeObject(asset_Brand_Master);

                if (asset_Brand_Master != null)
                {
                    asset_Brand_Master.Name = name;
                    asset_Brand_Master.Code = code;
                    asset_Brand_Master.Status = status;
                    asset_Brand_Master.Remark = remark;
                    asset_Brand_Master.ModifiedDatetime = DateTime.UtcNow;
                    asset_Brand_Master.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Modifiedby");
                    AuditService.InsertActionLog(asset_Brand_Master.CompanyId, asset_Brand_Master.CreatedBy, "Edit", "Master Asset Brand", oldJson, newJson);
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
                            if (status.ToUpper() == "YES")
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

            return RedirectToAction("Index", "Asset_Brand_Master");

        }

        public void Dispose()
        {
        
        }

        private bool Asset_Brand_MasterExists(int id)
        {
            return _context.MasterAssetBrand.Any(e => e.Id == id);
        }

    }
}
