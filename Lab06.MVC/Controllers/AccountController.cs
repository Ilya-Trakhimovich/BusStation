using Lab06.DAL.Entities;
using Lab06.MVC.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lab06.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tempUser = await _userManager.FindByNameAsync(model.Login) ?? new ApplicationUser();

                if (model.Login == tempUser.UserName)
                {
                    ModelState.AddModelError(string.Empty, "Login is already taken");
                }
                else
                {
                    var user = new ApplicationUser { UserName = model.Login };
                    var userResult = await _userManager.CreateAsync(user, model.Password);

                    if (userResult.Succeeded && await _roleManager.RoleExistsAsync("customer"))
                    {
                        await _userManager.AddToRoleAsync(user, "customer");
                        await _signInManager.SignInAsync(user, false);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var e in userResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, e.Description);
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (model.Login.Length < 4)
                {
                    ModelState.AddModelError(string.Empty, "Login must have at least 4 characters");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login or password is incorrect");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Register", "Account");
        }        
    }
}
