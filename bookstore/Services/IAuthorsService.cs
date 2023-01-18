using bookstore.Entities;

namespace bookstore.Services
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task<Author> UpdateAsync(int id, Author newAuthor);
        Task DeleteAsync(int id);
    }
}
