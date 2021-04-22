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
using Microsoft.AspNetCore.Identity;
using FixedModules.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace FixedModules.Controllers
{
    public class SupplierController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        public SupplierController(MyDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, IDataAccessService dataAccessService)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _dataAccessService = dataAccessService;
        }

        // GET: Supplier
        public async Task<IActionResult> Index(string error = null)
        {
            var items = await _dataAccessService.GetMenuItemsAsync(HttpContext.User);
            var TempError = TempData["mesage"];
            var controller = items.Where(x => x.ControllerName == ControllerContext.ActionDescriptor.ControllerName).ToList();
            var COAData = _context.ChartOfAccounts;
            var TaxData = _context.TaxCode;
            var SupplierData = _context.Supplier;

            var COAList = _context.ChartOfAccounts.Select(p=>new SelectListItem { Text=p.Name,Value=p.Id.ToString()}).ToList();
             ViewData["COAList"] = COAList;

            if (controller != null)
            {
                var SlSupplier = SupplierData
                    .Join(
                            COAData,
                            supplier => Convert.ToInt32(supplier.ChartOfAccount),
                            COA => COA.Id,
                            (supplier, COA) => new { COAList = COA, SUPList = supplier })
                            .Select(m => new SupplierAdd
                            {
                                Id = m.SUPList.Id,
                                Code = m.SUPList.Code,
                                Name = m.SUPList.Name,
                                ChartOfAccount = m.COAList.Name,
                                Remark = m.SUPList.Remark,
                                RegistrationNumber = m.SUPList.RegistrationNumber,
                                ContactPerson = m.SUPList.ContactPerson,
                                Address1 = m.SUPList.Address1,
                                Address2 = m.SUPList.Address2,
                                City = m.SUPList.City,
                                PostCode = m.SUPList.PostCode,
                                Country = m.SUPList.Country,
                                State = m.SUPList.State,
                                PhoneNumber = m.SUPList.PhoneNumber,
                                Fax = m.SUPList.Fax,
                                Email = m.SUPList.Email,
                                Website = m.SUPList.Website,
                                TaxCode = m.SUPList.TaxCode,
                                TaxNumber = m.SUPList.TaxNumber,
                                TaxExpiryDate = m.SUPList.TaxExpiryDate,
                                Status = m.SUPList.Status,
                                CompanyId = m.SUPList.CompanyId,
                                IsUsed = m.SUPList.IsUsed,
                                CreatedBy = m.SUPList.CreatedBy,
                                CreatedDatetime = m.SUPList.CreatedDatetime,
                                ModifiedBy = m.SUPList.ModifiedBy,
                                ModifiedDatetime = m.SUPList.ModifiedDatetime,
                                StatusId = controller.Select(x => 5).ToList()
                            }).OrderBy(m => m.CreatedDatetime).ToList(); // selection

                ViewBag.error = string.Empty;
                if (error != null)
                {
                    ViewBag.error = error;
                }

                if (TempError != null)
                {
                    ViewBag.error = TempError;
                }

                return View(SlSupplier);
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

            return View(await _context.Supplier.OrderBy(m => m.CreatedDatetime).ToListAsync());

            //return View(await _context.Supplier.ToListAsync());
        }

        public List<Supplier> Search(string code, string name, string coa, string active)
        {
            var COAData = _context.ChartOfAccounts;
            List<Supplier> res = null;
            code = string.IsNullOrEmpty(code) ? string.Empty : code.Trim().ToLower();
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                res = _context.Supplier.ToList().OrderBy(m => m.Id).ToList();

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

                if (!string.IsNullOrEmpty(coa))
                {
                    coa = coa.Trim();
                    res = res.Where(m => m.ChartOfAccount.ToLower().Equals(coa)).ToList();
                }

                if (!active.Equals("-1"))
                {
                    bool isActive = active.Equals("1") ? true : false;
                    res = res.Where(m => m.Status.Equals(isActive)).ToList();
                }

                res = res
                        .Join(
                            COAData,
                            supplier => Convert.ToInt32(supplier.ChartOfAccount),
                            COA => COA.Id,
                            (supplier, COA) => new { COAList = COA, SUPList = supplier })
                            .Select(m => new Supplier
                            {
                                Id = m.SUPList.Id,
                                Code = m.SUPList.Code,
                                Name = m.SUPList.Name,
                                ChartOfAccount = m.COAList.Name,
                                Remark = m.SUPList.Remark,
                                RegistrationNumber = m.SUPList.RegistrationNumber,
                                ContactPerson = m.SUPList.ContactPerson,
                                Address1 = m.SUPList.Address1,
                                Address2 = m.SUPList.Address2,
                                City = m.SUPList.City,
                                PostCode = m.SUPList.PostCode,
                                Country = m.SUPList.Country,
                                State = m.SUPList.State,
                                PhoneNumber = m.SUPList.PhoneNumber,
                                Fax = m.SUPList.Fax,
                                Email = m.SUPList.Email,
                                Website = m.SUPList.Website,
                                TaxCode = m.SUPList.TaxCode,
                                TaxNumber = m.SUPList.TaxNumber,
                                TaxExpiryDate = m.SUPList.TaxExpiryDate,
                                Status = m.SUPList.Status,
                                CompanyId = m.SUPList.CompanyId,
                                IsUsed = m.SUPList.IsUsed,
                                CreatedBy = m.SUPList.CreatedBy,
                                CreatedDatetime = m.SUPList.CreatedDatetime,
                                ModifiedBy = m.SUPList.ModifiedBy,
                                ModifiedDatetime = m.SUPList.ModifiedDatetime
                            }).OrderBy(m => m.CreatedDatetime).ToList(); 
            }
            catch (Exception ex)
            {
                return null;
            }

            return res;
        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            var countryList =  _context.tblCountry.OrderBy(x => x.CountryId).ToList();
            ViewData["countryList"] = new SelectList(countryList, "CountryId", "CountryName");

            var stateList = _context.tblState.Where(x => x.CountryId == 1).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateId", "StateName");

            var COAList = _context.ChartOfAccounts.ToList();
            ViewData["COAList"] = new SelectList(COAList, "Id", "Name");

            var TaxList = _context.TaxCode.ToList();
            ViewData["TaxList"] = new SelectList(TaxList, "Id", "Code");

            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("Id,Group,Code,Name,Status,RegistrationNumber,GST,TinNo,SST,Address,City,PostCode,Country,State,Email,PhoneNumber,Fax,CreatedBy,CreatedDatetime,ModifiedBy,ModifeidDatetime")]
        public async Task<IActionResult> Create([FromForm] Supplier supplier)
        {
            //if (ModelState.IsValid)
            //{

            supplier.CreatedBy = "Kelvin";
            supplier.CreatedDatetime = DateTime.UtcNow;
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(supplier);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier.FindAsync(id);

            var countryList = _context.tblCountry.ToList();//.OrderBy(x => x.CountryName == supplier.Country).ToList();
            ViewData["countryList"] = new SelectList(countryList, "CountryId", "CountryName");

            var stateList = _context.tblState.ToList();//.Where(x => x.StateName == supplier.State).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateId", "StateName");

            var COAList = _context.ChartOfAccounts.ToList();
            ViewData["COAList"] = new SelectList(COAList, "Id", "Name");

            var TaxList = _context.TaxCode.ToList();
            ViewData["TaxList"] = new SelectList(TaxList, "Id", "Code");

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,ChartOfAccount,Remark,RegistrationNumber,ContactPerson,Address1,Address2,City,PostCode,Country,State,PhoneNumber,Fax,Email,Website,TaxCode,TaxNumber,TaxExpiryDate,Status, CreatedBy,CreatedDatetime,ModifiedBy,ModifeidDatetime")] Supplier supplier)
        {
            try
            {
                bool isCodeExist = _context.MasterAssetBrand.Any(m => m.Code.Equals(supplier.Code) && !m.Id.Equals(id));
                if (isCodeExist)
                {
                    ModelState.AddModelError("Code", "Code Already Exists!");
                    return View(supplier);
                    //return RedirectToAction(nameof(Index), new { error = "Code exists" });
                }

                Supplier db_supplier = _context.Supplier.FirstOrDefault(m => m.Id.Equals(supplier.Id));
                if (supplier == null || id != supplier.Id)
                {
                    return NotFound();
                }

                OldDataSupplier oldData = new OldDataSupplier();
                oldData.Code = db_supplier.Code;
                oldData.Name = db_supplier.Name;
                oldData.ChartOfAccount = db_supplier.ChartOfAccount;
                oldData.Remark = db_supplier.Remark;
                oldData.RegistrationNumber = db_supplier.RegistrationNumber;
                oldData.ContactPerson = db_supplier.ContactPerson;
                oldData.Address1 = db_supplier.Address1;
                oldData.Address2 = db_supplier.Address2;
                oldData.City = db_supplier.City;
                oldData.PostCode = db_supplier.PostCode;
                oldData.Country = db_supplier.Country;
                oldData.State = db_supplier.State;
                oldData.PhoneNumber = db_supplier.PhoneNumber;
                oldData.Fax = db_supplier.Fax;
                oldData.Email = db_supplier.Email;
                oldData.Website = db_supplier.Website;
                oldData.TaxCode = db_supplier.TaxCode;
                oldData.TaxNumber = db_supplier.TaxNumber;
                oldData.TaxExpiryDate = db_supplier.TaxExpiryDate;
                oldData.Status = db_supplier.Status;

                string oldJson = JsonConvert.SerializeObject(oldData);
                string newJson = JsonConvert.SerializeObject(supplier);

                db_supplier.Code = supplier.Code;
                db_supplier.Name = supplier.Name;
                db_supplier.ChartOfAccount = supplier.ChartOfAccount;
                db_supplier.Remark = supplier.Remark;
                db_supplier.RegistrationNumber = supplier.RegistrationNumber;
                db_supplier.ContactPerson = supplier.ContactPerson;
                db_supplier.Address1 = supplier.Address1;
                db_supplier.Address2 = supplier.Address2;
                db_supplier.City = supplier.City;
                db_supplier.PostCode = supplier.PostCode;
                db_supplier.Country = supplier.Country;
                db_supplier.State = supplier.State;
                db_supplier.PhoneNumber = supplier.PhoneNumber;
                db_supplier.Fax = supplier.Fax;
                db_supplier.Email = supplier.Email;
                db_supplier.Website = supplier.Website;
                db_supplier.TaxCode = supplier.TaxCode;
                db_supplier.TaxNumber = supplier.TaxNumber;
                db_supplier.TaxExpiryDate = supplier.TaxExpiryDate;
                db_supplier.Status = supplier.Status;
                db_supplier.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                db_supplier.ModifiedDatetime = DateTime.UtcNow;
                AuditService.InsertActionLog(db_supplier.CompanyId, db_supplier.CreatedBy, "Edit", "Supplier", oldJson, newJson);

                _context.Update(db_supplier);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(supplier.Id))
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
                            string chartOfAccount = values[2];
                            string remark = values[3];
                            string registrationNumber = values[4];
                            string contactPerson = values[5]; 
                            string address1 = values[6]; 
                            string address2 = values[7]; 
                            string city = values[8]; 
                            string postCode = values[9]; 
                            string country = values[10]; 
                            string state = values[11];
                            string phoneNumber = values[12]; 
                            string fax = values[13]; 
                            string email = values[14]; 
                            string website = values[15]; 
                            string taxCode = values[16]; 
                            string taxNumber = values[17];
                            string taxExpiryDatestring = values[18];
                            string status = values[19];

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
                                    UpdateDm(code, name, chartOfAccount, remark, registrationNumber, contactPerson,
                                        address1, address2, city, Convert.ToInt32(postCode), country, state, phoneNumber, fax,
                                        email, website, taxCode, taxNumber, taxExpiryDatestring, IsStatus);
                                }
                                else
                                {
                                    CreateDm(code, name, chartOfAccount, remark, registrationNumber, contactPerson,
                                        address1, address2, city, Convert.ToInt32(postCode), country, state, phoneNumber, fax,
                                        email, website, taxCode, taxNumber, taxExpiryDatestring, IsStatus);
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

            return RedirectToAction("Index", "Supplier");
        }

        [HttpPost]
        public async Task<FileResult> Export()
        {
            string[] names = typeof(Supplier).GetProperties().Select(property => property.Name).ToArray();

            string[] columnNames = new string[] { "Code", "Name", "ChartOfAccountName", "Remark", "RegistrationNumber", "ContactPerson", "Address1", "Address2", "City", "PostCode", "Country", "State", "PhoneNumber", "Fax", "Email", "Website", "TaxCodeName", "TaxNumber", "TaxExpiryDate", "Status" };

            Supplier entities = new Supplier();

            var suppliers = await _context.Supplier.ToListAsync();

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

            foreach (var supplier in suppliers)
            {
                //Add the Data rows.
                csv += supplier.Code.Replace(",", ";") + ',';
                csv += supplier.Name.Replace(",", ";") + ',';
                csv += supplier.ChartOfAccount.Replace(",", ";") + ',';
                if (supplier.Remark != null)
                {
                    csv += supplier.Remark.Replace(",", ";") + ',';
                }
                else
                {
                    supplier.Remark = "";
                    csv += supplier.Remark.Replace(",", ";") + ',';
                }

                csv += supplier.RegistrationNumber.Replace(",", ";") + ',';
                csv += supplier.ContactPerson.Replace(",", ";") + ',';
                csv += supplier.Address1.Replace(",", ";") + ',';
                csv += supplier.Address2.Replace(",", ";") + ',';
                csv += supplier.City.Replace(",", ";") + ',';
                csv += Convert.ToString(supplier.PostCode).Replace(",", ";") + ',';
                csv += supplier.Country.Replace(",", ";") + ',';
                csv += supplier.State.Replace(",", ";") + ',';
                csv += supplier.PhoneNumber.Replace(",", ";") + ',';
                csv += supplier.Fax.Replace(",", ";") + ',';
                csv += supplier.Email.Replace(",", ";") + ',';
                csv += supplier.Website.Replace(",", ";") + ',';
                csv += supplier.TaxCode.Replace(",", ";") + ',';
                csv += supplier.TaxNumber.Replace(",", ";") + ',';
                csv += Convert.ToString(supplier.TaxExpiryDate).Replace(",", ";") + ',';

                if (supplier.Status != false)
                {
                    string Status = "YES";
                    csv += Status.Replace(",", ";");
                }
                if (supplier.Status != true)
                {
                    string Status = "NO";
                    csv += Status.Replace(",", ";");
                }

                //Add new line.
                csv += "\r\n";
            }

            var file = "Supplier.csv";
            //var file = ViewData;
            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", file);
        }

        private bool CreateDm(string code, string name, string chartOfAccount, string remark, string registrationNumber,
            string contactPerson, string address1, string address2, string city, int postCode, string country, string state,
            string phoneNumber, string fax, string email, string website, string taxCode, string taxNumber,
            string taxExpiryDate, bool status)
        {
            bool res = false;
            Supplier supplier = new Supplier();

            try
            {
                NewDataSupplier newData = new NewDataSupplier();
                newData.Code = code;
                newData.Name = name;
                newData.ChartOfAccount = chartOfAccount;
                newData.Remark = remark;
                newData.RegistrationNumber = registrationNumber;
                newData.ContactPerson = contactPerson;
                newData.Address1 = address1;
                newData.Address2 = address2;
                newData.City = city;
                newData.PostCode = postCode;
                newData.Country = country;
                newData.State = state;
                newData.PhoneNumber = phoneNumber;
                newData.Fax = fax;
                newData.Email = email;
                newData.Website = website;
                newData.TaxCode = taxCode;
                newData.TaxNumber = taxNumber;
                newData.TaxExpiryDate = Convert.ToDateTime(taxExpiryDate);
                newData.Status = status;

                string newJson = JsonConvert.SerializeObject(newData);

                supplier.Code = code;
                supplier.Name = name;
                supplier.ChartOfAccount = chartOfAccount;
                supplier.Remark = remark;
                supplier.RegistrationNumber = registrationNumber;
                supplier.ContactPerson = contactPerson;
                supplier.Address1 = address1;
                supplier.Address2 = address2;
                supplier.City = city;
                supplier.PostCode = postCode;
                supplier.Country = country;
                supplier.State = state;
                supplier.PhoneNumber = phoneNumber;
                supplier.Fax = fax;
                supplier.Email = email;
                supplier.Website = website;
                supplier.TaxCode = taxCode;
                supplier.TaxNumber = taxNumber;
                supplier.TaxExpiryDate = Convert.ToDateTime(taxExpiryDate);
                supplier.Status = status;
                supplier.CompanyId = 123;
                supplier.IsUsed = true;
                supplier.Status = status;
                supplier.CreatedBy = _configuration.GetValue<string>("HardcodeValue:Createdby");
                supplier.CreatedDatetime = DateTime.UtcNow;
                _context.Add(supplier);
                AuditService.InsertActionLog(supplier.CompanyId, supplier.CreatedBy, "Import Insert", "Supplier", null, newJson);

                _context.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        private bool UpdateDm(string code, string name,string chartOfAccount, string remark, string registrationNumber,
            string contactPerson, string address1, string address2, string city, int postCode, string country, string state,
            string phoneNumber, string fax, string email, string website, string taxCode, string taxNumber,
            string taxExpiryDate, bool status)
        {
            bool res = false;

            try
            {
                Supplier supplier = _context.Supplier.FirstOrDefault(m => m.Code.Equals(code));

                OldDataSupplier oldData = new OldDataSupplier();
                oldData.Code = supplier.Code;
                oldData.Name = supplier.Name;
                oldData.ChartOfAccount = chartOfAccount;
                oldData.Remark = remark;
                oldData.RegistrationNumber = registrationNumber;
                oldData.ContactPerson = contactPerson;
                oldData.Address1 = address1;
                oldData.Address2 = address2;
                oldData.City = city;
                oldData.PostCode = postCode;
                oldData.Country = country;
                oldData.State = state;
                oldData.PhoneNumber = phoneNumber;
                oldData.Fax = fax;
                oldData.Email = email;
                oldData.Website = website;
                oldData.TaxCode = taxCode;
                oldData.TaxNumber = taxNumber;
                oldData.TaxExpiryDate = Convert.ToDateTime(taxExpiryDate);
                oldData.Status = status;

                string oldJson = JsonConvert.SerializeObject(oldData);
                string newJson = JsonConvert.SerializeObject(supplier);

                if (supplier != null)
                {
                    supplier.Code = code;
                    supplier.Name = name;
                    supplier.ChartOfAccount = chartOfAccount;
                    supplier.Remark = remark;
                    supplier.RegistrationNumber = registrationNumber;
                    supplier.ContactPerson = contactPerson;
                    supplier.Address1 = address1;
                    supplier.Address2 = address2;
                    supplier.City = city;
                    supplier.PostCode = postCode;
                    supplier.Country = country;
                    supplier.State = state;
                    supplier.PhoneNumber = phoneNumber;
                    supplier.Fax = fax;
                    supplier.Email = email;
                    supplier.Website = website;
                    supplier.TaxCode = taxCode;
                    supplier.TaxNumber = taxNumber;
                    supplier.TaxExpiryDate = Convert.ToDateTime(taxExpiryDate);
                    supplier.Status = status;
                    supplier.ModifiedDatetime = DateTime.UtcNow;
                    supplier.ModifiedBy = _configuration.GetValue<string>("HardcodeValue:Modifiedby");
                    AuditService.InsertActionLog(supplier.CompanyId, supplier.CreatedBy, "Import Update", "Supplier", oldJson, newJson);
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

        public bool IsCodeExists(string code)
        {
            bool isExists = false;
            if (string.IsNullOrEmpty(code))
            {
                isExists = false;
            }
            else
            {
                string dbCode = _context.Supplier.FirstOrDefault(m => m.Code.Equals(code))?.Code;
                if (!string.IsNullOrEmpty(dbCode))
                {
                    isExists = true;
                }
            }

            return isExists;
        }

        public ActionResult getState(int id)
        {
            var stateList = _context.tblState.Where(x => x.CountryId == id).OrderByDescending(x => x.StateName).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateId", "StateName");
            return Json(ViewData["stateList"]);

        }

        public IActionResult getState(string name)
        {
            var coun = _context.tblCountry.Where(x => x.CountryName == name).FirstOrDefault();

            var stateList = _context.tblState.Where(x => x.CountryId == coun.CountryId).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateId", "StateName");

            return View();
        }

        public JsonResult getStates(string name)
        {
            ViewData.Clear();
            var coun = _context.tblCountry.Where(x => x.CountryName == name).FirstOrDefault();

            var stateList = _context.tblState.Where(x => x.CountryId == coun.CountryId).ToList();
            ViewData["stateList"] = new SelectList(stateList, "StateId", "StateName");

            return Json(stateList);
        }

        public string getChartOfAccount(int COAid)
        {
            var COAName = _context.ChartOfAccounts.Where(x => x.Id == COAid).Select(x => x.Name);

            return COAName.ToString();
        }

        public string getTaxname(int Taxid)
        {
            var TaxName = _context.TaxCode.Where(x => x.Id == Taxid).Select(x => x.Name);

            return TaxName.ToString();
        }

        private bool SupplierExists(int id)
        {
            return _context.MasterAssetBrand.Any(e => e.Id == id);
        }
    }
}
