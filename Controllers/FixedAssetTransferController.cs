using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixedAssets.Models;
using FixedModules.Data;
using FixedAssets.ViewModels;
using System.Text;
using System.Security.Claims;
using FixedModules.Models;
using Newtonsoft.Json;

namespace FixedAssets.Controllers
{
    public class FixedAssetTransferController : Controller
    {
        private readonly MyDbContext _context;
        private string GetUserId(ClaimsPrincipal user) => ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public FixedAssetTransferController(MyDbContext context)
        {
            _context = context;
        }

        // GET: FixedAssetTransfer
        public async Task<IActionResult> Index()
        {
            FixedModuleViewModel _view = new FixedModuleViewModel();
            _view.fixedAssetTransfer = await _context.FixedAssetTransfer.ToListAsync();

            return View(_view);
        }

        // GET: FixedAssetTransfer/Create
        public IActionResult Create()
        {
            var data = new FixedAssetTransfer();
            var dateAndTime = DateTime.Now;
            data.TransDate = dateAndTime.Date;

            data.asset_subCodes = _context.MasterAssetCategory.Where(a => a.Status == true).ToList();
            data.asset_subCodesFa = _context.FixedAssetRegisters.Where(a => a.Status == true).ToList();
            data.masterDepartment = _context.MasterDepartment.Where(a => a.Status == true).ToList();
            data.masterAssetLocation = _context.MasterAssetLocation.Where(a => a.Status == true).ToList();


            return View(data);
        }

