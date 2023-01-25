using bookstore.Models;

namespace bookstore.Services
{
    public interface IBookGenresService
    {
        Task<IEnumerable<BookGenresViewModel>> GetAll();
        Task<BookGenresViewModel> GetByIdAsync(int id);
        Task AddAsync(BookGenresViewModel bookGenreViewModel);
        Task<BookGenresViewModel> UpdateAsync(int id, BookGenresViewModel bookGenreViewModel);
        Task DeleteAsync(int id);
    }
}
