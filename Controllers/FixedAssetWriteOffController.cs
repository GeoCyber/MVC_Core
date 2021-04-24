using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixedAssets.Models;
using FixedModules.Data;
using FixedModules.Models;
using FixedAssets.ViewModels;
using System.Text;
using Newtonsoft.Json;

namespace FixedModules.Controllers
{
    public class FixedAssetWriteOffController : Controller
    {
        private readonly MyDbContext _context;

        public FixedAssetWriteOffController(MyDbContext context)
        {
            _context = context;
        }

        // GET: FixedAssetWriteOffs
        public async Task<IActionResult> Index()
        {
            FixedModuleViewModel _view = new FixedModuleViewModel();
            _view.fixedAssetWriteOff = await _context.FixedAssetWriteOff.ToListAsync();

            return View(_view);
        }

        // GET: FixedAssetWriteOffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetWriteOff = await _context.FixedAssetWriteOff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetWriteOff == null)
            {
                return NotFound();
            }

            return View(fixedAssetWriteOff);
        }

        // GET: FixedAssetWriteOffs/Create
        public IActionResult Create()
        {
            var data = new FixedAssetWriteOff();
            var dateAndTime = DateTime.Now;
            data.TransDate = dateAndTime.Date;
            //int days = DateTime.DaysInMonth(data.TransDate.Year, data.TransDate.Month);
            //DateTime firstDayOfMonth = new DateTime(data.TransDate.Year, data.TransDate.Month, 1);
            //System.TimeSpan diff = data.TransDate.Subtract(firstDayOfMonth);
            //System.TimeSpan diff1 = data.TransDate - firstDayOfMonth;

            //string diff2 = (data.TransDate - firstDayOfMonth).TotalDays.ToString();
            //int num = int.Parse(diff2);

            data.asset_subCodes = _context.MasterAssetCategory.Where(a => a.Status == true).ToList();
            data.asset_subCodesFa = _context.FixedAssetRegisters.Where(a => a.Status == true).ToList();
          
            return View(data);
        }

