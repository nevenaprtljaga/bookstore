using bookstore.Entities;
using bookstore.Models;

namespace bookstore.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<BooksViewModel>> GetAll();
        Task<BooksViewModel> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task<BooksViewModel> UpdateAsync(int id, Book newBook);
        Task DeleteAsync(int id);
    }
}
