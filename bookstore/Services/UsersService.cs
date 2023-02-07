﻿using bookstore.Entities;
using bookstore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /*  public async Task AddAsync(UsersViewModel usersViewModel)
          {
              var newUser = new ApplicationUser()
              {
                  UserName = usersViewModel.ApplicationUser.UserName,
                  FullName = usersViewModel.ApplicationUser.FullName,
                  Email = usersViewModel.ApplicationUser.Email,
                  PhoneNumber = usersViewModel.ApplicationUser.PhoneNumber
              };
              await _userManager.Users.AddAsync(newUser);
              await _userManager.SaveChangesAsync();

          }

          public async Task DeleteAsync(string id)
          {
              var result = await _userManager.Users.FirstOrDefaultAsync(n => n.Id == id);
              _context.Users.Remove(result);
              await _userManager.SaveChangesAsync();
          }
  */
        public async Task<IEnumerable<UsersViewModel>> GetAllAsync()
        {
            List<UsersViewModel> vm = new List<UsersViewModel>();
            var result = await _userManager.Users.ToListAsync();
            foreach (var item in result)
            {
                vm.Add(new UsersViewModel
                {
                    ApplicationUser = item
                });
            }
            return vm;
        }

        public async Task<IEnumerable<RolesViewModel>> GetAllRoles()
        {
            List<RolesViewModel> vm = new List<RolesViewModel>();
            var result = await _context.Roles.ToListAsync();
            foreach (var item in result)
            {
                vm.Add(new RolesViewModel
                {
                    Role = item
                });
            }
            return vm;
        }

        public async Task<UsersViewModel> GetByIdAsync(string id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(n => n.Id == id);
            return new UsersViewModel { ApplicationUser = result };
        }

        /*     public async Task<UsersViewModel> UpdateAsync(string id, UsersViewModel usersViewModel)
             {
                 var newUser = new ApplicationUser()
                 {
                     Id = id,
                     UserName = usersViewModel.ApplicationUser.UserName,
                     FullName = usersViewModel.ApplicationUser.FullName,
                     Email = usersViewModel.ApplicationUser.Email,
                     PhoneNumber = usersViewModel.ApplicationUser.PhoneNumber
                 };
                 _userManager.Users.Update(newUser);
                 await _userManager.SaveChangesAsync();
                 return new UsersViewModel { ApplicationUser = newUser };
             }*/
    }
}
