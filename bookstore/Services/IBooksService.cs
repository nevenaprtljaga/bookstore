using bookstore.Entities;

namespace bookstore.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAll();
        Book GetById(int id);
        void Add(Book book);
        Book Update(int id, Book newBook);
        void Delete(int id);
    }
}
