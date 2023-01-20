using bookstore.Entities;
using bookstore.Models;

namespace bookstore.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<BooksViewModel>> GetAll();
        BooksViewModel GetById(int id);
        void Add(Book book);
        BooksViewModel Update(int id, Book newBook);
        void Delete(int id);
    }
}
