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
    public class ChartOfAccountsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        public ChartOfAccountsController(MyDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, IDataAccessService dataAccessService)
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

        //public IActionResult Index()
        //{
        //    return View();
        //}

        // GET: ChartOfAccount
        public async Task<IActionResult> Index(string error = null)
        {
            var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);

            var controller = items.Where(x => x.ControllerName == ControllerContext.ActionDescriptor.ControllerName).ToList();

            if (controller != null)
            {
                var md = await _context.ChartOfAccounts.Select(m => new ChartOfAccountsVM
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
                    //StatusId = controller.Select(x => x.StatusId).ToList()
                    StatusId = controller.Select(x => 5).ToList()
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

            return View(await _context.ChartOfAccounts.OrderBy(m => m.CreatedDatetime).ToListAsync());
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
        public async Task<IActionResult> Create([FromForm] ChartOfAccounts chartOfAccounts)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    bool isCodeExist = _context.ChartOfAccounts.Any(m => m.Code.Equals(chartOfAccounts.Code));
                    if (isCodeExist)
                    {
                        ModelState.AddModelError("Code", "Code Already Exists!");
                        return View(chartOfAccounts);
                        //return RedirectToAction(nameof(Create), new { error = "Code exists" });
                    }

                    string newJson = JsonConvert.SerializeObject(chartOfAccounts);
                    bool isCodeExists = IsCodeExists(chartOfAccounts.Code);
                    chartOfAccounts.CompanyId = 123;
                    chartOfAccounts.IsUsed = true;
                    chartOfAccounts.Status = chartOfAccounts.Status;
                    chartOfAccounts.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                    chartOfAccounts.CreatedDatetime = DateTime.UtcNow;
                    _context.Add(chartOfAccounts);
                    await _context.SaveChangesAsync();
                    AuditService.InsertActionLog(chartOfAccounts.CompanyId, chartOfAccounts.CreatedBy, "Create", "Chart Of Accounts", null, newJson);
                    return RedirectToAction(nameof(Index));
                }
                return View(chartOfAccounts);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<ChartOfAccounts> Search(string code, string name, string active)
        {
            string[] statuses = { "1", "0", "-1" };
            if (!statuses.Contains(active))
            {
                return null;
            }

            List<ChartOfAccounts> res = null;
            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.ChartOfAccounts.ToList().OrderBy(m => m.CreatedDatetime).ToList();

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

            var chartOfAccounts = await _context.ChartOfAccounts.FindAsync(depId);

            if (chartOfAccounts == null)
            {
                return NotFound();
            }
            return View(chartOfAccounts);
        }

        public JsonResult CheckIfExists(string code, int? id)
        {

            var model = _context.ChartOfAccounts;

            var existing = _context.ChartOfAccounts.Where(x => x.Id == id && x.Code == code).SingleOrDefault();

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
                string dbCode = _context.ChartOfAccounts.FirstOrDefault(m => m.Code.Equals(code))?.Code;
                if (!string.IsNullOrEmpty(dbCode))
                {
                    isExists = true;
                }
            }

            return isExists;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] ChartOfAccounts chartOfAccounts)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isCodeExist = _context.ChartOfAccounts.Any(m => m.Code.Equals(chartOfAccounts.Code) && !m.Id.Equals(id));
                    if (isCodeExist)
                    {
                        ModelState.AddModelError("Code", "Code Already Exists!");
                        return View(chartOfAccounts);
                        //return RedirectToAction(nameof(Index), new { error = "Code exists" });
                    }

                    ChartOfAccounts db_chartOfAccounts = _context.ChartOfAccounts.FirstOrDefault(m => m.Id.Equals(chartOfAccounts.Id));
                    if (chartOfAccounts == null || id != chartOfAccounts.Id)
                    {
                        return NotFound();
                    }

                    OldData oldData = new OldData();
                    oldData.Code = db_chartOfAccounts.Code;
                    oldData.Name = db_chartOfAccounts.Name;
                    oldData.Remark = db_chartOfAccounts.Remark;
                    oldData.Status = db_chartOfAccounts.Status;

                    string oldJson = JsonConvert.SerializeObject(oldData);
                    string newJson = JsonConvert.SerializeObject(chartOfAccounts);

                    db_chartOfAccounts.Code = chartOfAccounts.Code;
                    db_chartOfAccounts.Name = chartOfAccounts.Name;
                    db_chartOfAccounts.Status = chartOfAccounts.Status;
                    db_chartOfAccounts.Remark = chartOfAccounts.Remark;
                    db_chartOfAccounts.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                    db_chartOfAccounts.ModifiedDatetime = DateTime.UtcNow;
                    AuditService.InsertActionLog(db_chartOfAccounts.CompanyId, db_chartOfAccounts.CreatedBy, "Edit", "Chart Of Accounts", oldJson, newJson);


                    _context.Update(db_chartOfAccounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChartOfAccountsExists(chartOfAccounts.Id))
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
            return View(chartOfAccounts);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chartOfAccounts = await _context.ChartOfAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chartOfAccounts == null)
            {
                return NotFound();
            }

            return View(chartOfAccounts);
        }

        public async Task<IActionResult> DeleteSetting(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int coaID = _context.ChartOfAccountAnalysisSetting.Where(m => m.Id == id).Select(x => x.ChartOfAccountId).FirstOrDefault();

            var AnalysisSet = await _context.ChartOfAccountAnalysisSetting.Where(m => m.Id == id).ToListAsync();
            _context.ChartOfAccountAnalysisSetting.RemoveRange(AnalysisSet);
            _context.SaveChanges();

            var AnalysisSetMap = await _context.ChartOfAccountAnalysisSetting_Mapping
                .Where(m => m.ChartOfAccountAnalysisSetting_Id == id).ToListAsync();

            _context.ChartOfAccountAnalysisSetting_Mapping.RemoveRange(AnalysisSetMap);
            _context.SaveChanges();
            //if (chartOfAccounts == null)
            //{
            //    return NotFound();
            //}

            return RedirectToAction("Edit", "ChartOfAccounts", new { Id = coaID });
        }

        public bool DeleteRuleSettingMapping(int? id)
        {
            try
            {
                //List<ChartOfAccountAnalysisSetting_Mapping> delSetMap = _context.ChartOfAccountAnalysisSetting_Mapping
                //.Where(m => m.ChartOfAccountAnalysisSetting_Id == id).ToList();
                //_context.ChartOfAccountAnalysisSetting_Mapping.RemoveRange(delSetMap);
                //_context.SaveChanges();


                //_context.ChartOfAccountAnalysisSetting_Mapping.RemoveRange(_context.ChartOfAccountAnalysisSetting_Mapping.Find(id));
                //_context.SaveChanges();

                var AnalysisSetMap = _context.ChartOfAccountAnalysisSetting_Mapping
                    .Where(m => m.ChartOfAccountAnalysisSetting_Id == id).ToList();

                _context.ChartOfAccountAnalysisSetting_Mapping.RemoveRange(AnalysisSetMap);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var chartOfAccounts = await _context.ChartOfAccounts.FindAsync(id);
            _context.ChartOfAccounts.Remove(chartOfAccounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<FileResult> Export()
        {
            string[] names = typeof(ChartOfAccounts).GetProperties().Select(property => property.Name).ToArray();

            string[] columnNames = new string[] { "Code", "Name", "Remark", "Status" };

            ChartOfAccounts entities = new ChartOfAccounts();

            var customers = await _context.ChartOfAccounts.ToListAsync();

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

            var file = "ChartOfAccounts.csv";
            //var file = ViewData;
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", file);
        }


        private bool CreateDm(string code, string name, string remark, bool status)
        {
            bool res = false;
            ChartOfAccounts chartOfAccounts = new ChartOfAccounts();

            try
            {
                NewData newData = new NewData();
                newData.Code = code;
                newData.Name = name;
                newData.Remark = remark;
                newData.Status = status;

                string newJson = JsonConvert.SerializeObject(newData);

                chartOfAccounts.Code = code;
                chartOfAccounts.Name = name;
                chartOfAccounts.Remark = remark;
                chartOfAccounts.CompanyId = 123;
                chartOfAccounts.IsUsed = true;
                chartOfAccounts.Status = status;
                chartOfAccounts.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                chartOfAccounts.CreatedDatetime = DateTime.UtcNow;
                _context.Add(chartOfAccounts);
                AuditService.InsertActionLog(chartOfAccounts.CompanyId, chartOfAccounts.CreatedBy, "Create", "Chart Of Accounts", null, newJson);

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
                ChartOfAccounts chartOfAccounts = _context.ChartOfAccounts.FirstOrDefault(m => m.Code.Equals(code));

                OldData oldData = new OldData();
                oldData.Code = chartOfAccounts.Code;
                oldData.Name = chartOfAccounts.Name;
                oldData.Remark = chartOfAccounts.Remark;
                oldData.Status = chartOfAccounts.Status;

                string oldJson = JsonConvert.SerializeObject(oldData);
                string newJson = JsonConvert.SerializeObject(chartOfAccounts);

                if (chartOfAccounts != null)
                {
                    chartOfAccounts.Name = name;
                    chartOfAccounts.Code = code;
                    chartOfAccounts.Status = status;
                    chartOfAccounts.Remark = remark;
                    chartOfAccounts.ModifiedDatetime = DateTime.UtcNow;
                    chartOfAccounts.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Modifiedby");
                    AuditService.InsertActionLog(chartOfAccounts.CompanyId, chartOfAccounts.CreatedBy, "Edit", "Chart Of Accounts", oldJson, newJson);
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
                        throw;
                    }

                    ln++;
                }
            }

            return RedirectToAction("Index", "ChartOfAccounts");

        }

        [HttpGet]
        public ActionResult GetSettings(int id)
        {
            var analysisCdeData = _context.AnalysisCode.ToList();
            var analysisSetData = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == id)
                .Select(m => new ChartOfAccountRuleList
                {
                    Id = m.Id,
                    AnalysisNumber = Convert.ToString(m.AnalysisNumber),
                    FromAnalysisCode = Convert.ToString(m.AnalysisCodeIdFrom),
                    ToAnalysisCode = Convert.ToString(m.AnalysisCodeIdTo),
                    Enabled = m.Enabled
                }).ToList();

            List < ChartOfAccountRuleList> analysisSettings = new List<ChartOfAccountRuleList>();
            foreach (var item in analysisSetData)
            {
                int from = item.FromAnalysisCode == "" ? 0 : Convert.ToInt32(item.FromAnalysisCode);
                int to = item.ToAnalysisCode == "" ? 0 : Convert.ToInt32(item.ToAnalysisCode);
                item.FromAnalysisCode = item.FromAnalysisCode == "" ? "ALL" : analysisCdeData.Where(x => x.Id == from).Select(y => y.Name).FirstOrDefault();
                item.ToAnalysisCode = item.ToAnalysisCode == "" ? "ALL" : analysisCdeData.Where(x => x.Id == to).Select(x => x.Name).FirstOrDefault();
                analysisSettings.Add(item);
            }

            return PartialView("~/Views/ChartOfAccounts/RuleList.cshtml", analysisSettings);
            //return PartialView("~/Views/ChartOfAccounts/RuleList.cshtml");
        }

        [HttpPost]
        public ActionResult AddSetting(int id, [Bind("SelectedAnalysisNumber,MapFull,AnalysisCodeFrom,AnalysisCodeTo")] AddChartOfAccountSettingModel model)
        {
            //var jsonResult = new JsonResultModel();
            //if (!ModelState.IsValid)
            //{
            //    jsonResult.MessageBody = ModelState.GetErrorMessages("<br/>");
            //    jsonResult.MessageTitle = "Error";
            //    return Json(jsonResult, JsonRequestBehavior.AllowGet);
            //}

            if(ModelState.IsValid)
            {
                //var chartOfAccount = _context.ChartOfAccounts.Where(x => x.Id == id).ToList();

                var analysisSettings = new ChartOfAccountAnalysisSetting()
                {
                    ChartOfAccountId = id,
                    AnalysisNumber = model.SelectedAnalysisNumber.Value,
                    Enabled = true,
                    AnalysisCodeSelectionModeId = model.MapFull ? Convert.ToInt32(AnalysisCodeSelectionMode.Full) : Convert.ToInt32(AnalysisCodeSelectionMode.Range),
                    AnalysisCodeIdFrom = model.MapFull ? null : model.AnalysisCodeFrom,
                    AnalysisCodeIdTo = model.MapFull ? null : model.AnalysisCodeTo,
                    CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby"),
                    CreatedDatetime = DateTime.UtcNow,
                };
                _context.ChartOfAccountAnalysisSetting.Add(analysisSettings);
                _context.SaveChanges();


                var AnalysisData = _context.AnalysisCode.ToList();

                if (model.MapFull)
                {
                    AnalysisData = AnalysisData.Where(x => x.AnalysisNumber == model.SelectedAnalysisNumber).ToList();
                }
                else
                {
                    AnalysisData = AnalysisData.Where(x => x.AnalysisNumber == model.SelectedAnalysisNumber &&
                    (x.Id >= model.AnalysisCodeFrom && x.Id <= model.AnalysisCodeTo)).ToList();
                }


                foreach (var data in AnalysisData)
                {
                    var analysisMap = new ChartOfAccountAnalysisSetting_Mapping()
                    {
                        //ChartOfAccountAnalysisSetting_Id = Convert.ToInt32(_context.ChartOfAccountAnalysisSetting
                        //.Where(x => x.ChartOfAccountId == id)
                        //.Select(m => new AnalysisCode()
                        //{
                        //    Id = m.Id
                        //}).OrderByDescending(m => m.Id).FirstOrDefault()),
                        ChartOfAccountAnalysisSetting_Id = _context.ChartOfAccountAnalysisSetting.OrderByDescending(m => m.Id)
                        .Where(x => x.ChartOfAccountId == id)
                        .Select(m => m.Id).FirstOrDefault(),

                        AnalysisCode_Id = data.Id
                    };

                    _context.ChartOfAccountAnalysisSetting_Mapping.Add(analysisMap);
                    _context.SaveChanges();
                }
            }

            //var insertResult = _chartOfAccountService.InsertChartOfAccountAnalysisSetting(analysisSettings);

            //if (insertResult != AddOrUpdateChartOfAccountAnalysisSettingResultEnum.Successful)
            //{
            //    switch (insertResult)
            //    {
            //        case AddOrUpdateChartOfAccountAnalysisSettingResultEnum.OverlappedAnalysisSetting:

            //            jsonResult.MessageBody = "Overlapped Rule";
            //            jsonResult.MessageTitle = "Error";
            //            return Json(jsonResult, JsonRequestBehavior.AllowGet);
            //    }
            //}

            ////TO DO : move to service layer
            //_actionLogService.InsertActionLog(_companyContext.Company.Id, _workContext.User.Id, ActionType.Create, ActionModule.ChartOfAccountSetting,
            //    newData: analysisSettings.ToLog(), objectId: chartOfAccount.Id);

            //jsonResult.IsSuccess = true;
            //jsonResult.MessageBody = "Added Setting";
            //jsonResult.MessageTitle = "Success";
            //return Json(jsonResult, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Edit", "ChartOfAccounts", new { Id = id }); 
        }

        //[Bind("SelectedAnalysisNumberEdit,MapFullEdit,AnalysisCodeFromEdit,AnalysisCodeToEdit")]
        // [Bind("SelectedAnalysisNumber,MapFull,AnalysisCodeFrom,AnalysisCodeTo")], [FromForm] AddChartOfAccountSettingModel AddRulemodel
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult EditSetting(int id, [Bind("SelectedAnalysisNumber,MapFull,AnalysisCodeFrom,AnalysisCodeTo")] AddChartOfAccountSettingModel AddRulemodel)
        {
            try
            {
                ChartOfAccountAnalysisSetting db_AnalysisSetting = _context.ChartOfAccountAnalysisSetting.FirstOrDefault(m => m.Id.Equals(id));
                int coaID = db_AnalysisSetting.ChartOfAccountId;
                ChartOfAccounts dbCOA = _context.ChartOfAccounts.Where(x => x.Id == coaID).FirstOrDefault();

                if (AddRulemodel == null)
                {
                    return NotFound();
                }

                OldDataAnalysisSetting oldData = new OldDataAnalysisSetting();
                oldData.AnalysisNumber = db_AnalysisSetting.AnalysisNumber.ToString();
                oldData.FromAnalysisCode = db_AnalysisSetting.AnalysisCodeIdFrom.ToString();
                oldData.ToAnalysisCode = db_AnalysisSetting.AnalysisCodeIdTo.ToString();
                oldData.Enabled = db_AnalysisSetting.Enabled;

                string oldJson = JsonConvert.SerializeObject(oldData);
                string newJson = JsonConvert.SerializeObject(AddRulemodel);

                //var analysisSettings = new ChartOfAccountAnalysisSetting()
                //{
                db_AnalysisSetting.AnalysisNumber = AddRulemodel.SelectedAnalysisNumber.Value;
                db_AnalysisSetting.AnalysisCodeSelectionModeId = AddRulemodel.MapFull ? Convert.ToInt32(AnalysisCodeSelectionMode.Full) : Convert.ToInt32(AnalysisCodeSelectionMode.Range);
                db_AnalysisSetting.AnalysisCodeIdFrom = AddRulemodel.MapFull ? null : AddRulemodel.AnalysisCodeFrom;
                db_AnalysisSetting.AnalysisCodeIdTo = AddRulemodel.MapFull ? null : AddRulemodel.AnalysisCodeTo;
                db_AnalysisSetting.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                db_AnalysisSetting.ModifiedDatetime = DateTime.UtcNow;
                //};
                _context.Update(db_AnalysisSetting);
                _context.SaveChanges();

                var AnalysisData = _context.AnalysisCode.ToList();
                bool delResult = DeleteRuleSettingMapping(db_AnalysisSetting.Id);

                if (delResult == true)
                {
                    if (AddRulemodel.MapFull)
                    {
                        AnalysisData = AnalysisData.Where(x => x.AnalysisNumber == AddRulemodel.SelectedAnalysisNumber).ToList();
                    }
                    else
                    {
                        AnalysisData = AnalysisData.Where(x => x.AnalysisNumber == AddRulemodel.SelectedAnalysisNumber &&
                        (x.Id >= AddRulemodel.AnalysisCodeFrom && x.Id <= AddRulemodel.AnalysisCodeTo)).ToList();
                    }

                    foreach (var data in AnalysisData)
                    {
                        var analysisMap = new ChartOfAccountAnalysisSetting_Mapping()
                        {
                            ChartOfAccountAnalysisSetting_Id = _context.ChartOfAccountAnalysisSetting.OrderByDescending(m => m.Id)
                            .Where(x => x.ChartOfAccountId == coaID)
                            .Select(m => m.Id).FirstOrDefault(),

                            AnalysisCode_Id = data.Id
                        };

                        _context.ChartOfAccountAnalysisSetting_Mapping.Add(analysisMap);
                        _context.SaveChanges();
                    }
                }


                AuditService.InsertActionLog(dbCOA.CompanyId, db_AnalysisSetting.CreatedBy, "Edit Setting", "Add Rule Setting", oldJson, newJson);

                return RedirectToAction("Edit", "ChartOfAccounts", new { Id = coaID });
            }
            catch
            {
                return NotFound();
            }
            
        }

        public AddChartOfAccountSettingModel EditRuleSetting(int id)
        {
            var COASetting = _context.ChartOfAccountAnalysisSetting.Where(x => x.Id == id).ToList();

            var RuleData = COASetting.Select(m => new AddChartOfAccountSettingModel
            {
                AnalysisCodeFrom = m.AnalysisCodeIdFrom == null ? -1 : m.AnalysisCodeIdFrom,
                AnalysisCodeTo = m.AnalysisCodeIdTo == null ? -1 : m.AnalysisCodeIdTo,
                SelectedAnalysisNumber = m.AnalysisNumber,
                MapFull = m.AnalysisCodeSelectionModeId == 1 ? true : false
            }).FirstOrDefault();

            return RuleData;
        }

        private bool ChartOfAccountsExists(int id)
        {
            return _context.ChartOfAccounts.Any(e => e.Id == id);
        }

        [HttpGet]
        public ActionResult GetAnalysisCodeSelectList(int? analysisNumber = null)
        {
            var analysisCodes = _context.AnalysisCode.Where(x => x.AnalysisNumber == analysisNumber).Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Code + " - " + x.Name
            });

            return Json(analysisCodes);
        }

    }
}
