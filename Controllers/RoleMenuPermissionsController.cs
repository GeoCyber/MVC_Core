using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FixedModules.Data;
using FixedModules.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FixedModules.Controllers
{
    public class RoleMenuPermissionsController : Controller
    {
        private readonly MyDbContext _context;
        RoleManager<IdentityRole> _roleManager;

        public RoleMenuPermissionsController(MyDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: RoleMenuPermissions
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.RoleMenuPermission.Include(r => r.NavigationMenu);
            var roles = _context.RoleMenuPermission.Include(r => r.NavigationMenu).Select(x => new RolesPermissionViewModel
            {
                Id = x.Id,
                RolesId = x.RoleId,
                NavId = x.NavigationMenuId,
                RolesName = _roleManager.FindByIdAsync(x.RoleId).Result.Name,
                NavName = x.NavigationMenu.Name,
                Seq = x.NavigationMenu.DisplayOrder
            }).OrderBy(x => x.Seq);

            ViewBag.Roles = roles;

            return View(await roles.ToListAsync());
        }

        // GET: RoleMenuPermissions/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var roleMenuPermission = await _context.RoleMenuPermission
            //    .Include(r => r.NavigationMenu).Include(r => _context.Roles)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (roleMenuPermission == null)
            //{
            //    return NotFound();
            //}

            var roles = _context.RoleMenuPermission.Include(r => r.NavigationMenu).Where(x => x.RoleId == id).Select(x => new RolesPermissionViewModel
            {
                Id = x.Id,
                RolesId = x.RoleId,
                NavId = x.NavigationMenuId,
                RolesName = _roleManager.FindByIdAsync(x.RoleId).Result.Name,
                NavName = x.NavigationMenu.Name
            });

            return View(await roles.SingleOrDefaultAsync());
        }

        // GET: RoleMenuPermissions/Create
        public IActionResult Create()
        {
            var nav = _context.NavigationMenu.OrderByDescending(x => x.ActionName);
            ViewData["NavigationMenuId"] = new SelectList(nav, "Id", "Name");
            ViewData["roles"] = new SelectList(_roleManager.Roles, "Id", "Name");
            return View();
        }

        // POST: RoleMenuPermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleId,NavigationMenuId")] RoleMenuPermission roleMenuPermission)
        {
            if (ModelState.IsValid)
            {
                roleMenuPermission.RoleId = Guid.NewGuid().ToString();
                roleMenuPermission.Status = 1002;
                _context.Add(roleMenuPermission);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Roles");
            }
            ViewData["NavigationMenuId"] = new SelectList(_context.NavigationMenu, "Id", "Id", roleMenuPermission.NavigationMenuId);
            return RedirectToAction("Index", "Roles");
        }

        // GET: RoleMenuPermissions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleMenuPermission = await _context.RoleMenuPermission.FindAsync(id);
            if (roleMenuPermission == null)
            {
                return NotFound();
            }
            ViewData["NavigationMenuId"] = new SelectList(_context.NavigationMenu, "Id", "Name", roleMenuPermission.NavigationMenuId);
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Name", roleMenuPermission.RoleId);
            return View(roleMenuPermission);
        }

        // POST: RoleMenuPermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,RoleId,NavigationMenuId")] RoleMenuPermission roleMenuPermission)
        {
            if (id != roleMenuPermission.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleMenuPermission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleMenuPermissionExists(roleMenuPermission.RoleId))
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
            ViewData["NavigationMenuId"] = new SelectList(_context.NavigationMenu, "Id", "Id", roleMenuPermission.NavigationMenuId);
            return View(roleMenuPermission);
        }

        // GET: RoleMenuPermissions/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var roleMenuPermission = await _context.RoleMenuPermission
            //    .Include(r => r.NavigationMenu)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (roleMenuPermission == null)
            //{
            //    return NotFound();
            //}

            var roles = _context.RoleMenuPermission.Include(r => r.NavigationMenu).Where(x => x.RoleId == id).Select(x => new RolesPermissionViewModel
            {
                Id = x.Id,
                RolesId = x.RoleId,
                NavId = x.NavigationMenuId,
                RolesName = _roleManager.FindByIdAsync(x.RoleId).Result.Name,
                NavName = x.NavigationMenu.Name
            });

            return View(await roles.SingleOrDefaultAsync());

            //return View(roleMenuPermission);
        }

        // POST: RoleMenuPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var roleMenuPermission = await _context.RoleMenuPermission.FindAsync(id);
            _context.RoleMenuPermission.Remove(roleMenuPermission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleMenuPermissionExists(string id)
        {
            return _context.RoleMenuPermission.Any(e => e.RoleId == id.ToString());
        }
    }
}
