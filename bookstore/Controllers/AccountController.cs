using bookstore.Entities;
using bookstore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bookstore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {

                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(identity));




                var result = await _signInManager.PasswordSignInAsync(loginViewModel.EmailAddress, loginViewModel.Password, loginViewModel.RememberMe, false);

                if (result.Succeeded && await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", "Users");
                }
                else if (result.Succeeded && await _userManager.IsInRoleAsync(user, "Customer"))
                {
                    return RedirectToAction("Index", "Books");
                }
                else
                {
                    return RedirectToAction("Index", "Authors");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(loginViewModel);

            }
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {


            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already registered.";
                return View(registerViewModel);
            }

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var role = _roleManager.Roles.FirstOrDefault((n => n.Name == "Customer"));


            var newUser = new ApplicationUser()
            {
                FullName = registerViewModel.FullName,
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.UserName,
                PhoneNumber = registerViewModel.PhoneNumber,

            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);


            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRolesAsync(newUser, new[] { role.Name });
            }
            else
            {
                TempData["Error"] = string.Join(" \n", newUserResponse.Errors.Select(x => x.Description)); //probala Environment.NewLine, <br/>, \n, \\n sta god, nece da napravi novu liniju
                return View(registerViewModel);
            }
            _context.SaveChanges();

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}