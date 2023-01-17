using bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services
{
    public class BooksService : IBooksService
    {
        private readonly AppDbContext _context;

        public BooksService(AppDbContext context) { _context = context; }
        public void Add(Book book)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var result = await _context.Books.Include(a => a.Author).Include(bg => bg.BookGenre).ToListAsync();
            return result;
        }

        public Book GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Book Update(int id, Book newBook)
        {
            throw new NotImplementedException();
        }
    }
}
