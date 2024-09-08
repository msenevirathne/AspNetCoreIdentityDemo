using AspNetCoreIdentityDemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityDemo.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(model.RoleName);

                if(roleExists) 
                {
                    ModelState.AddModelError("", "Role already exists.");
                }
                else
                {
                    var identityRole = new IdentityRole 
                    { 
                        Name = model.RoleName 
                    };

                    var result = await _roleManager.CreateAsync(identityRole);

                    if(result.Succeeded) 
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ListRoles()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
    }
}
