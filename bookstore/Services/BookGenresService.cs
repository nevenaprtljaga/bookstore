using bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services
{
    public class BookGenresService : IBookGenresService
    {

        private readonly AppDbContext _context;

        public BookGenresService(AppDbContext context)
        {
            _context = context;
        }

        public void Add(BookGenre bookGenre)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookGenre>> GetAll()
        {
            var result = await _context.BookGenres.ToListAsync();
            return result;
        }

        public BookGenre GetById(int id)
        {
            throw new NotImplementedException();
        }

        public BookGenre Update(int id, BookGenre newBookGenre)
        {
            throw new NotImplementedException();
        }
    }
}
