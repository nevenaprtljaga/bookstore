using bookstore.Entities;
using bookstore.Models;

namespace bookstore.Services
{
    public interface IBookGenresService
    {
        Task<IEnumerable<BookGenresViewModel>> GetAll();
        Task<BookGenresViewModel> GetByIdAsync(int id);
        Task AddAsync(BookGenre bookGenre);
        Task<BookGenresViewModel> UpdateAsync(int id, BookGenre newBookGenre);
        Task DeleteAsync(int id);
    }
}