        // POST: FixedAssetWriteOffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FixedAssetWriteOff model)
        {
            try
            {
                model.FixedAssetWriteOffSubForm = JsonConvert.DeserializeObject<List<FixedAssetWriteOffSubForm>>(model.MultiValuesWriteOff);

                if (model.TransDate == DateTime.Now.Date)
                {
                    var dateAndTime = DateTime.Now;
                    model.TransDate = dateAndTime.Date;


                }
                else
                {
                    model.TransDate = model.TransDate;
                }
                var savedrecord = _context.FixedAssetWriteOff.Add(model);
                _context.SaveChanges();
                if (savedrecord.Entity.CreationStatus != "Draft")
                {
                    savedrecord.Entity.DocumentCode = $"WAA - {savedrecord.Entity.Id}";
                }

                model.CreatedBy = string.Format(@"{0}", Guid.NewGuid());
                model.CreatedDatetime = DateTime.Now;
                _context.FixedAssetWriteOff.Update(savedrecord.Entity);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: FixedAssetWriteOffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var data = _context.FixedAssetWriteOff.Where(a => a.Id == id).FirstOrDefault();
            data.asset_subCodes = _context.MasterAssetCategory.Where(a => a.Status == true).ToList();
            data.asset_subCodesFa = _context.FixedAssetRegisters.Where(a => a.Status == true).ToList();

            return View(data);
        }

        // POST: FixedAssetWriteOffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FixedAssetWriteOff model)
        {
            model.FixedAssetWriteOffSubForm = JsonConvert.DeserializeObject<List<FixedAssetWriteOffSubForm>>(model.MultiValuesWriteOff);

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
                    model.DocumentCode = string.Format("WAA" + "-" + @"{0}", Guid.NewGuid());
                }

                model.CreatedBy = string.Format(@"{0}", Guid.NewGuid());
                model.CreatedDatetime = DateTime.Now;
                _context.FixedAssetWriteOff.Update(model);
                _context.SaveChanges();

            }
            catch(Exception ex)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: FixedAssetWriteOffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedAssetWriteOff = await _context.FixedAssetWriteOff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixedAssetWriteOff == null)
            {
                return NotFound();
            }

            return View(fixedAssetWriteOff);
        }

        // POST: FixedAssetWriteOffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int[] ids)
        {
            Fixed_Asset_Register fixed_Asset_Register = new Fixed_Asset_Register();

            foreach (int Id in ids)
            {
                var records = _context.FixedAssetRegisters.Where(x => x.Id == Id).SingleOrDefault();
                _context.FixedAssetRegisters.Remove(records);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public class MyClass
        {
            public int Id { get; set; }
            public string AssetSubCode { get; set; }
            public string SerialNumber { get; set; }
            public string Remark { get; set; }
            public decimal UnitAmount { get; set; }
            public decimal AccumulatedDepreciation { get; set; }
            public decimal NetBookValue { get; set; }

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
           

            var res = _context.FixedAssetRegisters.ToList();


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
                dat.AddRange(_context.AllModuleFormSub.Where(y => y.Fixed_Asset_RegisterId == item.Id).ToList());
            }

            //MyClass ch = new MyClass();
            foreach (var item in dat)
            {

                var gh = _context.FixedAssetMDepreciation.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.DepreciationAmount).FirstOrDefault();
                //var h = _context.ChartOfAccounts.Where(x => x.Id == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(x => x.Name + "-" + x.Code).FirstOrDefault();
                var up = Convert.ToDecimal(item.unitprc);
                MyClass ch = new MyClass()
                {
                    AssetSubCode = item.asssubcode,
                    SerialNumber = item.serialno,
                    Remark = "",
                    UnitAmount = up,
                    AccumulatedDepreciation = (gh / days) * num,
                    //NetBookValue = UnitAmount - AccumulatedDepreciation;
                    

                };
                ch.NetBookValue = ch.UnitAmount - ch.AccumulatedDepreciation;
                data.Add(ch);

            }

            var ch1 = data;
            foreach (var (item, i) in data.Select((v, i) => (v, i)))
            {


                html.Append($"<div class=\"link\" onclick=\"hidedata{i}()\"><span>#{i}</span><i class=\"fa fa-chevron-down\"></i></div>");
                html.Append($"<ul id=\"link{i}\" class=\"submenu\">");
               
                #region TD1

                {

                    html.Append($"<div class=\"row\">");
                    html.Append($"<div class=\"col-sm-4\">");
                    html.Append($"<div class=\"form-group\">");
                    html.Append($"<label class=\"col-sm-4 control-label col-form-label\">Asset SubCode<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                    html.Append($"<input name=\"AssetSubCode{i}\" id=\"AssetSubCode{i}\" value=\"{item.AssetSubCode}\" asp-for=\"AssetSubCode\" autocomplete=\"off\" class=\"form-control\" readonly />");
                    html.Append("<span asp-validation-for=\"AssetSubCode\" class=\"text-danger\"></span></div></div>");

                }

                #endregion

                #region TD2

                html.Append($"<div class=\"col-sm-4\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-4 control-label col-form-label\">Serial Number<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<input name=\"SerialNUmber{i}\" id=\"SerialNumber{i}\" asp-for=\"{item.SerialNumber}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                html.Append("<span asp-validation-for=\"Serial Number\" class=\"text-danger\"></span></div></div>");
                #endregion

                html.Append($"<div class=\"col-sm-4\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-4 control-label col-form-label\">Remark<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<input name=\"Remark{i}\" id=\"Remark{i}\" asp-for=\"{item.Remark}\" autocomplete=\"off\" class=\"form-control\" />");
                html.Append("<span asp-validation-for=\"Remark\" class=\"text-danger\"></span></div></div></div>");

                html.Append($"<div class=\"row\">");
                html.Append($"<div class=\"col-sm-3\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-12 control-label col-form-label\">Unit Amount<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<div class=\"col-sm-12\">");
                html.Append($"<input name=\"UnitAmount{i}\" id=\"UnitAmount{i}\" asp-for=\"{item.UnitAmount}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                html.Append("<span asp-validation-for=\"UnitAmount\" class=\"text-danger\"></span></div></div></div>");

                html.Append($"<div class=\"col-sm-3\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-12 control-label col-form-label\">Accumulated Depreciation<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<div class=\"col-sm-12\">");
                html.Append($"<inputname=\"AccumulatedDepreciation{i}\" id=\"AccumulatedDepreciation{i}\" asp-for=\"{item.AccumulatedDepreciation}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                html.Append("<span asp-validation-for=\"AccumulatedDepreciation\" class=\"text-danger\"></span></div></div></div>");

                html.Append($"<div class=\"col-sm-3\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-12 control-label col-form-label\">Net Book Value<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<div class=\"col-sm-12\">");
                html.Append($"<input name=\"NetBookValue{i}\" id=\"NetBookValue{i}\" asp-for=\"{item.NetBookValue}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                html.Append("<span asp-validation-for=\"NetBookValue\" class=\"text-danger\"></span></div></div></div></div>");

                html.Append($"<div class=\"form-check row\">");
                html.Append($"<input class=\"form-check-input\" type=\"checkbox\" asp-for=\"Id\" class=\"chkCheckBoxId\" value=\"{item.Id}\" id=\"check{i}\">");
                html.Append($"<label class=\"form- check-label\" for=\"check{i}\">Checked for delete selected</label>");
                html.Append($"<button class=\"btn btn-danger btn-sm rounded-0\" type=\"button\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Delete Selected\"><i class=\"fa fa - trash\"></i></button></div>");
          
                html.Append("</ul>");



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

                var gh = _context.FixedAssetMDepreciation.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.DepreciationAmount).FirstOrDefault();
                var h = _context.FixedAssetWriteOffSubForm.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.Remark).FirstOrDefault();
                //var h = _context.ChartOfAccounts.Where(x => x.Id == _context.FixedAssetRegisters.Where(t => t.Id == item.Fixed_Asset_RegisterId).Select(x => x.PL_DepreciationAccount).FirstOrDefault()).Select(x => x.Name + "-" + x.Code).FirstOrDefault();
                MyClass ch = new MyClass()
                {
                    AssetSubCode = item.asssubcode,
                    SerialNumber = item.serialno,
                    Remark = h,
                    UnitAmount = Convert.ToDecimal(item.unitprc),
                    AccumulatedDepreciation = (gh / days) * num,
                    //NetBookValue = UnitAmount - AccumulatedDepreciation;


                };
                ch.NetBookValue = ch.UnitAmount - ch.AccumulatedDepreciation;
                data.Add(ch);

            }

            var ch1 = data;
            foreach (var (item, i) in data.Select((v, i) => (v, i)))
            {


                html.Append($"<div class=\"link{i}\"><span>1</span><i class=\"fa fa-chevron-down\">#{i}</i></div>");
                html.Append($"<ul id=\"link{i}\" class=\"submenu\">");

                #region TD1

                {

                    html.Append($"<div class=\"row\">");
                    html.Append($"<div class=\"col-sm-4\">");
                    html.Append($"<div class=\"form-group\">");
                    html.Append($"<label class=\"col-sm-4 control-label col-form-label\">Asset SubCode)<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                    html.Append($"<input name=\"AssetSubCode{i}\" id=\"AssetSubCode{i}\" asp-for=\"{item.AssetSubCode}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                    html.Append("<span asp-validation-for=\"AssetSubCode\" class=\"text-danger\"></span></div></div>");

                }

                #endregion

                #region TD2

                html.Append($"<div class=\"col-sm-4\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-4 control-label col-form-label\">Serial Number)<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<input name=\"SerialNUmber{i}\" id=\"SerialNumber{i}\" asp-for=\"{item.SerialNumber}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                html.Append("<span asp-validation-for=\"Serial Number\" class=\"text-danger\"></span></div></div>");
                #endregion

                html.Append($"<div class=\"col-sm-4\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-4 control-label col-form-label\">Remark)<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<input name=\"Remark{i}\" id=\"Remark{i}\" asp-for=\"{item.Remark}\" autocomplete=\"off\" class=\"form-control\" />");
                html.Append("<span asp-validation-for=\"Remark\" class=\"text-danger\"></span></div></div></div>");

                html.Append($"<div class=\"row\">");
                html.Append($"<div class=\"col-sm-3\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-12 control-label col-form-label\">Unit Amount)<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<div class=\"col-sm-12\">");
                html.Append($"<input name=\"UnitAmount{i}\" id=\"UnitAmount{i}\" asp-for=\"{item.UnitAmount}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                html.Append("<span asp-validation-for=\"UnitAmount\" class=\"text-danger\"></span></div></div></div>");

                html.Append($"<div class=\"col-sm-3\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-12 control-label col-form-label\">Accumulated Depreciation)<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<div class=\"col-sm-12\">");
                html.Append($"<inputname=\"AccumulatedDepreciation{i}\" id=\"AccumulatedDepreciation{i}\" asp-for=\"{item.AccumulatedDepreciation}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                html.Append("<span asp-validation-for=\"AccumulatedDepreciation\" class=\"text-danger\"></span></div></div></div>");

                html.Append($"<div class=\"col-sm-3\">");
                html.Append($"<div class=\"form-group\">");
                html.Append($"<label class=\"col-sm-12 control-label col-form-label\">Net Book Value)<span class=\"value-mandatory\"><i class=\"mdi mdi-value\"></i></span></label>");
                html.Append($"<div class=\"col-sm-12\">");
                html.Append($"<input name=\"NetBookValue{i}\" id=\"NetBookValue{i}\" asp-for=\"{item.NetBookValue}\" autocomplete=\"off\" class=\"form-control\" readonly />");
                html.Append("<span asp-validation-for=\"NetBookValue\" class=\"text-danger\"></span></div></div></div></div>");

                html.Append($"<div class=\"form-check row\">");
                html.Append($"<input class=\"form-check-input\" type=\"checkbox\" value=\"\" id=\"check{i}\">");
                html.Append($"<label class=\"form- check-label\" for=\"check{i}\">Checked for delete selected</label>");
                html.Append($"<button class=\"btn btn-danger btn-sm rounded-0\" type=\"button\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Delete Selected\"><i class=\"fa fa - trash\"></i></button></div>");

                html.Append("</ul>");



            }
            return Json(new { data = html.ToString(), rowcount = data.Count() });
        }

        [HttpPost]
        public List<FixedAssetWriteOff> SearchFixedAssetWriteOff(DateTime EndDat, DateTime StartDat, string Multivalue, string Rangevalue1, string Rangevalue2, string Stats)
        {
            try
            {


                var date = EndDat.ToString() == "01-01-0001 12:00:00 AM";
                var startdat = StartDat.ToString() == "01-01-0001 12:00:00 AM";

                var list = Multivalue == null ? null : Multivalue.Split(",");
                Func<FixedAssetWriteOff, bool> none = a => a.CreatedBy != "test";
                Func<FixedAssetWriteOff, bool> strtdate = none;
                //Func<FixedAssetMDepreciation, bool> enddate = none;
                Func<FixedAssetWriteOff, bool> Checkmultivalue = none;
                Func<FixedAssetWriteOff, bool> Rangeval1 = none;
                //Func<FixedAssetMDepreciation, bool> Rangeval2 = none;             
                Func<FixedAssetWriteOff, bool> statusfilter = none;

                if ((StartDat != null) && (!date && !startdat)) strtdate = new Func<FixedAssetWriteOff, bool>(y => y.TransDate >= StartDat.AddDays(-1) && y.TransDate <= EndDat.AddDays(1));
                //if (EndDat != null) enddate = new Func<FixedAssetMDepreciation, bool>(a => a.TransDate == EndDat);
                if ((Multivalue != null && Multivalue != "") && Multivalue != "undefined") Checkmultivalue = new Func<FixedAssetWriteOff, bool>(a => list.Contains(a.Id.ToString()));
                if ((Rangevalue1 != null && Rangevalue1 != "") && Rangevalue1 != "undefined") Rangeval1 = new Func<FixedAssetWriteOff, bool>(y => y.Id >= Convert.ToInt32(Rangevalue1) && y.Id <= Convert.ToInt32(Rangevalue2));
                //if ((Rangevalue2 != null && Rangevalue2 != "") && Rangevalue2 != "undefined") Rangeval2 = new Func<FixedAssetMDepreciation, bool>(a => a.Id.ToString() == Rangevalue2);
                if ((Stats != null && Stats != "") && (Stats != "undefined" && Stats != "ALL")) statusfilter = new Func<FixedAssetWriteOff, bool>(a => a.CreationStatus == Stats);

                var res = _context.FixedAssetWriteOff.Where(strtdate).Where(Checkmultivalue).Where(Rangeval1).Where(statusfilter).ToList();

                return res;
            }
            catch (Exception er)
            {

                throw er;
            }
        }
        private bool FixedAssetWriteOffExists(int id)
        {
            return _context.FixedAssetWriteOff.Any(e => e.Id == id);
        }
    }
}
