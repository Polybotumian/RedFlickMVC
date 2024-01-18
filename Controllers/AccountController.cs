using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedFlickMVC.Models;
using System.Diagnostics;

namespace RedFlickMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(ILogger<AccountController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Email = model.UserEmail };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    // Redirect to a success page or login page
                    return RedirectToAction("RegisterSuccess");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If the model is not valid, return the registration view with errors
            return View(model);
        }

        public IActionResult RegisterSuccess()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Redirect to a success page or dashboard
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            // If the model is not valid or login fails, return the login view with errors
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Sign the user out
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Redirect to the home page or any other page after logout
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ProfileAsync()
        {
            // Retrieve the user by the current user's name
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                // Pass the user and other details to the view
                return View(new UserProfileViewModel
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        Roles = (List<string>) await _signInManager.UserManager.GetRolesAsync(user),
                        // Add more properties as needed
                    }
                );
            }

            // Handle the case where the user is not found
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
