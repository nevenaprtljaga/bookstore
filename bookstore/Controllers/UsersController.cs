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

        /*  public IActionResult Create()
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
          }*/

        public async Task<IActionResult> Details(string id)
        {
            var userDetails = await _service.GetByIdAsync(id);
            if (userDetails == null)
            {
                return View("NotFound");
            }
            return View(userDetails);
        }
        /*
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

  */
        /*   public async Task<IActionResult> Delete(string id)
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
           }*/
        public async Task<IActionResult> UpdateUserRoles(string id)
        {
            UpdateUserRolesViewModel vm = new UpdateUserRolesViewModel(id, _service.GetAllRoles, _service.GetAllRoles());
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRolesViewModel model)
        {
            // Get the user from the database
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Get the current user roles
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove the user from any roles they no longer belong to
            var rolesToRemove = currentRoles.Except(model.AllRoles.Where(x => model.SelectedRoleIds.Contains(x.Id)).Select(x => x.Name));
            foreach (var role in rolesToRemove)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            // Add the user to any new roles
            var rolesToAdd = model.AllRoles.Where(x => model.SelectedRoleIds.Contains(x.Id)).Select(x => x.Name).Except(currentRoles);
            foreach (var role in rolesToAdd)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            // Save the changes to the database
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update user roles.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

    }
}
