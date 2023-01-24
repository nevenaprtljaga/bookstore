using bookstore.Models;

namespace bookstore.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<BooksViewModel>> GetAll();
        Task<BooksViewModel> GetByIdAsync(int id);
        Task AddAsync(NewBookViewModel newBook);
        Task<BooksViewModel> UpdateAsync(NewBookViewModel newBook);
        Task DeleteAsync(int id);
        Task<BookDropdownViewModel> GetBookDropdownValues();
    }
}
