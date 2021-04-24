using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FixedModules.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FixedModules.Controllers
{
    public class NavigationMenuController : Controller
    {
        private readonly MyDbContext _context;
        RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public NavigationMenuController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, MyDbContext context)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        // GET: NavigationMenu
        public async Task<IActionResult> Index()
        {
            var roleIds = await GetUserRoleIds();

            //ViewBag.menu = await _context.RoleMenuPermission.Include(x => x.NavigationMenu).Include(x => x.RolesStatus).Include(x => x.NavigationMenu.ParentNavigationMenu).Where(x => roleIds.Contains(x.RoleId)).ToListAsync();

            var applicationDbContext = _context.RoleMenuPermission.Include(x => x.NavigationMenu).Include(x => x.NavigationMenu.ParentNavigationMenu).Where(x => roleIds.Contains(x.RoleId));

            return View(await applicationDbContext.ToListAsync());
        }

        private async Task<List<string>> GetUserRoleIds()
        {
            var userId = _userManager.GetUserId(User);
            var data = await (from role in _context.UserRoles
                              where role.UserId == userId
                              select role.RoleId).ToListAsync();

            return data;
        }

        // GET: NavigationMenu/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationMenu = await _context.NavigationMenu
                .Include(n => n.ParentNavigationMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navigationMenu == null)
            {
                return NotFound();
            }

            return View(navigationMenu);
        }

        // GET: NavigationMenu/Create
        public IActionResult Create()
        {
            var parent = _context.NavigationMenu.Where(x => x.ControllerName == null && x.ActionName == null);
            ViewData["ParentMenuId"] = new SelectList(parent, "Id", "Name");

            return View();
        }

        // POST: NavigationMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentMenuId,ControllerName,ActionName,Status")] NavigationMenu navigationMenu)
        {
            if (ModelState.IsValid)
            {
                navigationMenu.Id = Guid.NewGuid();
                _context.Add(navigationMenu);
                await _context.SaveChangesAsync();
                ModelState.AddModelError("Create", "Successfully Create Child Module");
                ViewBag.Id = navigationMenu.Id;

                return RedirectToAction("Index", "NavigationMenu");
            }
            ViewData["ParentMenuId"] = new SelectList(_context.NavigationMenu, "Id", "Name", navigationMenu.ParentMenuId);
            return View(navigationMenu);
        }

        public IActionResult CreateParent()
        {
            var parent = _context.NavigationMenu.Where(x => x.ControllerName == null && x.ActionName == null);
            ViewData["ParentMenuId"] = new SelectList(parent, "Id", "Name");

            return View();
        }

        // POST: NavigationMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParent([Bind("Id,Name,Status")] NavigationMenu navigationMenu)
        {
            if (ModelState.IsValid)
            {
                var lastNo = await _context.NavigationMenu.OrderBy(x => x.DisplayOrder).Select(x => x.DisplayOrder).LastAsync();

                navigationMenu.DisplayOrder = (Int16)(lastNo + Convert.ToInt16(1));
                navigationMenu.Id = Guid.NewGuid();
                navigationMenu.Visible = true;
                _context.Add(navigationMenu);
                await _context.SaveChangesAsync();
                ModelState.AddModelError("Create", "Successfully Create Parent Module");
                ViewBag.Id = navigationMenu.Id;

                return View();
            }
            //ViewData["ParentMenuId"] = new SelectList(_context.NavigationMenu, "Id", "Name", navigationMenu.ParentMenuId);
            return View(navigationMenu);
        }

        // GET: NavigationMenu/Edit/5
        public async Task<IActionResult> Edit(Guid? id, string StatusMessage = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (StatusMessage != null)
            {
                ModelState.AddModelError("Edit", "Successfully Edit Module");
                ViewBag.Id = id;
            }

            var parent = _context.NavigationMenu.Where(x => x.ControllerName == null && x.ActionName == null);

            var navigationMenu = await _context.NavigationMenu.FindAsync(id);
            if (navigationMenu == null)
            {
                return NotFound();
            }
            ViewData["ParentMenuId"] = new SelectList(parent, "Id", "Name", navigationMenu.ParentMenuId);
            return View(navigationMenu);
        }

        // POST: NavigationMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ParentMenuId,ControllerName,ActionName,Status")] NavigationMenu navigationMenu)
        {
            if (id != navigationMenu.Id)
            {
                return NotFound();
            }

            var local = _context.Set<NavigationMenu>().Local.FirstOrDefault(entry => entry.Id.Equals(navigationMenu.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //navigationMenu.Status = 2;

                    _context.Update(navigationMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavigationMenuExists(navigationMenu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Edit", new { id = navigationMenu.Id, StatusMessage = "Success Edit" });
            }
            ViewData["ParentMenuId"] = new SelectList(_context.NavigationMenu, "Id", "Id", navigationMenu.ParentMenuId);
            return View(navigationMenu);
        }

        // GET: NavigationMenu/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navigationMenu = await _context.NavigationMenu
                .Include(n => n.ParentNavigationMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navigationMenu == null)
            {
                return NotFound();
            }

            return View(navigationMenu);
        }

        // POST: NavigationMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var navigationMenu = await _context.NavigationMenu.FindAsync(id);
            _context.NavigationMenu.Remove(navigationMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavigationMenuExists(Guid id)
        {
            return _context.NavigationMenu.Any(e => e.Id == id);
        }
    }
}
