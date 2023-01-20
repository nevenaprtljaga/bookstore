using bookstore.Entities;
using bookstore.Models;

namespace bookstore.Services
{
    public interface IBookGenresService
    {
        Task<IEnumerable<BookGenresViewModel>> GetAll();
        BookGenresViewModel GetById(int id);
        void Add(BookGenre bookGenre);
        BookGenresViewModel Update(int id, BookGenre newBookGenre);
        void Delete(int id);
    }
}
