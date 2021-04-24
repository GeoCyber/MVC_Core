using FixedAssets.Models;
using FixedModules.Data;
using FixedModules.Models;
using FixedModules.Services;
using FixedModules.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FixedModules.Controllers
{
    public class Master_AllModuleController : Controller
    {

        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IImportService _import;
        public IConfiguration Configuration { get; }
        public Master_AllModuleController(MyDbContext context, IWebHostEnvironment env, IConfiguration _Configuration, IImportService import)
        {
            _context = context;
            _env = env;
            _import = import;
            Configuration = _Configuration;
        }


        private string GetUserId(ClaimsPrincipal user) => ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public IActionResult Index()
        {
            FixedAssetViewModel _view = new FixedAssetViewModel();
            _view.Fixed_Asset_Registers = _context.FixedAssetRegisters.Where(a => a.CreationStatus != 1).ToList();
            var statuslist = new List<SelectListItem>
            {
                new SelectListItem {Text = "Draft", Value = "1"},
                new SelectListItem {Text = "Save", Value = "2"},
                new SelectListItem {Text = "Post", Value = "3"},

            };
            var assettypelist = new List<SelectListItem>
            {
                new SelectListItem {Text = "Depreciation", Value = "1"},
                new SelectListItem {Text = "Non-Depreciation", Value = "0"},

            };
            _view.asset_type = assettypelist;
            _view.static_list = statuslist;

            _view.asset_Category = _context.MasterAssetCategory.Where(a => a.Status == true).ToList();
            return View(_view);
        }



        public IActionResult Create()
        {

            var data = new Fixed_Asset_Register();
            //Check IF Draft Avaialble

            var draft = _context.FixedAssetRegisters.SingleOrDefault(a => a.CreationStatus == 1 && a.CreatedBy == GetUserId(User));
            if (draft != null) data = draft;


            //Static Data
            var listCategories = new List<SelectListItem>
            {
                new SelectListItem {Text = "Test1", Value = "1"},
                new SelectListItem {Text = "Test2", Value = "2"},
                new SelectListItem {Text = "Test3", Value = "3"},
                new SelectListItem {Text = "Test4", Value = "4"}
            };

            var assettypelist = new List<SelectListItem>
            {
                new SelectListItem {Text = "Depreciation", Value = "1"},
                new SelectListItem {Text = "Non-Depreciation", Value = "0"},

            };
            TempData["utilifelife"] = _context.Setup.Select(x => x.Value).FirstOrDefault();
            TempData["reducingbalance"] = _context.Setup.Select(x => x.Reducing_Balance_UL).FirstOrDefault();
            TempData["DailyUL"] = _context.Setup.Select(x => x.Daily_UL).FirstOrDefault();
            TempData["MonthlyUL"] = _context.Setup.Select(x => x.Monthy_UL).FirstOrDefault();
            //var value = _context.MasterAssetCategory.Select(x => new SelectListItem() { Value = x.id, Text = x.Name });

            data.asset_Category = _context.MasterAssetCategory.Where(a => a.Status == true).ToList();
            data.asset_model = _context.MasterAssetModel.Where(a => a.Status == true).ToList();
            data.asset_brand = _context.MasterAssetBrand.Where(a => a.Status == true).ToList();
            data.supplier = _context.Supplier.Where(a => a.Status == true).ToList();
            data.fixed_asset_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
            data.pl_description_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
            data.bs_description_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
            data.disposal_gain_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
            data.disposal_loss_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
            data.write_off_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
            data.department_master = _context.MasterDepartment.Where(a => a.Status == true).ToList();
            data.asset_location_master = _context.MasterAssetLocation.Where(a => a.Status == true).ToList();
            data.analysis_Codes = _context.AnalysisCode.Where(a => a.Status == true).ToList();

            data.asset_type = assettypelist;
            var dateAndTime = DateTime.Now;
            data.DepreciationStartDate = dateAndTime.Date;

            data.PurchaseDate = dateAndTime.Date;
            data.StaticList = listCategories;

            return View(data);
        }

        [HttpPost]
        public IActionResult Create(Fixed_Asset_Register model, string chartofaccount)
        {

            try
            {

                model.MultiFormData = JsonConvert.DeserializeObject<List<AllModuleFormSub>>(model.MultiValuesForm);
                

                model.CreatedBy = GetUserId(User);
                model.CreationTimeStamp = DateTime.Now;
                if (model.CreationStatus == 1) model.AssetCode = "";
                else
                {
                    var Assetcategorydetails = _context.MasterAssetCategory.FirstOrDefault(x => x.Id == model.AssetCategory);
                    model.AssetCode = Assetcategorydetails.CodeFormat;

                }
                if (model.Id == 0) _context.FixedAssetRegisters.Add(model);
                else _context.FixedAssetRegisters.Update(model);

                if (model.DepreciationStartDate == DateTime.Now.Date)
                {
                    var dateAndTime = DateTime.Now;
                    model.DepreciationStartDate = dateAndTime.Date;
                    model.PurchaseDate = dateAndTime.Date;
                }
                else
                {
                    model.DepreciationStartDate = model.DepreciationStartDate;
                }

                if (model.savepopupdata[0] != null)
                {
                    string newstring = String.Join(", ", model.savepopupdata.Select(s => s.ToString()).ToArray());
                    string[] name = newstring.Split(",");
                    var data = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.FixedAssetAccountID).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Name).FirstOrDefault();
                        data[i].AnalysisName = code;

                        _context.ChartOfAccountAnalysisSetting.Update(data[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata1[0] != null)
                {
                    string newstring1 = String.Join(", ", model.savepopupdata1.Select(s => s.ToString()).ToArray());
                    string[] name1 = newstring1.Split(",");
                    var data1 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.PL_DepreciationAccount).ToList();
                    for (int i = 0; i < data1.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name1[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name1[i]).Select(y => y.Name).FirstOrDefault();
                        data1[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data1[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata2[0] != null)
                {
                    string newstring2 = String.Join(", ", model.savepopupdata2.Select(s => s.ToString()).ToArray());
                    string[] name2 = newstring2.Split(",");
                    var data2 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.PS_DepreciationAccount).ToList();
                    for (int i = 0; i < data2.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name2[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name2[i]).Select(y => y.Name).FirstOrDefault();
                        data2[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data2[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata3[0] != null)
                {
                    string newstring3 = String.Join(", ", model.savepopupdata3.Select(s => s.ToString()).ToArray());
                    string[] name3 = newstring3.Split(",");
                    var data3 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.DisposalGainAccount).ToList();
                    for (int i = 0; i < data3.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name3[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name3[i]).Select(y => y.Name).FirstOrDefault();
                        data3[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data3[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata4[0] != null)
                {

                    string newstring4 = String.Join(", ", model.savepopupdata4.Select(s => s.ToString()).ToArray());
                    string[] name4 = newstring4.Split(",");
                    var data4 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.DisposalLossAccount).ToList();
                    for (int i = 0; i < data4.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name4[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name4[i]).Select(y => y.Name).FirstOrDefault();
                        data4[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data4[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata5[0] != null)
                {
                    string newstring5 = String.Join(", ", model.savepopupdata5.Select(s => s.ToString()).ToArray());
                    string[] name5 = newstring5.Split(",");
                    var data5 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.writeOfAccount).ToList();
                    for (int i = 0; i < data5.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name5[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name5[i]).Select(y => y.Name).FirstOrDefault();
                        data5[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data5[i]);
                        _context.SaveChanges();
                    }
                }
                //Save Attachment
                if (model.AttachmentFile != null)
                {
                    FixedAssetRegisterAttachment fixedAssetRegisterAttachment = new FixedAssetRegisterAttachment();

                    foreach (var item in model.AttachmentFile)
                    {
                        var pathandname = Path.Combine("Fixed_AssetAttachments", Guid.NewGuid().ToString() + item.FileName);
                        var path = Path.Combine(_env.WebRootPath, pathandname);
                        using (var stream = new FileStream(path, FileMode.CreateNew))
                        {
                            item.CopyTo(stream);
                        }
                        fixedAssetRegisterAttachment.AttachmentPath = "/" + pathandname;
                        fixedAssetRegisterAttachment.CreatedBy = model.CreatedBy;
                        fixedAssetRegisterAttachment.CreatedDatetime = DateTime.UtcNow;
                        fixedAssetRegisterAttachment.FixedAssetRegisterId = model.Id;
                        _context.FixedAssetRegisterAttachment.Add(fixedAssetRegisterAttachment);
                    }
                }

                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return Ok();
            }
        }



        [HttpPost]
        public JsonResult FillAssetSubCode(int id)
        {

            var data = _context.MasterAssetCategory.Where(x => x.Id == id).Select(y => y.CodeFormat).FirstOrDefault();

            return Json(data);

        }


        [HttpPost]
        public JsonResult CalculateDate(string startdte, string enddate)
        {
            var data3 = Convert.ToDateTime(startdte);
            var data1 = Convert.ToDateTime(enddate);
            var dataa = (data1 - data3).TotalDays;


            return Json(dataa);

        }

        [HttpPost]
        public JsonResult Fillutilizelife(int id)
        {

            var data = _context.MasterAssetCategory.Where(x => x.Id == id).Select(y => y.UtilizeL).FirstOrDefault();

            return Json(data);

        }

        [HttpPost]
        public JsonResult DynamicRowCreation(int rowcount, string asssubcode, string registrationno, string serial, string depart, string loc, string unit, string allocation)
        {

            StringBuilder html = new StringBuilder();

            List<SelectListItem> departmentmaster = new List<SelectListItem>();

            List<SelectListItem> locationmaster = new List<SelectListItem>();


            var department = _context.MasterDepartment.Where(a => a.Status == true).ToList();
            var location = _context.MasterAssetLocation.Where(a => a.Status == true).ToList();


            foreach (var item in department)
            {
                departmentmaster.Add(new SelectListItem { Text = $"{item.Code}-{item.Name}", Value = item.Id.ToString() });
            }

            foreach (var item in location)
            {
                locationmaster.Add(new SelectListItem { Text = $"{item.Code}-{item.Name}", Value = item.Id.ToString() });
            }



            for (int i = 0; i < rowcount; i++)
            {

                html.Append("<tr>");

                #region TD1


                if (rowcount == 1)
                {

                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"AssetSubCode{i}\" id=\"AssetSubCode{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{asssubcode}\" readonly /><span asp-validation-for=\"AssetSubCode\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");

                }
                else
                {
                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"AssetSubCode{i}\" id=\"AssetSubCode{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{asssubcode}-{i + 1}\" readonly /><span asp-validation-for=\"AssetSubCode\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");

                }

                #endregion

                #region TD2
                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"> <input name=\"RegistrationNumber{i}\" id=\"RegistrationNumber{i}\" autocomplete=\"off\" class=\"form-control\" /> <span asp-validation-for=\"RegistrationNumber\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                #region TD3
                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"SerialNumber{i}\" id=\"SerialNumber{i}\" autocomplete=\"off\" class=\"form-control\" /><span asp-validation-for=\"SerialNumber\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                #region TD4
                html.Append("<td class=\"text-center\">");
                html.Append($"<select name=\"Department{i}\" id=\"Department{i}\" class=\"select2 form-control\">");
                foreach (var item in departmentmaster)
                {
                    html.Append($"<option value=\"{item.Value}\" >{item.Text}</option>");
                }
                html.Append("</select>");
                html.Append("</td>");
                #endregion

                #region TD5
                html.Append("<td class=\"text-right\">");
                html.Append("<div class=\"col-sm-12 pd_0\">");
                html.Append($"<select name=\"Location{i}\" id=\"Location{i}\" class=\"select2 form-control\">");
                foreach (var item in locationmaster)
                {
                    html.Append($"<option value=\"{item.Value}\" >{item.Text}</option>");
                }
                html.Append("</select>");
                html.Append("</div");
                html.Append("</td>");
                #endregion

                #region TD6
                html.Append("<td class=\"text-right\">");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"UnitPrice{i}\" id=\"UnitPrice{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{unit}\" readonly value=\"1\" /><span asp-validation-for=\"UnitPrice\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                #region TD7
                html.Append("<td class=\"text-right\">");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"AllocationValue{i}\" id=\"AllocationValue{i}\" autocomplete=\"off\" class=\"form-control\" readonly value=\"{allocation}\"/><span asp-validation-for=\"AllocationValue\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                html.Append("</tr>");
            }



            return Json(html.ToString());

        }




        [HttpPost]
        public JsonResult DynamicRowCreationEdit(int FixedRegisterid, int rowcount, string asssubcode, string registrationno, string serial, string depart, string loc, string unit, string allocation)
        {


            var data = _context.FixedAssetRegisters.Include(a => a.MultiFormData).SingleOrDefault(a => a.Id == FixedRegisterid).MultiFormData.ToList();


            StringBuilder html = new StringBuilder();

            List<SelectListItem> departmentmaster = new List<SelectListItem>();

            List<SelectListItem> locationmaster = new List<SelectListItem>();


            var department = _context.MasterDepartment.Where(a => a.Status == true).ToList();
            var location = _context.MasterAssetLocation.Where(a => a.Status == true).ToList();


            foreach (var item in department)
            {
                departmentmaster.Add(new SelectListItem { Text = $"{item.Code}-{item.Name}", Value = item.Id.ToString() });
            }

            foreach (var item in location)
            {
                locationmaster.Add(new SelectListItem { Text = $"{item.Code}-{item.Name}", Value = item.Id.ToString() });
            }



            for (int i = 0; i < rowcount; i++)
            {

                html.Append("<tr>");

                #region TD1


                if (rowcount == 1)
                {

                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"AssetSubCode{i}\" id=\"AssetSubCode{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{asssubcode}\" readonly /><span asp-validation-for=\"AssetSubCode\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");

                }
                else
                {
                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"AssetSubCode{i}\" id=\"AssetSubCode{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{asssubcode}-{i + 1}\" readonly /><span asp-validation-for=\"AssetSubCode\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");

                }

                #endregion

                #region TD2


                if (data.Count() - 1 >= i)
                {
                    var row = data[i];
                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"> <input name=\"RegistrationNumber{i}\" value=\"{row.registrationno}\" id=\"RegistrationNumber{i}\" autocomplete=\"off\" class=\"form-control\" /> <span asp-validation-for=\"RegistrationNumber\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");
                }
                else
                {
                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"> <input name=\"RegistrationNumber{i}\" id=\"RegistrationNumber{i}\" autocomplete=\"off\" class=\"form-control\" /> <span asp-validation-for=\"RegistrationNumber\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");
                }


                #endregion


                #region TD3



                if (data.Count() - 1 >= i)
                {
                    var row = data[i];
                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"SerialNumber{i}\" id=\"SerialNumber{i}\" value=\"{row.serialno}\" autocomplete=\"off\" class=\"form-control\" /><span asp-validation-for=\"SerialNumber\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");
                }
                else
                {
                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"SerialNumber{i}\" id=\"SerialNumber{i}\" autocomplete=\"off\" class=\"form-control\" /><span asp-validation-for=\"SerialNumber\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");
                }



                #endregion

                #region TD4
                html.Append("<td class=\"text-center\">");
                html.Append($"<select name=\"Department{i}\" id=\"Department{i}\" class=\"select2 form-control\">");
                foreach (var item in departmentmaster)
                {
                    html.Append($"<option value=\"{item.Value}\" >{item.Text}</option>");
                }
                html.Append("</select>");
                html.Append("</td>");
                #endregion

                #region TD5
                html.Append("<td class=\"text-right\">");
                html.Append("<div class=\"col-sm-12 pd_0\">");
                html.Append($"<select name=\"Location{i}\" id=\"Location{i}\" class=\"select2 form-control\">");
                foreach (var item in locationmaster)
                {
                    html.Append($"<option value=\"{item.Value}\" >{item.Text}</option>");
                }
                html.Append("</select>");
                html.Append("</div");
                html.Append("</td>");
                #endregion

                #region TD6
                html.Append("<td class=\"text-right\">");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"UnitPrice{i}\" id=\"UnitPrice{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{unit}\" readonly value=\"1\" /><span asp-validation-for=\"UnitPrice\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                #region TD7
                html.Append("<td class=\"text-right\">");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"AllocationValue{i}\" id=\"AllocationValue{i}\" autocomplete=\"off\" class=\"form-control\" readonly value=\"{allocation}\"/><span asp-validation-for=\"AllocationValue\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                html.Append("</tr>");
            }



            return Json(html.ToString());

        }


        [HttpPost]
        public JsonResult Filldate(int id, string date)
        {

            //var data1 = _context.FixedAssetRegisters.Select(y => y.DepreciationStartDate).FirstOrDefault();

            DateTime datetime = (Convert.ToDateTime(date).Date).AddDays(id);
            var data = datetime.ToString("yyyy-MM-dd");


            return Json(data);

        }

        [HttpPost]
        public JsonResult ShowAnalysisPopup(int Fixedassetid, int disposallossid, int Psaccountid, int Disposalgainid, int Disposalossid, int Writeoffid)
        {
            List<ChartOfAccountAnalysisSetting> chartOfAccountAnalysisSettings = new List<ChartOfAccountAnalysisSetting>();
            List<ChartOfAccountAnalysisSetting> chartOfAccountAnalysisSettings1 = new List<ChartOfAccountAnalysisSetting>();
            List<ChartOfAccountAnalysisSetting> chartOfAccountAnalysisSettings2 = new List<ChartOfAccountAnalysisSetting>();
            List<ChartOfAccountAnalysisSetting> chartOfAccountAnalysisSettings3 = new List<ChartOfAccountAnalysisSetting>();
            List<ChartOfAccountAnalysisSetting> chartOfAccountAnalysisSettings4 = new List<ChartOfAccountAnalysisSetting>();
            List<ChartOfAccountAnalysisSetting> chartOfAccountAnalysisSettings5 = new List<ChartOfAccountAnalysisSetting>();
            List<AnalysisCode> analysisCodes = new List<AnalysisCode>();
            List<AnalysisCode> analysisCodes1 = new List<AnalysisCode>();
            List<AnalysisCode> analysisCodes2 = new List<AnalysisCode>();
            List<AnalysisCode> analysisCodes3 = new List<AnalysisCode>();
            List<AnalysisCode> analysisCodes4 = new List<AnalysisCode>();
            List<AnalysisCode> analysisCodes5 = new List<AnalysisCode>();

            try
            {
                var data = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == Fixedassetid).ToList();
                foreach (var COAitems in data)
                {
                    chartOfAccountAnalysisSettings.Add(COAitems);
                    foreach (var ACitem in _context.AnalysisCode.Where(x => x.AnalysisNumber == COAitems.AnalysisNumber && x.Status == true))
                    {
                        analysisCodes.Add(ACitem);
                        COAitems.AnalysisCode = analysisCodes;
                    }
                }
                var data2 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == disposallossid).ToList();
                foreach (var COAitems in data)
                {
                    chartOfAccountAnalysisSettings1.Add(COAitems);
                    foreach (var ACitem in _context.AnalysisCode.Where(x => x.AnalysisNumber == COAitems.AnalysisNumber && x.Status == true))
                    {
                        analysisCodes1.Add(ACitem);
                        COAitems.AnalysisCode = analysisCodes1;
                    }
                }
                var data3 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == Psaccountid).ToList();
                foreach (var COAitems in data)
                {
                    chartOfAccountAnalysisSettings2.Add(COAitems);
                    foreach (var ACitem in _context.AnalysisCode.Where(x => x.AnalysisNumber == COAitems.AnalysisNumber && x.Status == true))
                    {
                        analysisCodes2.Add(ACitem);
                        COAitems.AnalysisCode = analysisCodes2;
                    }
                }
                var data4 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == Disposalgainid).ToList();
                foreach (var COAitems in data)
                {
                    chartOfAccountAnalysisSettings3.Add(COAitems);
                    foreach (var ACitem in _context.AnalysisCode.Where(x => x.AnalysisNumber == COAitems.AnalysisNumber && x.Status == true))
                    {
                        analysisCodes3.Add(ACitem);
                        COAitems.AnalysisCode = analysisCodes3;
                    }
                }
                var data5 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == Disposalossid).ToList();
                foreach (var COAitems in data)
                {
                    chartOfAccountAnalysisSettings4.Add(COAitems);
                    foreach (var ACitem in _context.AnalysisCode.Where(x => x.AnalysisNumber == COAitems.AnalysisNumber && x.Status == true))
                    {
                        analysisCodes4.Add(ACitem);
                        COAitems.AnalysisCode = analysisCodes4;
                    }
                }
                var data6 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == Writeoffid).ToList();
                foreach (var COAitems in data)
                {
                    chartOfAccountAnalysisSettings5.Add(COAitems);
                    foreach (var ACitem in _context.AnalysisCode.Where(x => x.AnalysisNumber == COAitems.AnalysisNumber && x.Status == true))
                    {
                        analysisCodes5.Add(ACitem);
                        COAitems.AnalysisCode = analysisCodes5;
                    }
                }




                return Json(new { data = data.Select(x => x.AnalysisNumber).ToList(), accouninfo = data2.Select(x => x.AnalysisNumber).ToList(), psaccount = data3.Select(x => x.AnalysisNumber).ToList(), disposalgain = data4.Select(x => x.AnalysisNumber).ToList(), disposalloss = data5.Select(x => x.AnalysisNumber).ToList(), writeoff = data6.Select(x => x.AnalysisNumber).ToList(), popupvalues =  chartOfAccountAnalysisSettings, popupvalues1 = chartOfAccountAnalysisSettings1, popupvalues2 = chartOfAccountAnalysisSettings2, popupvalues3 = chartOfAccountAnalysisSettings3, popupvalues4 = chartOfAccountAnalysisSettings4, popupvalues5 = chartOfAccountAnalysisSettings5 });

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IActionResult Edit(int CreditTermid)
        {
            try
            {

                var data = _context.FixedAssetRegisters.Include(a => a.MultiFormData).FirstOrDefault(a => a.Id == CreditTermid);
                var assettypelist = new List<SelectListItem>

                {
                new SelectListItem {Text = "Depreciation", Value = "1"},
                new SelectListItem {Text = "Non-Depreciation", Value = "0"},
                };
                TempData["utilifelife"] = _context.Setup.Select(x => x.Value).FirstOrDefault();
                TempData["reducingbalance"] = _context.Setup.Select(x => x.Reducing_Balance_UL).FirstOrDefault();
                TempData["DailyUL"] = _context.Setup.Select(x => x.Daily_UL).FirstOrDefault();
                TempData["MonthlyUL"] = _context.Setup.Select(x => x.Monthy_UL).FirstOrDefault();


                data.asset_Category = _context.MasterAssetCategory.Where(a => a.Status == true).ToList();
                data.asset_model = _context.MasterAssetModel.Where(a => a.Status == true).ToList();
                data.asset_brand = _context.MasterAssetBrand.Where(a => a.Status == true).ToList();
                data.asset_type = assettypelist;
                data.supplier = _context.Supplier.Where(a => a.Status == true).ToList();
                data.fixed_asset_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.pl_description_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.bs_description_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.disposal_gain_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.disposal_loss_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.write_off_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.department_master = _context.MasterDepartment.Where(a => a.Status == true).ToList();
                data.asset_location_master = _context.MasterAssetLocation.Where(a => a.Status == true).ToList();




                return View(data);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public JsonResult GetSelected(string Mode, int id)
        {

            try
            {
                var data = _context.FixedAssetRegisters.FirstOrDefault(a=>a.Id==id);
                string selected = "";
                switch (Mode)
                {
                    case "FixedAssetAccountID":
                        if (data.FixedAssetAccountID!=0)
                        {
                            selected = _context.ChartOfAccountAnalysisSetting.FirstOrDefault(x => x.ChartOfAccountId == data.FixedAssetAccountID).AnalysisName;
                            break;
                        }
                        else
                        {
                            break;
                        }
                      
                    case "PL_DepreciationAccount":
                        if (data.PL_DepreciationAccount!=0)
                        {
                            selected = _context.ChartOfAccountAnalysisSetting.FirstOrDefault(x => x.ChartOfAccountId == data.PL_DepreciationAccount).AnalysisName;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    case "PS_DepreciationAccount":
                        if (data.PS_DepreciationAccount!=0)
                        {
                            selected = _context.ChartOfAccountAnalysisSetting.FirstOrDefault(x => x.ChartOfAccountId == data.PS_DepreciationAccount).AnalysisName;
                            break;
                        }
                        else
                        {
                            break;
                        }
                       
                    case "DisposalGainAccount":
                        if (data.DisposalGainAccount!=0)
                        {
                            selected = _context.ChartOfAccountAnalysisSetting.FirstOrDefault(x => x.ChartOfAccountId == data.DisposalGainAccount).AnalysisName;
                            break;
                        }
                        else
                        {
                            break;
                        }
                       
                    case "DisposalLossAccount":
                        if (data.DisposalLossAccount!=0)
                        {
                            selected = _context.ChartOfAccountAnalysisSetting.FirstOrDefault(x => x.ChartOfAccountId == data.DisposalLossAccount).AnalysisName;
                            break;
                        }
                        else
                        {
                            break;
                        }
                     
                    case "writeOfAccount":
                        if (data.writeOfAccount!=0)
                        {
                            selected = _context.ChartOfAccountAnalysisSetting.FirstOrDefault(x => x.ChartOfAccountId == data.writeOfAccount).AnalysisName;
                            break;

                        }
                       else
                        {
                            break;
                        }
                    default:
                        selected = "";
                        break;
                }
                return Json(selected);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public IActionResult Edit(Fixed_Asset_Register model)
        {

            try
            {



                var data = _context.FixedAssetRegisters.Include(a => a.MultiFormData).FirstOrDefault(a => a.Id == model.Id);
                _context.RemoveRange(data.MultiFormData);
                _context.SaveChanges();



                data.AssetCode = model.AssetCode;
                data.AssetName = model.AssetName;
                data.AssetType = model.AssetType;


                data.AssetCategory = model.AssetCategory;
                data.AssetModel = model.AssetModel;
                data.Remarks = model.Remarks;
                data.SupplierID = model.SupplierID;
                data.PurchaseDate = model.PurchaseDate;
                data.InvoiceNumber = model.InvoiceNumber;
                data.TotalUnit = model.TotalUnit;
                data.UnitPrice = model.UnitPrice;
                data.CapitalizeAmount = model.CapitalizeAmount;
                data.DepreciationStartDate = model.DepreciationStartDate;
                data.ResidualAmount = model.ResidualAmount;
                data.NetBookValue = model.NetBookValue;
                data.UtilizeLife = model.UtilizeLife;
                data.DepreciationpercentagePer = model.DepreciationpercentagePer;
                data.DepreciationEndDate = model.DepreciationEndDate;
                data.FixedAssetAccountID = model.FixedAssetAccountID;
                data.PL_DepreciationAccount = model.PL_DepreciationAccount;
                data.PS_DepreciationAccount = model.PS_DepreciationAccount;
                data.DisposalGainAccount = model.DisposalGainAccount;
                data.DisposalLossAccount = model.DisposalLossAccount;
                data.writeOfAccount = model.writeOfAccount;
                data.Adjustment_CapitalizeAmount = model.Adjustment_CapitalizeAmount;
                data.Adjustment_ResidualAmount = model.Adjustment_ResidualAmount;
                data.Adjustment_UtilizeLifeInMonths = model.Adjustment_UtilizeLifeInMonths;
                data.Attachment = model.Attachment;
                data.AssetSubCode = model.AssetSubCode;
                data.RegistrationNumber = model.RegistrationNumber;
                data.SerialNumber = model.SerialNumber;
                data.Department = model.Department;
                data.Location = model.Location;
                data.Asset_UnitPrice = model.Asset_UnitPrice;
                data.AllocationValue = model.AllocationValue;
                data.Status = model.Status;
                data.FixedAssetRegisterAttachment = _context.FixedAssetRegisterAttachment.Where(x => x.FixedAssetRegisterId == data.Id).ToList();
                data.AttchmentPath = model.AttchmentPath;
                data.CreationStatus = model.CreationStatus;

                //Update Attachment
                if (model.AttachmentFile != null)
                {
                    FixedAssetRegisterAttachment fixedAssetRegisterAttachment = new FixedAssetRegisterAttachment();

                    foreach (var item in model.AttachmentFile)
                    {
                        var pathandname = Path.Combine("Fixed_AssetAttachments", Guid.NewGuid().ToString() + item.FileName);
                        var path = Path.Combine(_env.WebRootPath, pathandname);
                        using (var stream = new FileStream(path, FileMode.CreateNew))
                        {
                            item.CopyTo(stream);
                        }
                        fixedAssetRegisterAttachment.AttachmentPath = "/" + pathandname;
                        fixedAssetRegisterAttachment.CreatedBy = model.CreatedBy;
                        fixedAssetRegisterAttachment.CreatedDatetime = DateTime.UtcNow;
                        fixedAssetRegisterAttachment.FixedAssetRegisterId = model.Id;
                        _context.FixedAssetRegisterAttachment.Add(fixedAssetRegisterAttachment);
                    }
                }
                data.ModifiedBy = GetUserId(User);
                data.ModificationTimeStamp = DateTime.Now;
                if (data.CreationStatus == 1) data.AssetCode = "";
                else
                {
                    var Assetcategorydetails = _context.MasterAssetCategory.FirstOrDefault(x => x.Id == data.AssetCategory);
                    data.AssetCode = Assetcategorydetails.CodeFormat;
                }
                data.MultiFormData = model.MultiFormData;

                if (model.savepopupdata[0] != null)
                {
                    string newstring = String.Join(", ", model.savepopupdata.Select(s => s.ToString()).ToArray());
                    string[] name = newstring.Split(",");
                    var dataa = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.FixedAssetAccountID).ToList();
                    for (int i = 0; i < dataa.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Name).FirstOrDefault();
                        dataa[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(dataa[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata1[0] != null)
                {
                    string newstring1 = String.Join(", ", model.savepopupdata1.Select(s => s.ToString()).ToArray());
                    string[] name1 = newstring1.Split(",");
                    var data1 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.PL_DepreciationAccount).ToList();
                    for (int i = 0; i < data1.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name1[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name1[i]).Select(y => y.Name).FirstOrDefault();
                        data1[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data1[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata2[0] != null)
                {
                    string newstring2 = String.Join(", ", model.savepopupdata2.Select(s => s.ToString()).ToArray());
                    string[] name2 = newstring2.Split(",");
                    var data2 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.PS_DepreciationAccount).ToList();
                    for (int i = 0; i < data2.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name2[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name2[i]).Select(y => y.Name).FirstOrDefault();
                        data2[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data2[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata3[0] != null)
                {

                    string newstring3 = String.Join(", ", model.savepopupdata3.Select(s => s.ToString()).ToArray());
                    string[] name3 = newstring3.Split(",");
                    var data3 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.DisposalGainAccount).ToList();
                    for (int i = 0; i < data3.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name3[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name3[i]).Select(y => y.Name).FirstOrDefault();
                        data3[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data3[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata4[0] != null)
                {

                    string newstring4 = String.Join(", ", model.savepopupdata4.Select(s => s.ToString()).ToArray());
                    string[] name4 = newstring4.Split(",");
                    var data4 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.DisposalLossAccount).ToList();
                    for (int i = 0; i < data4.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name4[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name4[i]).Select(y => y.Name).FirstOrDefault();
                        data4[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data4[i]);
                        _context.SaveChanges();
                    }
                }
                if (model.savepopupdata5[0] != null)
                {
                    string newstring5 = String.Join(", ", model.savepopupdata5.Select(s => s.ToString()).ToArray());
                    string[] name5 = newstring5.Split(",");
                    var data5 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == model.writeOfAccount).ToList();
                    for (int i = 0; i < data5.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name5[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name5[i]).Select(y => y.Name).FirstOrDefault();
                        data5[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data5[i]);
                        _context.SaveChanges();
                    }
                }

                _context.FixedAssetRegisters.Update(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #region Search

        [HttpPost]
        public List<Fixed_Asset_Register> SearchFixedAssetRegister(string code, string name, DateTime date, int category, short cstatus, int type)
        {
            try
            {
                Func<Fixed_Asset_Register, bool> none = a => a.CreatedBy != "test";
                Func<Fixed_Asset_Register, bool> namefilter = none;
                Func<Fixed_Asset_Register, bool> codefilter = none;
                Func<Fixed_Asset_Register, bool> datefilter = none;
                Func<Fixed_Asset_Register, bool> categoryfilter = none;
                Func<Fixed_Asset_Register, bool> statusfilter = none;
                Func<Fixed_Asset_Register, bool> typefilter = none;

                if (code != null) codefilter = new Func<Fixed_Asset_Register, bool>(a => a.AssetCode == code);
                if (name != null) namefilter = new Func<Fixed_Asset_Register, bool>(a => a.AssetName.ToLower().Contains(name.ToLower()));

                if (date != DateTime.MinValue) datefilter = new Func<Fixed_Asset_Register, bool>(a => a.PurchaseDate == date);
                if (category != 0) categoryfilter = new Func<Fixed_Asset_Register, bool>(a => a.AssetCategory == category);
                if (cstatus != 0) statusfilter = new Func<Fixed_Asset_Register, bool>(a => a.CreationStatus == cstatus);
                if (type != -1) typefilter = new Func<Fixed_Asset_Register, bool>(a => a.AssetType == type);

                //return _context.FixedAssetRegisters.Where(a => a.CreationStatus != 1).Where(namefilter).Where(codefilter).ToList();
                return _context.FixedAssetRegisters.Where(namefilter).Where(codefilter).Where(datefilter).Where(categoryfilter).Where(statusfilter).Where(typefilter).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region Import

        [HttpPost]
        public IActionResult Import(IFormFile importfilesetting)
        {
            var data = _import.GetFixedAssetRegisterDataFromCSV(importfilesetting);

            //MakeAllData as Post
            data.Data.All(c =>
            {
                c.CreationStatus = 2;
                c.CreatedBy = GetUserId(User);
                c.CreationTimeStamp = DateTime.Now;
                return true;
            });

            _context.FixedAssetRegisters.AddRange(data.Data);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));

        }

        #endregion

        public IActionResult Delete(int id)
        {
            try
            {
                //var data = _context.FixedAssetRegisters.Include(a => a.MultiFormData).FirstOrDefault(a => a.Id == model.Id);

                var ch = _context.AllModuleFormSub.Where(y => y.Fixed_Asset_RegisterId == id).ToList();
                var data = _context.FixedAssetRegisters.Find(id);
                _context.FixedAssetRegisters.Remove(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public IActionResult Assetrefrence(int CreditTermid)
        {
            try
            {
                var data = _context.FixedAssetRegisters.FirstOrDefault(a => a.Id == CreditTermid);
                data.StaticList = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Test1", Value = "1"},
                    new SelectListItem {Text = "Test2", Value = "2"},
                    new SelectListItem {Text = "Test3", Value = "3"},
                    new SelectListItem {Text = "Test4", Value = "4"}
                };
                var assettypelist = new List<SelectListItem>

                {
                new SelectListItem {Text = "Depreciation", Value = "1"},
                new SelectListItem {Text = "Non-Depreciation", Value = "0"},
                };


                data.asset_Category = _context.MasterAssetCategory.Where(a => a.Status == true).ToList();
                data.asset_model = _context.MasterAssetModel.Where(a => a.Status == true).ToList();
                data.asset_brand = _context.MasterAssetBrand.Where(a => a.Status == true).ToList();
                data.asset_type = assettypelist;
                data.supplier = _context.Supplier.Where(a => a.Status == true).ToList();
                data.fixed_asset_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.pl_description_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.bs_description_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.disposal_gain_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.disposal_loss_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.write_off_account = _context.ChartOfAccounts.Where(a => a.Status == true).ToList();
                data.department_master = _context.MasterDepartment.Where(a => a.Status == true).ToList();
                data.asset_location_master = _context.MasterAssetLocation.Where(a => a.Status == true).ToList();


                return View("Create", data);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost]
        public IActionResult SaveFixAssetData(string[] name, int chartaccount, int pldescription, int psdescription, int disposalgain, int disposlloss, int writeaccount)
        {

            try
            {
                if (chartaccount != 0)
                {
                    var data = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == chartaccount).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Name).FirstOrDefault();
                        data[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data[i]);
                        _context.SaveChanges();
                    }
                }
                else if (pldescription != 0)
                {
                    var data = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == pldescription).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Name).FirstOrDefault();
                        data[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data[i]);
                        _context.SaveChanges();
                    }
                }
                else if (psdescription != 0)
                {
                    var data = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == psdescription).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Name).FirstOrDefault();
                        data[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data[i]);
                        _context.SaveChanges();
                    }

                }
                else if (disposalgain != 0)
                {
                    var data = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == disposalgain).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Name).FirstOrDefault();
                        data[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data[i]);
                        _context.SaveChanges();
                    }
                }
                else if (disposlloss != 0)
                {
                    var data = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == disposlloss).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Name).FirstOrDefault();
                        data[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data[i]);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    var data = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == writeaccount).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        var code = _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Code).FirstOrDefault() + "-" +
                         _context.AnalysisCode.Where(x => x.Id.ToString() == name[i]).Select(y => y.Name).FirstOrDefault();
                        data[i].AnalysisName = code;
                        _context.ChartOfAccountAnalysisSetting.Update(data[i]);
                        _context.SaveChanges();
                    }
                }



            }
            catch (Exception)
            {

                return Ok();
            }
            return View("");
        }


        public async Task<IActionResult> Copy(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            /*TODO How to get the Data From Screen*/

            //Get Data


            Fixed_Asset_Register FixedAssetRegister = await _context.FixedAssetRegisters.FindAsync(id);


            var AssetType = new List<SelectListItem>
            {
                new SelectListItem {Text = "- ALL -", Value = "-1"},
                new SelectListItem {Text = "Depreciation", Value = "1"},
                new SelectListItem {Text = "Non-Depreciation", Value = "0"},
            };
            ViewData["AssetType"] = new SelectList(AssetType, "Value", "Text",Convert.ToString(FixedAssetRegister.AssetType));

            //var companies = await _context.Companies.FindAsync(id);
            //var countryList = _context.tblCountry.OrderBy(x => x.CountryId).ToList();
            //ViewData["countryList"] = new SelectList(countryList, "CountryName", "CountryName");

            //var stateList = _context.tblState.Where(x => x.CountryId == 1).ToList();
            //ViewData["stateList"] = new SelectList(stateList, "StateName", "StateName");

            //if (companies == null)
            //{
            //    return NotFound();
            //}
            //return View(companies);


            if (FixedAssetRegister == null)
            {
                return NotFound();
            }
            return View(FixedAssetRegister);
        }

    }
}
