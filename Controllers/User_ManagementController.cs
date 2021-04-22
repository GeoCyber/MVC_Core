using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FixedModules.Areas.Identity.Pages.Account;
using FixedModules.Data;
using FixedModules.Models;
using FixedModules.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FixedModules.Controllers
{
    public class User_ManagementController : Controller
    {

        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public User_ManagementController(MyDbContext context, IConfiguration configuration, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger,
            IEmailSender emailSender, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }
        // GET: Auth
        public async Task<ActionResult> IndexAsync()
        {
            var roleViewModel = new List<RoleViewModel>();
            var userViewModel = new List<UserViewModel>();
            UserData mymodel = new UserData();
            var roles = await _roleManager.Roles.ToListAsync();
            foreach (var item in roles)
            {
                roleViewModel.Add(new RoleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }

            var users = await _userManager.Users.ToListAsync();
            foreach (var item in users)
            {
                userViewModel.Add(new UserViewModel()
                {
                    Id = item.Id,
                    Email = item.Email,
                    UserName = item.UserName,
                    IsInvited = _context.ApplicationUser.Where(x => x.Email == item.Email).Select(y => y.IsInvited).FirstOrDefault(),
                    EmailConfirmed = _context.ApplicationUser.Where(x => x.Email == item.Email).Select(y => y.EmailConfirmed).FirstOrDefault(),
                });
            }

            mymodel.PRoles = roleViewModel;
            mymodel.PUsers = userViewModel;
            mymodel.PUserRoles = _context.PUserRoles.ToList();

            return View(mymodel);
        }

        public List<ApplicationRoles> GetRoles() {
            List <ApplicationRoles> res = _context.ApplicationRoles.ToList();

            return res;
        }

        public List<string> GetUserEmailList()
        {
            List<string> res = _userManager.Users.Select(m => m.Email).ToList();

            return res;
        }

        // GET: Auth/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MasterDepartment/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var companies = _userManager.Users.First(m => m.Id.Equals(id));
            await _userManager.DeleteAsync(companies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public JsonResult GetUserRole(string searchTerm)
        //{
        //    var role = _context.UserRoles.ToList();


        //    if (searchTerm != null)
        //    {
        //        role = _context.UserRoles.Where(x => x.Desc.Contains(searchTerm)).ToList();
        //    }

        //    var modifiedData = role.Select(x => new
        //    {
        //        RoleId = x.Id,
        //        RoleDesc = x.Desc
        //    });
        //    return Json(modifiedData);
        //}


        //public ActionResult Invite()
        //{
        //    var roleViewModel = new List<RoleViewModel>();
        //    var nav = _roleManager.Roles.OrderByDescending(x => x.Name);
        //    ViewData["Roles"] = new SelectList(nav, "Id", "Name");
        //    //ViewData["roles"] = new SelectList(_roleManager.Roles, "Id", "Name");
        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InputModel Input, string roleIds, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    returnUrl = returnUrl ?? Url.Content("~/");
                    Input.Password = "admin@123";
                    var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    user.IsInvited = true;
                    _context.ApplicationUser.Update(user);
                    await _context.SaveChangesAsync();

                    foreach(string roleId in roleIds.Split(',')) {
                        PUserRoles puserrole = new PUserRoles();
                        puserrole.UserId = user.Id;
                        puserrole.RoleId = roleId;
                        _context.PUserRoles.Add(puserrole);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction(nameof(IndexAsync));
                }
            }
            return RedirectToAction(nameof(IndexAsync));
        }

        public List<UserViewModel> Search(string email)
        {
            List<UserViewModel> res = new List<UserViewModel>();
            email = string.IsNullOrEmpty(email) ? string.Empty : email.Trim().ToLower();

            try
            {
                //var users = _userManager.Users.Where(m => m.Email.ToLower().Equals(email)).ToList();
                var users = _userManager.Users.Where(m => m.Email.ToLower().Contains(email)).ToList();

                foreach (var user in users) {
                    UserViewModel userModel = new UserViewModel();

                    userModel.Id = user.Id;
                    userModel.Email = user.Email;
                    userModel.UserName = user.UserName;
                    userModel.IsInvited = _context.ApplicationUser.Where(x => x.Email == user.Email).Select(y => y.IsInvited).FirstOrDefault();
                    userModel.EmailConfirmed = user.EmailConfirmed;

                    List<PUserRoles> roles = _context.PUserRoles.Where(m => m.UserId.Equals(userModel.Id)).ToList();
                    userModel.Roles = new RoleViewModel[roles.Count];
                 
                    for(int i = 0; i < roles.Count; i++) {
                        RoleViewModel roleModel = new RoleViewModel();
                        var q = _roleManager.Roles.First(m => m.Id.Equals(roles[i]));
                        roleModel.Id = q.Id;
                        roleModel.Name = q.Name;
                        userModel.Roles.Append(roleModel);
                    }

                    res.Add(userModel);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return res;
        }

        public async Task<IActionResult> EditUser(string id)
        {
            int depId = 0;
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out depId))
            {
                return NotFound();
            }


            var user_management = await _userManager.FindByIdAsync(id);
            if (user_management == null)
            {
                return NotFound();
            }
            return View(user_management);
        }
        public async Task<IActionResult> InviteUser(string id)
        {
            var viewModel = new UserViewModel();
            var user = await _userManager.FindByIdAsync(id);
                var allRoles = await _roleManager.Roles.ToListAsync();
                viewModel.Roles = allRoles.Select(x => new RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToArray();

            viewModel.Email = user.Email;

            return View(viewModel);
        }

        public async Task<IActionResult> Invite()
        {
            var viewModel = new UserViewModel();
            var allRoles = await _roleManager.Roles.ToListAsync();
            viewModel.Roles = allRoles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToArray();

           
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> InviteUser(UserViewModel viewModel, string roleIds)
        {
            if (ModelState.IsValid)
            {
                var regis = new ApplicationUser { UserName = viewModel.Email, Email = viewModel.Email, IsInvited = true };
                var result = await _userManager.CreateAsync(regis, "admin@123");
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(viewModel.Email);
                    var userRoles = await _userManager.GetRolesAsync(user);

                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRolesAsync(user, viewModel.Roles.Where(x => x.Selected).Select(x => x.Name));

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Action("ConfirmEmail", "User_Management", new { userId = user.Id, code = code }, protocol: Request.Scheme);
                        //Url.Action(
                        //"/Account/ConfirmEmail",
                        //pageHandler: null,
                        //values: new { area = "Identity", userId = user.Id, code = code },
                        //protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    foreach (string roleId in roleIds.Split(','))
                    {
                        PUserRoles puserrole = new PUserRoles();
                        puserrole.UserId = user.Id;
                        puserrole.RoleId = roleId;
                        _context.PUserRoles.Add(puserrole);
                    }

                    await _context.SaveChangesAsync();

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("ConfirmEmailUrl", "User_Management", new { url = callbackUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Admin", "User");
                    }
                }

                

                return RedirectToAction("Admin","User");
            }

            return View(viewModel);
        }

        public bool DisplayConfirmAccountLink { get; set; }

        public async Task<IActionResult> ConfirmEmailUrl(string url)
        {
            ViewBag.url = url;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail (string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            //StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string Email, [FromForm] List<RoleViewModel> user_Login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PUserRoles user_Invited = new PUserRoles();
                    var uu = _context.ApplicationUser.Where(x => x.Email == Email).Select(y => new { y.Id }).FirstOrDefault();
                    
                    ApplicationUser db_user_Login = _context.ApplicationUser.FirstOrDefault(m => m.Id.Equals(uu.Id));
                    if (db_user_Login == null)
                    {
                        return NotFound();
                    }
                    //var roleList = _context.UserInvited.Where(x => x.UserId == db_user_Login.Id).ToList();
                    //_context.Remove(roleList);
                    //await _context.SaveChangesAsync();

                    user_Invited.UserId = db_user_Login.Id;
                    //user_Invited.RoleId = "535f2e47-9bf3-4ac9-a298-e866f79b779f";
                    _context.Add(user_Invited);
                    await _context.SaveChangesAsync();

                    foreach (var items in user_Login)
                    {
                        user_Invited.UserId = db_user_Login.Id;
                        user_Invited.RoleId = items.Id;
                        _context.Add(user_Invited);
                        await _context.SaveChangesAsync();
                    }

                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!Department_MasterExists(department_Master.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(IndexAsync));
            }
            return RedirectToAction(nameof(IndexAsync));
        }

        private bool User_ManagementExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}