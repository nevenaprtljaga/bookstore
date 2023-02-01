/*using bookstore.Models;
using bookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _service;
        public UsersController(IUsersService service)
        {
            _service = service;
        }
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


    }
}
*/