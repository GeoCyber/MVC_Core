using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FixedModules.Data;
using FixedModules.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FixedModules.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        private readonly MyDbContext _context;

        /// 
        /// Injecting Role Manager
        /// 
        /// 
        public RolesController(RoleManager<IdentityRole> roleManager, MyDbContext context)
        {
            this.roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public async Task<IActionResult> CreateRoles()
        {
            var navmenu = _context.NavigationMenu;

            //ViewBag.Parent = _context.NavigationMenu.Include(x => x.ParentNavigationMenu).Where(x => x.ParentMenuId == null && x.Name.Contains("Home")).Select(x => new 
            //{
            //    Seq = x.Seq,
            //    Parent = x.Name,
            //    Child = _context.NavigationMenu.
            //}).OrderBy(x => x.Seq).ToList();
            //ViewBag.Child = _context.NavigationMenu.Where(x => x.ActionName != null && x.ControllerName != null).ToList();
            return View(await navmenu.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles(IdentityRole role, List<string> roles, string rolename)
        {
            List<RoleMenuExtRMP> p = new List<RoleMenuExtRMP>();

            role.Name = rolename;

            await roleManager.CreateAsync(role);

            var local = _context.Set<IdentityRole>().Local.FirstOrDefault(entry => entry.Id.Equals(role.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }

            foreach (var x in roles)
            {
                RoleMenuExtRMP permission = new RoleMenuExtRMP();

                permission.NavigationMenuId = new Guid(x.Substring(2, 36));
                permission.TempNavMenu = x.Substring(0, 1);

                p.Add(permission);
            }

            var count = p.GroupBy(x => x.NavigationMenuId).Select(c => new
            {
                id = c.Key,
                count = c.Select(s => s.RoleId).Distinct().Count(),
                status = c.Select(s => s.TempNavMenu).ToList()
            }).ToList();

            foreach (var c in count)
            {
                RoleMenuPermission permission = new RoleMenuPermission();
                var alpha = string.Join("", c.status);

                var parent = await _context.NavigationMenu.Where(x => x.Id == c.id).Select(x => x.ParentMenuId).SingleOrDefaultAsync();
                var subparent = await _context.NavigationMenu.Where(x => x.Id == parent.Value).Select(x => x.ParentMenuId).SingleOrDefaultAsync();

                var home = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == Guid.Parse("3C10AD2B-9D75-4E68-811A-A5E7B7B782EE") && x.RoleId == role.Id);

                if (!home)
                {
                    permission.RoleId = Guid.NewGuid().ToString();
                    permission.NavigationMenuId = Guid.Parse("3C10AD2B-9D75-4E68-811A-A5E7B7B782EE");
                    permission.RoleId = role.Id;
                    permission.Status = 5;

                    _context.RoleMenuPermission.Add(permission);
                    await _context.SaveChangesAsync();
                }

                if (subparent != null)
                {
                    var checker = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == subparent.Value && x.RoleId == role.Id);

                    if (!checker)
                    {
                        permission.RoleId = Guid.NewGuid().ToString();
                        permission.NavigationMenuId = subparent.Value;
                        permission.RoleId = role.Id;
                        permission.Status = 5;

                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                    }
                }

                if (parent != null)
                {
                    var checker = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == parent.Value && x.RoleId == role.Id);

                    if (!checker)
                    {
                        permission.RoleId = Guid.NewGuid().ToString();
                        permission.NavigationMenuId = parent.Value;
                        permission.RoleId = role.Id;
                        permission.Status = 5;

                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                    }
                }

                permission.RoleId = Guid.NewGuid().ToString();
                permission.NavigationMenuId = c.id;
                permission.RoleId = role.Id;

                switch (alpha)
                {
                    case "a":
                        permission.Status = 1;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "b":
                        permission.Status = 2;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "c":
                        permission.Status = 3;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "d":
                        permission.Status = 4;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "ab":
                        permission.Status = 6;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "ac":
                        permission.Status = 7;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "ad":
                        permission.Status = 8;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "bc":
                        permission.Status = 9;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "bd":
                        permission.Status = 10;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "cd":
                        permission.Status = 11;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "abc":
                        permission.Status = 12;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "abd":
                        permission.Status = 13;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "acd":
                        permission.Status = 14;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "bcd":
                        permission.Status = 15;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    case "abcd":
                        permission.Status = 5;
                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                        break;
                    default:
                        Console.WriteLine("Other");
                        break;
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var roles = _context.Roles.Where(x => x.Id == id
            ).Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).SingleOrDefault();

            ViewBag.Id = roles.Id;
            ViewBag.Name = roles.Name;

            var nav = _context.NavigationMenu.Select(x => new RolesUserPermissionNavMenu
            {
                RoleId = id,
                StatusId = _context.RoleMenuPermission.Include(r => r.RolesStatus).Where(r => r.NavigationMenuId == x.Id && r.RoleId == id).Select(r => r.RolesStatus.Id).SingleOrDefault(),
                NavActionName = x.ActionName,
                NavControllerName = x.ControllerName,
                NavId = x.Id,
                NavName = x.Name,
                NavParentMenuId = x.ParentMenuId
            });

            return View(await nav.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityRole role, List<string> roles, string name)
        {
            var currentrole = roleManager.Roles.Where(x => x.Id == id).SingleOrDefault();

            currentrole.Name = name;
            currentrole.NormalizedName = name.ToUpper();

            _context.Roles.Update(currentrole);
            await _context.SaveChangesAsync();

            List<RoleMenuExtRMP> p = new List<RoleMenuExtRMP>();

            var local = _context.Set<IdentityRole>().Local.FirstOrDefault(entry => entry.Id.Equals(role.Id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }

            foreach (var x in roles)
            {
                RoleMenuExtRMP permission = new RoleMenuExtRMP();

                permission.NavigationMenuId = new Guid(x.Substring(2, 36));
                permission.TempNavMenu = x.Substring(0, 1);

                p.Add(permission);
            }

            var count = p.GroupBy(x => x.NavigationMenuId).Select(c => new
            {
                id = c.Key,
                count = c.Select(s => s.RoleId).Distinct().Count(),
                status = c.Select(s => s.TempNavMenu).ToList()
            }).ToList();

            foreach (var c in count)
            {
                var currentpermission = await _context.RoleMenuPermission.Where(r => r.NavigationMenuId == c.id && r.RoleId == id).AsNoTracking().SingleOrDefaultAsync();

                if(currentpermission != null)
                {
                    RoleMenuPermission permission = new RoleMenuPermission();
                    var alpha = string.Join("", c.status);

                    var parent = await _context.NavigationMenu.Where(x => x.Id == c.id).Select(x => x.ParentMenuId).SingleOrDefaultAsync();
                    var subparent = await _context.NavigationMenu.Where(x => x.Id == parent.Value).Select(x => x.ParentMenuId).SingleOrDefaultAsync();

                    var home = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == Guid.Parse("3C10AD2B-9D75-4E68-811A-A5E7B7B782EE") && x.RoleId == role.Id);

                    if (!home)
                    {
                        permission.Id = Guid.NewGuid();
                        permission.NavigationMenuId = Guid.Parse("3C10AD2B-9D75-4E68-811A-A5E7B7B782EE");
                        permission.RoleId = role.Id;
                        permission.Status = 5;

                        _context.RoleMenuPermission.Update(permission);
                        await _context.SaveChangesAsync();
                    }

                    if (subparent != null)
                    {
                        var checker = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == subparent.Value && x.RoleId == role.Id);

                        if (!checker)
                        {
                            permission.Id = Guid.NewGuid();
                            permission.NavigationMenuId = subparent.Value;
                            permission.RoleId = role.Id;
                            permission.Status = 5;

                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (parent != null)
                    {
                        var checker = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == parent.Value && x.RoleId == role.Id);

                        if (!checker)
                        {
                            permission.Id = Guid.NewGuid();
                            permission.NavigationMenuId = parent.Value;
                            permission.RoleId = role.Id;
                            permission.Status = 5;

                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var localnav = _context.Set<NavigationMenu>().Local.FirstOrDefault(entry => entry.Id.Equals(c.id));

                    // check if local is not null 
                    if (localnav != null)
                    {
                        // detach
                        _context.Entry(localnav).State = EntityState.Detached;
                    }

                    permission.Id = currentpermission.Id;
                    permission.NavigationMenuId = c.id;
                    permission.RoleId = role.Id;

                    switch (alpha)
                    {
                        case "a":
                            permission.Status = 1;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "b":
                            permission.Status = 2;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "c":
                            permission.Status = 3;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "d":
                            permission.Status = 4;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "ab":
                            permission.Status = 6;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "ac":
                            permission.Status = 7;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "ad":
                            permission.Status = 8;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "bc":
                            permission.Status = 9;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "bd":
                            permission.Status = 10;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "cd":
                            permission.Status = 11;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "abc":
                            permission.Status = 12;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "abd":
                            permission.Status = 13;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "acd":
                            permission.Status = 14;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "bcd":
                            permission.Status = 15;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "abcd":
                            permission.Status = 5;
                            _context.RoleMenuPermission.Update(permission);
                            await _context.SaveChangesAsync();
                            break;
                        default:
                            Console.WriteLine("Other");
                            break;
                    }

                } else
                {
                    RoleMenuPermission permission = new RoleMenuPermission();
                    var alpha = string.Join("", c.status);

                    var parent = await _context.NavigationMenu.Where(x => x.Id == c.id).Select(x => x.ParentMenuId).SingleOrDefaultAsync();
                    var subparent = await _context.NavigationMenu.Where(x => x.Id == parent.Value).Select(x => x.ParentMenuId).SingleOrDefaultAsync();

                    var home = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == Guid.Parse("3C10AD2B-9D75-4E68-811A-A5E7B7B782EE") && x.RoleId == role.Id);

                    if (!home)
                    {
                        permission.Id = Guid.NewGuid();
                        permission.NavigationMenuId = Guid.Parse("3C10AD2B-9D75-4E68-811A-A5E7B7B782EE");
                        permission.RoleId = role.Id;
                        permission.Status = 5;

                        _context.RoleMenuPermission.Add(permission);
                        await _context.SaveChangesAsync();
                    }

                    if (subparent != null)
                    {
                        var checker = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == subparent.Value && x.RoleId == role.Id);

                        if (!checker)
                        {
                            permission.Id = Guid.NewGuid();
                            permission.NavigationMenuId = subparent.Value;
                            permission.RoleId = role.Id;
                            permission.Status = 5;

                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (parent != null)
                    {
                        var checker = _context.RoleMenuPermission.Any(x => x.NavigationMenuId == parent.Value && x.RoleId == role.Id);

                        if (!checker)
                        {
                            permission.Id = Guid.NewGuid();
                            permission.NavigationMenuId = parent.Value;
                            permission.RoleId = role.Id;
                            permission.Status = 5;

                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                        }
                    }

                    permission.Id = Guid.NewGuid();
                    permission.NavigationMenuId = c.id;
                    permission.RoleId = role.Id;

                    switch (alpha)
                    {
                        case "a":
                            permission.Status = 1;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "b":
                            permission.Status = 2;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "c":
                            permission.Status = 3;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "d":
                            permission.Status = 4;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "ab":
                            permission.Status = 6;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "ac":
                            permission.Status = 7;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "ad":
                            permission.Status = 8;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "bc":
                            permission.Status = 9;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "bd":
                            permission.Status = 10;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "cd":
                            permission.Status = 11;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "abc":
                            permission.Status = 12;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "abd":
                            permission.Status = 13;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "acd":
                            permission.Status = 14;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "bcd":
                            permission.Status = 15;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        case "abcd":
                            permission.Status = 5;
                            _context.RoleMenuPermission.Add(permission);
                            await _context.SaveChangesAsync();
                            break;
                        default:
                            Console.WriteLine("Other");
                            break;
                    }
                }

            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        public List<ApplicationRoles> Search(string name)
        {
            
            List<ApplicationRoles> res = null;
            name = string.IsNullOrEmpty(name) ? string.Empty : name.Trim().ToLower();

            try
            {
                

                if (!string.IsNullOrEmpty(name))
                {
                    name = name.Trim();
                    res = res.Where(m => m.Name.ToLower().Contains(name)).ToList();
                    
                }


            }
            catch (Exception ex)
            {
                return null;
            }

            return res;
        }
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var roles = await _context.Roles.FindAsync(id);
                _context.Roles.Remove(roles);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