        // POST: FixedAssetTransfer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FixedAssetTransfer model)
        {
            try
            {
                model.FixedAssetTransferSubForm = JsonConvert.DeserializeObject<List<FixedAssetTransferSubForm>>(model.MultiValuesTransfer);
                if (model.TransDate == DateTime.Now.Date)
                {
                    var dateAndTime = DateTime.Now;
                    model.TransDate = dateAndTime.Date;


                }
                else
                {
                    model.TransDate = model.TransDate;
                }
                model.CreatedBy = GetUserId(User);

                model.CreatedDatetime = DateTime.Now;
                var savedrecord = _context.FixedAssetTransfer.Add(model);
                _context.SaveChanges();

                if (savedrecord.Entity.CreationStatus != "Draft")
                {
                    savedrecord.Entity.DocumentCode = $"TAA - {savedrecord.Entity.Id}";
                }

                _context.FixedAssetTransfer.Update(savedrecord.Entity);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: FixedAssetTransfer/Edit/5
        public IActionResult Edit(int? id)
        {
            var data = _context.FixedAssetTransfer.Where(a => a.Id == id).FirstOrDefault();
            data.asset_subCodes = _context.MasterAssetCategory.Where(a => a.Status == true).ToList();
            data.asset_subCodesFa = _context.FixedAssetRegisters.Where(a => a.Status == true).ToList();
            data.masterDepartment = _context.MasterDepartment.Where(a => a.Status == true).ToList();
            data.masterAssetLocation = _context.MasterAssetLocation.Where(a => a.Status == true).ToList();

            return View(data);
        }

        // POST: FixedAssetTransfer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FixedAssetTransfer model)
        {
            try
            {
                if (model.TransDate == DateTime.Now.Date)
                {
                    var dateAndTime = DateTime.Now;
                    model.TransDate = dateAndTime.Date;


                }
                else
                {
                    model.TransDate = model.TransDate;
                }
                if (model.CreationStatus != "DRAFT")
                {
                    model.DocumentCode = string.Format("TAA" + "-" + @"{0}", Guid.NewGuid());
                }

                model.ModifiedBy = string.Format(@"{0}", Guid.NewGuid());
                model.ModifiedDatetime = DateTime.Now;
                _context.FixedAssetTransfer.Update(model);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: FixedAssetTransfer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetTransfer = await _context.FixedAssetTransfer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetTransfer == null)
            {
                return NotFound();
            }

            return View(fixedAssetTransfer);
        }
        [HttpPost]
        public List<FixedAssetTransfer> SearchFixedAssetTransfer(DateTime EndDat, DateTime StartDat, string Multivalue, string Rangevalue1, string Rangevalue2, string Stats)
        {
            try
            {


                var date = EndDat.ToString() == "01-01-0001 12:00:00 AM";
                var startdat = StartDat.ToString() == "01-01-0001 12:00:00 AM";

                var list = Multivalue == null ? null : Multivalue.Split(",");
                Func<FixedAssetTransfer, bool> none = a => a.CreatedBy != "test";
                Func<FixedAssetTransfer, bool> strtdate = none;
                //Func<FixedAssetMDepreciation, bool> enddate = none;
                Func<FixedAssetTransfer, bool> Checkmultivalue = none;
                Func<FixedAssetTransfer, bool> Rangeval1 = none;
                //Func<FixedAssetMDepreciation, bool> Rangeval2 = none;             
                Func<FixedAssetTransfer, bool> statusfilter = none;

                if ((StartDat != null) && (!date && !startdat)) strtdate = new Func<FixedAssetTransfer, bool>(y => y.TransDate >= StartDat.AddDays(-1) && y.TransDate <= EndDat.AddDays(1));
                //if (EndDat != null) enddate = new Func<FixedAssetMDepreciation, bool>(a => a.TransDate == EndDat);
                if ((Multivalue != null && Multivalue != "") && Multivalue != "undefined") Checkmultivalue = new Func<FixedAssetTransfer, bool>(a => list.Contains(a.Id.ToString()));
                if ((Rangevalue1 != null && Rangevalue1 != "") && Rangevalue1 != "undefined") Rangeval1 = new Func<FixedAssetTransfer, bool>(y => y.Id >= Convert.ToInt32(Rangevalue1) && y.Id <= Convert.ToInt32(Rangevalue2));
                //if ((Rangevalue2 != null && Rangevalue2 != "") && Rangevalue2 != "undefined") Rangeval2 = new Func<FixedAssetMDepreciation, bool>(a => a.Id.ToString() == Rangevalue2);
                if ((Stats != null && Stats != "") && (Stats != "undefined" && Stats != "ALL")) statusfilter = new Func<FixedAssetTransfer, bool>(a => a.CreationStatus == Stats);

                var res = _context.FixedAssetTransfer.Where(strtdate).Where(Checkmultivalue).Where(Rangeval1).Where(statusfilter).ToList();

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
            public string Department { get; set; }
            public string Location { get; set; }
        }

        [HttpPost]
        public JsonResult GetAssetSubCode(DateTime transdate, string CatMultivalue, string CatRangevalue1, string CatRangevalue2, string AssetMultivalue, string AssetRangevalue1, string AssetRangevalue2)
        {
            var catlist = CatMultivalue == null ? null : CatMultivalue.Split(",");
            var assetlist = AssetMultivalue == null ? null : AssetMultivalue.Split(",");
            Func<Fixed_Asset_Register, bool> none = a => a.CreatedBy != "test";
            //Func<FixedAssetMDepreciation, bool> enddate = none;
            Func<Fixed_Asset_Register, bool> CatCheckmultivalue = none;
            Func<Fixed_Asset_Register, bool> AssetCheckmultivalue = none;
            Func<Fixed_Asset_Register, bool> CatRangeval1 = none;
            Func<Fixed_Asset_Register, bool> AssetRangeval1 = none;

            if ((CatMultivalue != null && CatMultivalue != "") && CatMultivalue != "undefined") CatCheckmultivalue = new Func<Fixed_Asset_Register, bool>(a => catlist.Contains(a.Id.ToString()));
            if ((AssetMultivalue != null && AssetMultivalue != "") && AssetMultivalue != "undefined") AssetCheckmultivalue = new Func<Fixed_Asset_Register, bool>(a => assetlist.Contains(a.Id.ToString()));
            if ((CatRangevalue1 != null && CatRangevalue1 != "") && CatRangevalue1 != "undefined") CatRangeval1 = new Func<Fixed_Asset_Register, bool>(y => y.Id >= Convert.ToInt32(CatRangevalue1) && y.Id <= Convert.ToInt32(CatRangevalue2));
            if ((AssetRangevalue1 != null && AssetRangevalue1 != "") && AssetRangevalue1 != "undefined") AssetRangeval1 = new Func<Fixed_Asset_Register, bool>(y => y.Id >= Convert.ToInt32(AssetRangevalue1) && y.Id <= Convert.ToInt32(AssetRangevalue2));


            var res = _context.FixedAssetRegisters.Where(CatCheckmultivalue).Where(AssetCheckmultivalue).Where(CatRangeval1).Where(AssetRangeval1).Select(x => x.Id).ToList();


            StringBuilder html = new StringBuilder();

            var id = 0;
            List<string> fcuk = new List<string>();
            List<string> psname1 = new List<string>();
            List<string> plname1 = new List<string>();
            int days = DateTime.DaysInMonth(transdate.Year, transdate.Month);
            DateTime firstDayOfMonth = new DateTime(transdate.Year, transdate.Month, 1);
            System.TimeSpan diff = transdate.Subtract(firstDayOfMonth);
            System.TimeSpan diff1 = transdate - firstDayOfMonth;
            string diff2 = (transdate - firstDayOfMonth).TotalDays.ToString();
            int num = int.Parse(diff2);


            List<MyClass> data = new List<MyClass>();


            List<AllModuleFormSub> dat = new List<AllModuleFormSub>();

            foreach (var item in res)
            {
                dat.AddRange(_context.AllModuleFormSub.Where(y => y.Fixed_Asset_RegisterId == item).ToList());
            }

            //MyClass ch = new MyClass();
            foreach (var item in dat)
            {
                MyClass ch = new MyClass()
                {
                    AssetSubCode = item.asssubcode,
                    Department = item.departmnt,
                    Location = item.location,
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
                html.Append($"<div class=\"col-sm-12 pd_0\"> <input name=\"Department{i}\" id=\"Department{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.Department}\" readonly /> <span asp-validation-for=\"DepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                #region TD3

                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"Location{i}\" id=\"Location{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.Location}\" readonly /><span asp-validation-for=\"BSDepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("<div class=\"showpopup6 input-group-append\">");

                html.Append("<span class=\"input-group-text fs-xl lcss\">");
                html.Append("</td>");
                #endregion
                html.Append("</tr>");

            }
            return Json(new { data = html.ToString(), rowcount = data.Count() });
        }

        [HttpPost]
        public JsonResult GetAssetSubCodeEdit(DateTime transdate, string CatMultivalue, string CatRangevalue1, string CatRangevalue2, string AssetMultivalue, string AssetRangevalue1, string AssetRangevalue2)
        {
            var catlist = CatMultivalue == null ? null : CatMultivalue.Split(",");
            var assetlist = AssetMultivalue == null ? null : AssetMultivalue.Split(",");
            Func<Fixed_Asset_Register, bool> none = a => a.CreatedBy != "test";
            //Func<FixedAssetMDepreciation, bool> enddate = none;
            Func<Fixed_Asset_Register, bool> CatCheckmultivalue = none;
            Func<Fixed_Asset_Register, bool> AssetCheckmultivalue = none;
            Func<Fixed_Asset_Register, bool> CatRangeval1 = none;
            Func<Fixed_Asset_Register, bool> AssetRangeval1 = none;

            if ((CatMultivalue != null && CatMultivalue != "") && CatMultivalue != "undefined") CatCheckmultivalue = new Func<Fixed_Asset_Register, bool>(a => catlist.Contains(a.Id.ToString()));
            if ((AssetMultivalue != null && AssetMultivalue != "") && AssetMultivalue != "undefined") AssetCheckmultivalue = new Func<Fixed_Asset_Register, bool>(a => assetlist.Contains(a.Id.ToString()));
            if ((CatRangevalue1 != null && CatRangevalue1 != "") && CatRangevalue1 != "undefined") CatRangeval1 = new Func<Fixed_Asset_Register, bool>(y => y.Id >= Convert.ToInt32(CatRangevalue1) && y.Id <= Convert.ToInt32(CatRangevalue2));
            if ((AssetRangevalue1 != null && AssetRangevalue1 != "") && AssetRangevalue1 != "undefined") AssetRangeval1 = new Func<Fixed_Asset_Register, bool>(y => y.Id >= Convert.ToInt32(AssetRangevalue1) && y.Id <= Convert.ToInt32(AssetRangevalue2));


            var res = _context.FixedAssetRegisters.Where(CatCheckmultivalue).Where(AssetCheckmultivalue).Where(CatRangeval1).Where(AssetRangeval1).Select(x => x.Id).ToList();


            StringBuilder html = new StringBuilder();

            var id = 0;
            List<string> fcuk = new List<string>();
            List<string> psname1 = new List<string>();
            List<string> plname1 = new List<string>();
            int days = DateTime.DaysInMonth(transdate.Year, transdate.Month);
            DateTime firstDayOfMonth = new DateTime(transdate.Year, transdate.Month, 1);
            System.TimeSpan diff = transdate.Subtract(firstDayOfMonth);
            System.TimeSpan diff1 = transdate - firstDayOfMonth;
            string diff2 = (transdate - firstDayOfMonth).TotalDays.ToString();
            int num = int.Parse(diff2);


            List<MyClass> data = new List<MyClass>();


            List<AllModuleFormSub> dat = new List<AllModuleFormSub>();

            foreach (var item in res)
            {
                dat.AddRange(_context.AllModuleFormSub.Where(y => y.Fixed_Asset_RegisterId == item).ToList());
            }

            //MyClass ch = new MyClass();
            foreach (var item in dat)
            {


                MyClass ch = new MyClass()
                {
                    AssetSubCode = item.asssubcode,
                    Department = item.departmnt,
                    Location = item.location,
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
                html.Append($"<div class=\"col-sm-12 pd_0\"> <input name=\"Department{i}\" id=\"Department{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.Department}\" readonly /> <span asp-validation-for=\"DepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("</td>");
                #endregion

                #region TD3

                html.Append("<td>");
                html.Append($"<div class=\"col-sm-12 pd_0\"><input name=\"Location{i}\" id=\"Location{i}\" autocomplete=\"off\" class=\"form-control\" value=\"{item.Location}\" readonly /><span asp-validation-for=\"BSDepreciationAmount\" class=\"text-danger\"></span></div>");
                html.Append("<div class=\"showpopup6 input-group-append\">");

                html.Append("<span class=\"input-group-text fs-xl lcss\">");
                html.Append("</td>");

                #endregion
                html.Append("</tr>");



            }
            return Json(new { data = html.ToString(), rowcount = data.Count() });
        }
    



        // POST: FixedAssetTransfer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixedAssetTransfer = await _context.FixedAssetTransfer.FindAsync(id);
            _context.FixedAssetTransfer.Remove(fixedAssetTransfer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixedAssetTransferExists(int id)
        {
            return _context.FixedAssetTransfer.Any(e => e.Id == id);
        }
    }
}