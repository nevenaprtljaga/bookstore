using bookstore.Entities;
using bookstore.Models;

namespace bookstore.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersViewModel>> GetAllAsync();
        Task<UsersViewModel> GetByIdAsync(string id);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<UpdateUserRolesViewModel> UpdateUserRoles(UpdateUserRolesViewModel vm);
        Task<UsersViewModel> AddAsync(UsersViewModel usersViewModel);
        Task<UsersViewModel> UpdateAsync(string id, UsersViewModel usersViewModel);
        Task<string> DeleteAsync(string id);
    }
}
