using bookstore.Entities;
using bookstore.Models;
using bookstore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _service;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsersController(IUsersService service, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var allUsers = await _service.GetAllAsync();
            return View(allUsers);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsersViewModel usersViewModel)
        {
            var user = await _userManager.FindByEmailAsync(usersViewModel.ApplicationUser.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already registered.";
                return View(usersViewModel);
            }
            if (!ModelState.IsValid)
            {
                return View(usersViewModel);
            }
            await _service.AddAsync(usersViewModel);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            var userDetails = await _service.GetByIdAsync(id);
            if (userDetails == null)
            {
                return View("NotFound");
            }
            return View(userDetails);
        }
        [Authorize]
        public async Task<IActionResult> Update(string id)
        {
            var userDetails = await _service.GetByIdAsync(id);
            if (userDetails == null)
            {
                return View("NotFound");
            }
            return View(userDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UsersViewModel usersViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(usersViewModel);
            }
            await _service.UpdateAsync(id, usersViewModel);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var userDetails = await _service.GetByIdAsync(id);
            if (userDetails == null)
            {
                return View("NotFound");
            }
            return View(userDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userDetails = await _service.GetByIdAsync(id);
            if (userDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRoles(string id)
        {
            UpdateUserRolesViewModel vm = new UpdateUserRolesViewModel()
            {
                Id = id,
                AllRoles = await _service.GetAllRoles()

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles(string id, UpdateUserRolesViewModel vm)
        {

            var user = await _userManager.FindByIdAsync(vm.Id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var currentRoles = await _userManager.GetRolesAsync(user);


            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            vm.AllRoles = await _service.GetAllRoles();
            var selectedRoles = vm.AllRoles.Where(r => vm.SelectedRoleIds.Contains(r.Id)).Select(r => r.Name).ToList();


            foreach (var role in selectedRoles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update user roles.");
                return View(vm);
            }

            return RedirectToAction("Index");
        }
    }


}

