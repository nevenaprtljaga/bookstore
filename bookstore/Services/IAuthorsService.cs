using bookstore.Entities;

namespace bookstore.Services
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Author>> GetAll();
        Author GetById(int id);
        void Add(Author author);
        Author Update(int id, Author newAuthor);
        void Delete(int id);
    }
}
