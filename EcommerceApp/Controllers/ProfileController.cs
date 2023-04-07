using EcommerceApp.Models;
using EcommerceApp.viewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509.SigI;

namespace EcommerceApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> PersonalData()
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var userData = await userManager.FindByIdAsync(currentUser.Id);

            var userModel = new PersonalDataVM
            {
                UserName = userData.UserName,
                FullName = userData.FullName,
                Email   = userData.Email,
                Gender  = userData.Gender,  
                Address= userData.Address,  
                BirthDate= userData.BirthDate,
            };

            return PartialView(userModel);
        }

        [HttpGet]
        public async Task< IActionResult> UpdateUser()
        {
            var model = await userManager.GetUserAsync(User);

            if (model == null)
            {
                return NotFound();
            }
            var user = new RegisterVM();
            user.FullName = model.FullName;
            user.Gender = model.Gender;
            user.Address = model.Address;
            user.BirthDate = model.BirthDate;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Password = model.PasswordHash;

            return PartialView(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Error updating user data.";
                return RedirectToAction("Profile");
            }

            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var newPasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);

            user.FullName = model.FullName;
            user.Gender = model.Gender;
            user.Address = model.Address;
            user.BirthDate = model.BirthDate;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PasswordHash = newPasswordHash;
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Error updating user data.";
                return RedirectToAction("Profile");

            }

            return RedirectToAction("Profile");
        }
    }
}

