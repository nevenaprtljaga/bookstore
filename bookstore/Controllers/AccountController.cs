using bookstore.Entities;
using bookstore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
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
                var result = await _signInManager.PasswordSignInAsync(user.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
                if (user.ChangedPassword == false)
                {
                    return RedirectToAction("ChangePassword", new { id = user.Id });
                }
                else
                {
                    if (result.Succeeded && await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Users");
                    }
                    else if (result.Succeeded && await _userManager.IsInRoleAsync(user, "Librarian"))
                    {
                        return RedirectToAction("Index", "Authors");
                    }
                    else if (result.Succeeded && await _userManager.IsInRoleAsync(user, "Customer"))
                    {
                        return RedirectToAction("Index", "Books");
                    }
                    else
                    {
                        return RedirectToAction("Index", "BookGenres");
                    }
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
            if (registerViewModel.EmailAddress == null)
            {
                return View(registerViewModel);
            }
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
                TempData["Error"] = string.Join("\t\n", newUserResponse.Errors.Select(x => x.Description)); //probala Environment.NewLine, <br/>, \n, \\n sta god, nece da napravi novu liniju
                return View(registerViewModel);
            }
            _context.SaveChanges();

            return View("RegisterCompleted");
        }



        [Authorize]
        public async Task<IActionResult> Update()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDetails = await _userManager.Users.FirstOrDefaultAsync(n => n.Id == userId);
            if (userDetails == null)
            {
                return View("NotFound");
            }
            var result = new ApplicationUser()
            {
                Id = userDetails.Id,
                FullName = userDetails.FullName,
                Email = userDetails.Email,
                UserName = userDetails.UserName,
                PhoneNumber = userDetails.PhoneNumber,

            };

            return View(result);

        }

        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUser ApplicationUser)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            user.UserName = ApplicationUser.UserName;
            user.FullName = ApplicationUser.FullName;
            user.Email = ApplicationUser.Email;
            user.PhoneNumber = ApplicationUser.PhoneNumber;
            user.EmailConfirmed = ApplicationUser.EmailConfirmed;
            if (!ModelState.IsValid)
            {
                return View(ApplicationUser);
            }
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Update));
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

        public IActionResult ChangePassword()
        {
            ChangePasswordViewModel vm = new ChangePasswordViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            vm.UserId = userId;
            return View(vm);

        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId = userId;

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.ChangedPassword = true;
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index", "Books");
                    }
                    else
                    {
                        TempData["Error"] = string.Join("\t\n", result.Errors.Select(x => x.Description));
                        return View(model);
                    }
                }
                return RedirectToAction("Index", "Books");
            }
            return View(model);
        }

    }
}