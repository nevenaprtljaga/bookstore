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

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var allUsers = await _service.GetAllAsync();
            return View(allUsers);
        }

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

        public async Task<IActionResult> Details(string id)
        {
            var userDetails = await _service.GetByIdAsync(id);
            if (userDetails == null)
            {
                return View("NotFound");
            }
            return View(userDetails);
        }

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
        public async Task<IActionResult> UpdateUserRoles(string id)
        {
            UpdateUserRolesViewModel vm = new UpdateUserRolesViewModel(id, _service.GetAllRoles, _service.GetAllRoles());
            return View(vm);
        }


    }
}
