using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _UserManager, SignInManager<ApplicationUser> _SignInManager)
        {
            userManager = _UserManager;
            signInManager = _SignInManager;
        }
        [HttpGet] //<ahref>
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if(ModelState.IsValid)
            {
                //create Account
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUserVM.UserName;
                userModel.PasswordHash = newUserVM.Password;
                IdentityResult result = await userManager.CreateAsync(userModel,newUserVM.Password);
                if (result.Succeeded)
                {
                    //create cookie
                   await signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction("Index","Book");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                await userManager.CreateAsync(userModel);

            }
            return View(newUserVM);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userVm)
        {
            if (ModelState.IsValid)
            {
                //check pass & username
                ApplicationUser userModel= await userManager.FindByNameAsync(userVm.UserName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, userVm.Password);
                    if(found)
                    {
                        await signInManager.SignInAsync(userModel, userVm.RememberMe);
                        return RedirectToAction("Index", "Book");
                    }
                }
                ModelState.AddModelError("", "UserName and Password Invalid");

            }
            return View(userVm);
        }
    }
}
