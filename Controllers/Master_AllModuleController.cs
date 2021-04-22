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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FixedModules.Controllers
{
    public class Master_AllModuleController : Controller
    {

        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IImportService _import;
        public Master_AllModuleController(MyDbContext context, IWebHostEnvironment env, IImportService import)
        {
            _context = context;
            _env = env;
            _import = import;
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

            //data.code_format = _context.MasterAssetCategory.Select(x => x.CodeFormat).ToList();

            //data.utilize_life = _context.Setup.Select(x => x.Value).ToList();

            //data.code_format = _context.MasterAssetCategory.ToList();
            //var data4 = data.code_format.Select(x => x.CodeFormat);


            //data.asset_Category = _context.MasterAssetCategory.ToList();
            //data.asset_model = _context.MasterAssetModel.ToList();
            //data.asset_brand = _context.MasterAssetBrand.ToList();
            //data.supplier = _context.Supplier.ToList();

            //data.fixed_asset_account = _context.ChartOfAccounts.ToList();
            //data.pl_description_account = _context.ChartOfAccounts.ToList();
            //data.bs_description_account = _context.ChartOfAccounts.ToList();
            //data.disposal_gain_account = _context.ChartOfAccounts.ToList();
            //data.disposal_loss_account = _context.ChartOfAccounts.ToList();
            //data.write_off_account = _context.ChartOfAccounts.ToList();
            //data.department_master = _context.MasterDepartment.ToList();
            //data.asset_location_master = _context.MasterAssetLocation.ToList();

            data.asset_type = assettypelist;
            var dateAndTime = DateTime.Now;
            data.DepreciationStartDate = dateAndTime.Date;

            data.PurchaseDate = dateAndTime.Date;
            data.StaticList = listCategories;

            return View(data);
        }

        [HttpPost]
        public IActionResult Create(Fixed_Asset_Register model)
        {

            try
            {

                model.MultiFormData = JsonConvert.DeserializeObject<List<AllModuleFormSub>>(model.MultiValuesForm);
                //Save Attachment
                if (model.AttachmentFile != null)
                {
                    var pathandname = Path.Combine("Fixed_AssetAttachments", Guid.NewGuid().ToString() + model.AttachmentFile.FileName);
                    var path = Path.Combine(_env.WebRootPath, pathandname);
                    using (var stream = new FileStream(path, FileMode.CreateNew))
                    {
                        model.AttachmentFile.CopyTo(stream);
                    }
                    model.AttchmentPath = "/" + pathandname;
                }

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
            var data = (data1 - data3).TotalDays;


            return Json(data);

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
        public JsonResult DynamicRowCreationEdit(int FixedRegisterid,int rowcount, string asssubcode, string registrationno, string serial, string depart, string loc, string unit, string allocation)
        {


            var data = _context.FixedAssetRegisters.Include(a => a.MultiFormData).SingleOrDefault(a=>a.Id==FixedRegisterid).MultiFormData.ToList();


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
                else {
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


        public IActionResult Edit(int CreditTermid)
        {
            try
            {
                var data = _context.FixedAssetRegisters.Include(a=>a.MultiFormData).FirstOrDefault(a => a.Id == CreditTermid);
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



        [HttpPost]
        public IActionResult Edit(Fixed_Asset_Register model)
        {

            try
            {
                var data = _context.FixedAssetRegisters.Include(a => a.MultiFormData).FirstOrDefault(a => a.Id == model.Id);
                _context.RemoveRange(data.MultiFormData);
                _context.SaveChanges();

                //Update Attachment
                model.MultiFormData = JsonConvert.DeserializeObject<List<AllModuleFormSub>>(model.MultiValuesForm);
                if (model.AttachmentFile != null)
                {
                    var pathandname = Path.Combine("Fixed_AssetAttachments", Guid.NewGuid().ToString() + model.AttachmentFile.FileName);
                    var path = Path.Combine(_env.WebRootPath, pathandname);
                    using (var stream = new FileStream(path, FileMode.CreateNew))
                    {
                        model.AttachmentFile.CopyTo(stream);
                    }
                    model.AttchmentPath = "/" + pathandname;
                }



                model.ModifiedBy = GetUserId(User);
                model.ModificationTimeStamp = DateTime.Now;

                if (model.CreationStatus == 1) model.AssetCode = "";
                else
                {
                    var Assetcategorydetails = _context.MasterAssetCategory.FirstOrDefault(x => x.Id == model.AssetCategory);
                    model.AssetCode = Assetcategorydetails.CodeFormat;
                }

                //model._FixedAssetList = _context.FixedAssetRegisters.ToList();
                //_context.FixedAssetRegisters.Update(model);
                //_context.SaveChanges();


                var local = _context.Set<Fixed_Asset_Register>().Local.FirstOrDefault(entry => entry.Id.Equals(model.Id));

                // check if local is not null 
                if (local != null)
                {
                    // detach
                    _context.Entry(local).State = EntityState.Detached;
                }
                // set Modified flag in your entrysss
                _context.Entry(model).State = EntityState.Modified;
                _context.FixedAssetRegisters.Update(model);
                // save 
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Search

        [HttpPost]
        public List<Fixed_Asset_Register> SearchFixedAssetRegister(string code, string name, DateTime date, int category, bool status, int type)
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

                if (date != null) datefilter = new Func<Fixed_Asset_Register, bool>(a => a.PurchaseDate == date);
                if (category != 0) datefilter = new Func<Fixed_Asset_Register, bool>(a => a.AssetCategory == category);
                if (status) datefilter = new Func<Fixed_Asset_Register, bool>(a => a.Status == status);
                if (type != 0) datefilter = new Func<Fixed_Asset_Register, bool>(a => a.AssetType == type);


                return _context.FixedAssetRegisters.Where(a => a.CreationStatus != 1).Where(namefilter).Where(codefilter).ToList();
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
                var data = _context.FixedAssetRegisters.Find(id);
                _context.Remove(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
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



                //data.asset_Category = _context.MasterAssetCategory.ToList();
                //data.asset_model = _context.MasterAssetModel.ToList();
                //data.asset_brand = _context.MasterAssetBrand.ToList();
                //data.asset_type = assettypelist;
                //data.supplier = _context.Supplier.ToList();
                //data.fixed_asset_account = _context.ChartOfAccounts.ToList();
                //data.pl_description_account = _context.ChartOfAccounts.ToList();
                //data.bs_description_account = _context.ChartOfAccounts.ToList();
                //data.disposal_gain_account = _context.ChartOfAccounts.ToList();
                //data.disposal_loss_account = _context.ChartOfAccounts.ToList();
                //data.write_off_account = _context.ChartOfAccounts.ToList();
                //data.department_master = _context.MasterDepartment.ToList();
                //data.asset_location_master = _context.MasterAssetLocation.ToList();
                return View(data);
            }
            catch (Exception)
            {

                throw;
            }

        }




    }
}
