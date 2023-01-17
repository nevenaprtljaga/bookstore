using bookstore.Entities;

namespace bookstore.Services
{
    public interface IBookGenresService
    {
        Task<IEnumerable<BookGenre>> GetAll();
        BookGenre GetById(int id);
        void Add(BookGenre bookGenre);
        BookGenre Update(int id, BookGenre newBookGenre);
        void Delete(int id);
    }
}
