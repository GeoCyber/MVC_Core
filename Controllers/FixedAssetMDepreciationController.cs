using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FixedAssets.Models;
using FixedModules.Data;
using FixedModules.Models;
using FixedModules.Services;
using FixedModules.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FixedModules.Controllers
{
    public class FixedAssetMDepreciationController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IImportService _import;
        public FixedAssetMDepreciationController(MyDbContext context, IWebHostEnvironment env, IImportService import)
        {
            _context = context;
            _env = env;
            _import = import;
        }
        private string GetUserId(ClaimsPrincipal user) => ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public IActionResult Index()
        {
            FixedAssetMDepreciation listdata = new FixedAssetMDepreciation();
            listdata._FixedAssetMDepreciations = _context.FixedAssetMDepreciation.ToList();
            listdata._documentnumber = _context.FixedAssetMDepreciation.ToList();
            return View(listdata);
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            var data = new FixedAssetMDepreciation();
            var psid = _context.FixedAssetRegisters.Select(a => a.PS_DepreciationAccount).FirstOrDefault().ToString();
            var plid = _context.FixedAssetRegisters.Select(a => a.PL_DepreciationAccount).FirstOrDefault().ToString();
            var PSaccount = _context.ChartOfAccounts.FirstOrDefault(a => a.Id == Convert.ToInt32(psid));
            var PLAccount = _context.ChartOfAccounts.FirstOrDefault(a => a.Id == Convert.ToInt32(plid));

            if(PSaccount != null && PLAccount != null)
            {
                var Psdata = PSaccount.Code + "-" + PSaccount.Name;
                var Pldata = PLAccount.Code + "-" + PLAccount.Name;
                data.BSDepreciationAmount = Psdata;
                data.PLDepreciationAmount = Pldata;
            }
            data.TransDate = DateTime.Now;

            return View(data);
        }


        [HttpPost]

        public ActionResult Create(FixedAssetMDepreciation model)
        {
            try
            {
                model.MontlyDepreMultiFormData = JsonConvert.DeserializeObject<List<MonthlyDepreciationSubForm>>(model.MultiValuesDepreciation);
                if (model.TransDate == DateTime.Now.Date)
                {
                    var dateAndTime = DateTime.Now;
                    model.TransDate = dateAndTime.Date;
                    model.Year = dateAndTime.Year;
                    model.Month = dateAndTime.Month;

                }
                else
                {
                    model.TransDate = model.TransDate;
                }


                model.CreatedBy = GetUserId(User);

                model.Status = model.CreationStatus;
                var savedrecord = _context.FixedAssetMDepreciation.Add(model);
                _context.SaveChanges();

                if (savedrecord.Entity.CreationStatus != "Draft")
                {
                    savedrecord.Entity.DocumentCode = $"DAA - {savedrecord.Entity.Id}";
                }

                _context.FixedAssetMDepreciation.Update(savedrecord.Entity);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _context.FixedAssetMDepreciation.Where(a => a.Id == id).FirstOrDefault();


            var psid = _context.FixedAssetRegisters.FirstOrDefault().PS_DepreciationAccount.ToString();
            var plid = _context.FixedAssetRegisters.FirstOrDefault().PL_DepreciationAccount.ToString();
            var PSaccount = _context.ChartOfAccounts.FirstOrDefault(a => a.Id == Convert.ToInt32(psid));
            var PLAccount = _context.ChartOfAccounts.FirstOrDefault(a => a.Id == Convert.ToInt32(plid));
            var Psdata = PSaccount.Code + "-" + PSaccount.Name;
            var Pldata = PLAccount.Code + "-" + PLAccount.Name;
            data.BSDepreciationAmount = Psdata;
            data.PLDepreciationAmount = Pldata;



            return View(data);
        }


        [HttpPost]

        public ActionResult Edit(FixedAssetMDepreciation model)
        {
            try
            {
                model.MontlyDepreMultiFormData = JsonConvert.DeserializeObject<List<MonthlyDepreciationSubForm>>(model.MultiValuesDepreciation);
                if (model.TransDate == DateTime.Now.Date)
                {
                    var dateAndTime = DateTime.Now;
                    model.TransDate = dateAndTime.Date;
                    model.Year = dateAndTime.Year;
                    model.Month = dateAndTime.Month;

                }
                else
                {
                    model.TransDate = model.TransDate;
                }
                if (model.CreationStatus != "Draft")
                {
                    model.DocumentCode = string.Format("DAA" + "-" + @"{0}", Guid.NewGuid());
                }


                _context.FixedAssetMDepreciation.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public List<FixedAssetMDepreciation> SearchFixedAssetMDepreciation(DateTime EndDat, DateTime StartDat, string Multivalue, string Rangevalue1, string Rangevalue2, string Stats)
        {
            try
            {


                var date = EndDat.ToString() == "01-01-0001 12:00:00 AM";
                var startdat = StartDat.ToString() == "01-01-0001 12:00:00 AM";

                var list = Multivalue == null ? null : Multivalue.Split(",");
                Func<FixedAssetMDepreciation, bool> none = a => a.CreatedBy != "test";
                Func<FixedAssetMDepreciation, bool> strtdate = none;
                //Func<FixedAssetMDepreciation, bool> enddate = none;
                Func<FixedAssetMDepreciation, bool> Checkmultivalue = none;
                Func<FixedAssetMDepreciation, bool> Rangeval1 = none;
                //Func<FixedAssetMDepreciation, bool> Rangeval2 = none;             
                Func<FixedAssetMDepreciation, bool> statusfilter = none;

                if ((StartDat != null) && (!date && !startdat)) strtdate = new Func<FixedAssetMDepreciation, bool>(y => y.TransDate >= StartDat.AddDays(-1) && y.TransDate <= EndDat.AddDays(1));
                //if (EndDat != null) enddate = new Func<FixedAssetMDepreciation, bool>(a => a.TransDate == EndDat);
                if ((Multivalue != null && Multivalue != "") && Multivalue != "undefined") Checkmultivalue = new Func<FixedAssetMDepreciation, bool>(a => list.Contains(a.Id.ToString()));
                if ((Rangevalue1 != null && Rangevalue1 != "") && Rangevalue1 != "undefined") Rangeval1 = new Func<FixedAssetMDepreciation, bool>(y => y.Id >= Convert.ToInt32(Rangevalue1) && y.Id <= Convert.ToInt32(Rangevalue2));
                //if ((Rangevalue2 != null && Rangevalue2 != "") && Rangevalue2 != "undefined") Rangeval2 = new Func<FixedAssetMDepreciation, bool>(a => a.Id.ToString() == Rangevalue2);
                if ((Stats != null && Stats != "") && (Stats != "undefined" && Stats != "ALL")) statusfilter = new Func<FixedAssetMDepreciation, bool>(a => a.Status == Stats);

                var res = _context.FixedAssetMDepreciation.Where(strtdate).Where(Checkmultivalue).Where(Rangeval1).Where(statusfilter).ToList();

                return res;
            }
            catch (Exception er)
            {

                throw er;
            }
        }

        public class MyClass
        {
            public string AssetSubCode { get; set; }
            public string DepreciationAmount { get; set; }
            public string PS_DepreciationAccount { get; set; }
            public string PL_DepreciationAccount { get; set; }

            public string PSAnaylsisName { get; set; }
            public string PLnaylsisName { get; set; }
            public string Depreciationpercentage { get; set; }
            public List<string> ListOfdata { get; set; }
            public List<string> ListOfdata1 { get; set; }


        }

        [HttpPost]
        public JsonResult GetAssetSubCode(int year, int month)
        {
            StringBuilder html = new StringBuilder();
            var fixedaseetid = _context.FixedAssetRegisters.Where(x => x.DepreciationStartDate.Year == year && x.DepreciationStartDate.Month == month).Select(a => a.Id).ToList();
            var id = 0;
            List<string> fcuk = new List<string>();
            List<string> psname1 = new List<string>();
            List<string> plname1 = new List<string>();


            List<MyClass> data = new List<MyClass>();


            List<AllModuleFormSub> dat = new List<AllModuleFormSub>();

            foreach (var item in fixedaseetid)
            {
                dat.AddRange(_context.AllModuleFormSub.Where(y => y.Fixed_Asset_RegisterId == item).ToList());
            }

            //MyClass ch = new MyClass();
            foreach (var item in dat)
            {

                var gh = _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault();
                var h = _context.ChartOfAccounts.Where(x => x.Id == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(x => x.Name + "-" + x.Code).FirstOrDefault();
                MyClass ch = new MyClass()
                {
                    AssetSubCode = item.asssubcode,
                    DepreciationAmount = "",
                    PL_DepreciationAccount = _context.ChartOfAccounts.Where(x => x.Id == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(x => x.Name + "-" + x.Code).FirstOrDefault(),
                    PS_DepreciationAccount = _context.ChartOfAccounts.Where(x => x.Id == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PS_DepreciationAccount).FirstOrDefault()).Select(x => x.Name + "-" + x.Code).FirstOrDefault(),
                    PLnaylsisName = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(y => y.AnalysisName).FirstOrDefault(),
                    PSAnaylsisName = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PS_DepreciationAccount).FirstOrDefault()).Select(y => y.AnalysisName).FirstOrDefault(),
                    ListOfdata = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PS_DepreciationAccount).FirstOrDefault()).Select(y => y.AnalysisName).ToList(),
                    ListOfdata1 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(y => y.AnalysisName).ToList(),
                    Depreciationpercentage = _context.FixedAssetRegisters.Where(x => x.Id == item.Fixed_Asset_RegisterId).Select(y => y.DepreciationpercentagePer).FirstOrDefault()
                };
                data.Add(ch);

            }

            var ch1 = data;
            foreach (var (item, i) in data.Select((v, i) => (v, i)))
            {


                html.Append("<tr>");

                #region TD1

                {
                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"AssetSubCode{i}\" id=\"AssetSubCode{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.AssetSubCode}\" readonly /><span asp-validation-for=\"AssetSubCode\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");

                }

                #endregion

                #region TD2

                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"> <input name=\"DepreciationAmount{i}\" id=\"DepreciationAmount{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.Depreciationpercentage}\" readonly /> <span asp-validation-for=\"DepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                #region TD3

                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"BSDepreciationAmount{i}\" id=\"BSDepreciationAmount{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.PS_DepreciationAccount}\" readonly /><span asp-validation-for=\"BSDepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("<div class=\"showpopup6 input-group-append\">");

                html.Append("<span class=\"input-group-text fs-xl lcss\">");


                StringBuilder popupdata = new StringBuilder();
                foreach (var rec in item.ListOfdata.Select((v, index) => (v, index)))
                {
                    popupdata.Append(rec.index == 0 ? rec.v : $",{rec.v}");
                }


                html.Append($"<i class=\"fa fa-lightbulb-o\" onclick=\"BindPopup('{popupdata.ToString()}')\" ></i>");
                html.Append("</span>");
                html.Append("</div>");

                html.Append("</td>");
                #endregion


                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"PLDepreciationAmount{i}\" id=\"PLDepreciationAmount{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.PL_DepreciationAccount}\" readonly /><span asp-validation-for=\"PLDepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("<div class=\"showpopup6 input-group-append\">");

                html.Append("<span class=\"input-group-text fs-xl lcss\">");




                StringBuilder popupdata1 = new StringBuilder();
                foreach (var rec in item.ListOfdata1.Select((v, index) => (v, index)))
                {
                    popupdata1.Append(rec.index == 0 ? rec.v : $",{rec.v}");
                }
                html.Append($"<i class=\"fa fa-lightbulb-o\" onclick=\"BindPopup('{popupdata1.ToString()}')\" ></i>");
                html.Append("</span>");
                html.Append("</div>");

                html.Append("</td>");




                html.Append("</tr>");



            }
            return Json(new { data = html.ToString(), rowcount = data.Count() });
        }

        [HttpPost]
        public JsonResult GetAssetSubCodeEdit(int year, int month)
        {
            StringBuilder html = new StringBuilder();
            var fixedaseetid = _context.FixedAssetRegisters.Where(x => x.DepreciationStartDate.Year == year && x.DepreciationStartDate.Month == month).Select(a => a.Id).ToList();
            var id = 0;
            List<string> fcuk = new List<string>();
            List<string> psname1 = new List<string>();
            List<string> plname1 = new List<string>();


            List<MyClass> data = new List<MyClass>();


            List<AllModuleFormSub> dat = new List<AllModuleFormSub>();

            foreach (var item in fixedaseetid)
            {
                dat.AddRange(_context.AllModuleFormSub.Where(y => y.Fixed_Asset_RegisterId == item).ToList());
            }

            //MyClass ch = new MyClass();
            foreach (var item in dat)
            {

                var gh = _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault();
                var h = _context.ChartOfAccounts.Where(x => x.Id == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(x => x.Name + "-" + x.Code).FirstOrDefault();
                MyClass ch = new MyClass()
                {
                    AssetSubCode = item.asssubcode,
                    DepreciationAmount = "",
                    PL_DepreciationAccount = _context.ChartOfAccounts.Where(x => x.Id == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(x => x.Name + "-" + x.Code).FirstOrDefault(),
                    PS_DepreciationAccount = _context.ChartOfAccounts.Where(x => x.Id == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PS_DepreciationAccount).FirstOrDefault()).Select(x => x.Name + "-" + x.Code).FirstOrDefault(),
                    PLnaylsisName = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(y => y.AnalysisName).FirstOrDefault(),
                    PSAnaylsisName = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PS_DepreciationAccount).FirstOrDefault()).Select(y => y.AnalysisName).FirstOrDefault(),
                    ListOfdata = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PS_DepreciationAccount).FirstOrDefault()).Select(y => y.AnalysisName).ToList(),
                    ListOfdata1 = _context.ChartOfAccountAnalysisSetting.Where(x => x.ChartOfAccountId == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(y => y.AnalysisName).ToList(),
                    Depreciationpercentage = _context.FixedAssetRegisters.Where(x => x.Id == item.Fixed_Asset_RegisterId).Select(y => y.DepreciationpercentagePer).FirstOrDefault()
                };
                data.Add(ch);

            }

            var ch1 = data;
            foreach (var (item, i) in data.Select((v, i) => (v, i)))
            {


                html.Append("<tr>");

                #region TD1

                {
                    html.Append("<td>");
                    html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"AssetSubCode{i}\" id=\"AssetSubCode{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.AssetSubCode}\" readonly /><span asp-validation-for=\"AssetSubCode\" class=\"text-danger\"></span></div>");
                    html.Append("</td>");

                }

                #endregion

                #region TD2

                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"> <input name=\"DepreciationAmount{i}\" id=\"DepreciationAmount{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.Depreciationpercentage}\" readonly /> <span asp-validation-for=\"DepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                #region TD3

                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"BSDepreciationAmount{i}\" id=\"BSDepreciationAmount{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.PS_DepreciationAccount}\" readonly /><span asp-validation-for=\"BSDepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("<div class=\"showpopup6 input-group-append\">");

                html.Append("<span class=\"input-group-text fs-xl lcss\">");


                StringBuilder popupdata = new StringBuilder();
                foreach (var rec in item.ListOfdata.Select((v, index) => (v, index)))
                {
                    popupdata.Append(rec.index == 0 ? rec.v : $",{rec.v}");
                }


                html.Append($"<i class=\"fa fa-lightbulb-o\" onclick=\"BindPopup('{popupdata.ToString()}')\" ></i>");
                html.Append("</span>");
                html.Append("</div>");

                html.Append("</td>");
                #endregion


                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"PLDepreciationAmount{i}\" id=\"PLDepreciationAmount{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.PL_DepreciationAccount}\" readonly /><span asp-validation-for=\"PLDepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("<div class=\"showpopup6 input-group-append\">");

                html.Append("<span class=\"input-group-text fs-xl lcss\">");




                StringBuilder popupdata1 = new StringBuilder();
                foreach (var rec in item.ListOfdata1.Select((v, index) => (v, index)))
                {
                    popupdata1.Append(rec.index == 0 ? rec.v : $",{rec.v}");
                }
                html.Append($"<i class=\"fa fa-lightbulb-o\" onclick=\"BindPopup1('{popupdata1.ToString()}')\" ></i>");
                html.Append("</span>");
                html.Append("</div>");

                html.Append("</td>");




                html.Append("</tr>");



            }
            return Json(new { data = html.ToString(), rowcount = data.Count() });
        }


    }

}