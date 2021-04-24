using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixedModules.Data;
using FixedModules.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Globalization;
using ObjectsComparer;
using Newtonsoft.Json;
using FixedModules.Controllers.Modules;

namespace FixedModules.Controllers
{
    public class AuditTrailController : Controller
    {
        private readonly MyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public AuditTrailController(MyDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        // GET: AuditTrail
        public IActionResult Index()
        {

            var userList = _context.ApplicationUser.ToList();
            ViewData["userList"] = new SelectList(userList, "Id", "Email");

            //var audits = _context.AuditTrail.ToList();


            return View();
        }

        public async Task<IActionResult> Search(string module, string user, string dateFrom)
        {
            List<AuditTrail> res = new List<AuditTrail>();
            var userList1 = _context.ApplicationUser.ToList();
            ViewData["userList"] = new SelectList(userList1, "Id", "Email");
            module = string.IsNullOrEmpty(module) ? string.Empty : module.Trim().ToLower();
            user = string.IsNullOrEmpty(user) ? string.Empty : user.Trim().ToLower();
            dateFrom = string.IsNullOrEmpty(dateFrom) ? string.Empty : dateFrom.Trim().ToLower();

            string start = dateFrom.Substring(0, dateFrom.IndexOf('-'));
            DateTime sDate = DateTime.ParseExact(start.Trim(), @"MM/dd/yyyy", CultureInfo.InvariantCulture);
            string startDate = sDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string end = dateFrom.Substring(dateFrom.LastIndexOf('-') + 1);
            DateTime eDate = DateTime.ParseExact(end.Trim(), @"MM/dd/yyyy", CultureInfo.InvariantCulture);
            string endDate = eDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            try
            {
                DateTime startdate = Convert.ToDateTime(startDate);
                DateTime enddate = Convert.ToDateTime(endDate);
                if (!module.Equals("-1"))
                {
                    res = await _context.AuditTrail.Where(a => a.UserId == user && a.ActionModuleId.ToLower() == module.ToLower() && a.CreatedDatetime >= startdate.AddDays(-1) &&
                    a.CreatedDatetime <= enddate.AddDays(1)).ToListAsync();
                }
                else
                {
                    res = await _context.AuditTrail.Where(a => a.UserId == user && a.CreatedDatetime >= startdate.AddDays(-1) &&
                        a.CreatedDatetime <= enddate.AddDays(1)).ToListAsync();
                }
                foreach (var item in res)
                {
                    ModulesFunction.ToModel(item);
                }
            }


            catch (Exception ex)
            {
                return null;
            }
            return PartialView("SearchPartial",res);
        }



        [HttpPost]
        public async Task<FileResult> Export()
        {
            string[] names = typeof(MasterAssetModel).GetProperties().Select(property => property.Name).ToArray();

            string[] columnNames = new string[] { "Id", "OldData", "NewData", "ActionModule", "ActionType", "CompanyId", "ObjectId", "CreatedOnUtc", "UserId" };

            MasterAssetModel entities = new MasterAssetModel();

            var auditTrails = await _context.AuditTrail.ToListAsync();

            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                //Add the Header row for CSV file.
                csv += columnName + ',';
            }

            //Add new line.
            csv += "\r\n";

            foreach (var at in auditTrails)
            {
                //Add the Data rows.
                csv += at.Id.ToString().Replace(",", ";") + ',';
                csv += at.OldData.Replace(",", ";") + ',';
                csv += at.NewData.Replace(",", ";") + ',';
                csv += at.ActionModuleId.Replace(",", ";") + ',';
                csv += at.ActionTypeId.Replace(",", ";") + ',';
                csv += at.CompanyId.ToString().Replace(",", ";") + ',';
                csv += at.ObjectId.ToString().Replace(",", ";") + ',';
                csv += at.CreatedDatetime.ToString().Replace(",", ";") + ',';
                csv += at.UserId.Replace(",", ";") + ',';

                //Add new line.
                csv += "\r\n";
            }

            var file = "AuditTrail.csv";
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", file);
        }

        // GET: AuditTrail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuditTrail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuditTrail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}