using bookstore.Models;

namespace bookstore.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersViewModel>> GetAllAsync();
        Task<UsersViewModel> GetByIdAsync(string id);
        Task<IEnumerable<RolesViewModel>> GetAllRoles();
        /*    Task AddAsync(UsersViewModel usersViewModel);
           Task<UsersViewModel> UpdateAsync(string id, UsersViewModel usersViewModel);
           Task DeleteAsync(string id);*/
    }
}
