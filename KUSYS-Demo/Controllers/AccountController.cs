using EntityLayer;
using KUSYS_Demo.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<User> signInManager, ILogger<AccountController> logger, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email.ToUpper());
                }
                var signResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
                if (signResult.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    if (role.Any(x => x.Equals("Admin")))
                    {
                        return RedirectToAction("Students", "Home");
                    }
                    else
                    {
                        return RedirectToAction("StudentsAll", "Home");
                    }
                }
                else
                {
                    ViewBag.LoginErrorMessage = "Kullanıcı Adı veya Şifre Yanlıştır.";
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
