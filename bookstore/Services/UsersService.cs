using bookstore.Entities;
using bookstore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UsersService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<UsersViewModel> AddAsync(UsersViewModel usersViewModel)
        {
            var role = _roleManager.Roles.FirstOrDefault((n => n.Name == "Customer"));
            var user = new ApplicationUser()
            {
                UserName = usersViewModel.ApplicationUser.UserName,
                FullName = usersViewModel.ApplicationUser.FullName,
                Email = usersViewModel.ApplicationUser.Email,
                PhoneNumber = usersViewModel.ApplicationUser.PhoneNumber

            };
            string pass = randomPass(user);
            var newUserResponse = await _userManager.CreateAsync(user, pass);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, new[] { role.Name });
            }
            return new UsersViewModel { ApplicationUser = user };
        }
        public string randomPass(ApplicationUser ApplicationUser)
        {//username prvo veliko slovo + @ + prve tri cifre broja telefona   Nevenap@123
            return ApplicationUser.UserName.ToUpper().Substring(0, 1) + ApplicationUser.UserName.Substring(1) + "@" + ApplicationUser.PhoneNumber.ToString().Substring(0, 3);
        }
        public async Task<string> DeleteAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return user != null ? await DeleteUserAsync(user) : "success";
        }

        public async Task<string> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.ToString();
        }

        public async Task<IEnumerable<UsersViewModel>> GetAllAsync()
        {
            List<UsersViewModel> vm = new List<UsersViewModel>();
            var result = await _userManager.Users.ToListAsync();


            foreach (var item in result)
            {
                var currentRoles = await _userManager.GetRolesAsync(item);
                vm.Add(new UsersViewModel
                {
                    ApplicationUser = item,
                    ListOfRoles = string.Join(", ", currentRoles)
                });
            }



            return vm;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            List<RolesViewModel> vm = new List<RolesViewModel>();
            var result = await _context.Roles.ToListAsync();
            /*   foreach (var item in result)
               {
                   vm.Add(new RolesViewModel//automapper ubaci
                   {
                       Role = item
                   });
               }*/
            return result;
        }
        public async Task<UpdateUserRolesViewModel> UpdateUserRoles(UpdateUserRolesViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id);
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var selectedRoles = vm.AllRoles
                .Where(r => vm.SelectedRoleIds.Contains(r.Id))
                .Select(r => r.Name);
            await _userManager.AddToRolesAsync(user, selectedRoles);
            return vm;
        }

        public async Task<UsersViewModel> GetByIdAsync(string id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(n => n.Id == id);
            return new UsersViewModel { ApplicationUser = result };
        }

        public async Task<UsersViewModel> UpdateAsync(string id, UsersViewModel usersViewModel)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            user.UserName = usersViewModel.ApplicationUser.UserName;
            user.FullName = usersViewModel.ApplicationUser.FullName;
            user.Email = usersViewModel.ApplicationUser.Email;
            user.PhoneNumber = usersViewModel.ApplicationUser.PhoneNumber;
            user.EmailConfirmed = usersViewModel.ApplicationUser.EmailConfirmed;

            await _userManager.UpdateAsync(user);
            return new UsersViewModel { ApplicationUser = user };
        }
    }
}
