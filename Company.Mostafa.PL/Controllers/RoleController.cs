using Company.Mostafa.DAL.Models;
using Company.Mostafa.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Mostafa.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchInput)
        {
            var roles = Enumerable.Empty<RoleViewModel>();
            if (string.IsNullOrEmpty(searchInput))
            {
                roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
            }
            else
            {
                roles = await _roleManager.Roles.Where(X => X.Name.ToLower().Contains(searchInput.ToLower())).Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
            }

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rolemodel = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var flag = await _roleManager.CreateAsync(rolemodel);
                if (flag.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string viewname)
        {
            if (id is null) return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound();

            var rolemodel = new RoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(viewname, rolemodel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role is null) return NotFound();

                role.Name = model.RoleName;
                var flag = await _roleManager.UpdateAsync(role);
                if (flag.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);

                if (role is null) return NotFound();
                var flag = await _roleManager.DeleteAsync(role);

                if (flag.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string id)
        {
            var users = await _userManager.Users.ToListAsync();
            var role = await _roleManager.FindByIdAsync(id);
            ViewData["roleId"] = id;
            var useraddorremovevm = new List<UserAddOrRemoveViewModel>();
            if (ModelState.IsValid)
            {

                foreach (var user in users)
                {
                    var useraddorremove = new UserAddOrRemoveViewModel()
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        useraddorremove.IsSelected = true;
                    }
                    else
                    {
                        useraddorremove.IsSelected = false;
                    }
                    useraddorremovevm.Add(useraddorremove);
                }
            }
            return View(useraddorremovevm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserAddOrRemoveViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return BadRequest();

            if (ModelState.IsValid)
            {
                
                foreach (var user in users)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);
                    
                    if (user.IsSelected && !await _userManager.IsInRoleAsync(appuser, role.Name))
                    {
                          await _userManager.AddToRoleAsync(appuser, role.Name);
                        
                    }
                    else if(!user.IsSelected && await _userManager.IsInRoleAsync(appuser, role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(appuser, role.Name);
                        
                    }

                    
                }
                
                    return RedirectToAction("Update", new { Id = roleId });
                
            }
            return View(users);
        }
    }
}

