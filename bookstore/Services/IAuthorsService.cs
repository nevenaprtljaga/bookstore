using bookstore.Entities;
using bookstore.Models;

namespace bookstore.Services
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorsViewModel>> GetAllAsync();
        Task<AuthorsViewModel> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task<AuthorsViewModel> UpdateAsync(int id, Author newAuthor);
        Task DeleteAsync(int id);
    }
}
