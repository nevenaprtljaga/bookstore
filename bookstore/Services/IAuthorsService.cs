using bookstore.Models;

namespace bookstore.Services
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorsViewModel>> GetAllAsync();
        Task<AuthorsViewModel> GetByIdAsync(int id);
        Task AddAsync(AuthorsViewModel authorsViewModel);
        Task<AuthorsViewModel> UpdateAsync(int id, AuthorsViewModel authorsViewModel);
        Task DeleteAsync(int id);
    }
}
