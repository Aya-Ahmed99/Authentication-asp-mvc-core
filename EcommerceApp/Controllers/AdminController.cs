using EcommerceApp.viewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcommerceApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }

        public IActionResult Dashboard()
        {
            return View();
        }


        public IActionResult Index()
        {
            return PartialView();
        }
        public IActionResult AllRoles()
        {
            var roles = roleManager.Roles.ToList();
            var rolemodel = roles.Select(r => new RoleVM
            {
                Id = r.Id,
                RoleName = r.Name
            }).ToList();

            return PartialView(rolemodel);
        }

        public IActionResult NewRole()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> NewRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleModel = new IdentityRole();
                roleModel.Name = roleVM.RoleName;
                IdentityResult identityResult = await roleManager.CreateAsync(roleModel);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return RedirectToAction("Dashboard");

                    }
                }
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            RoleVM roleVM = new RoleVM()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return PartialView(roleVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = await roleManager.FindByIdAsync(roleVM.Id);

                if (role == null)
                {
                    return NotFound();
                }

                role.Name = roleVM.RoleName;

                IdentityResult result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return RedirectToAction("Dashboard");
        }



    }
}
