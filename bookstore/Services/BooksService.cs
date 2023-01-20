using bookstore.Models;
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

        public async Task<IEnumerable<BooksViewModel>> GetAll()
        {
            var result = await _context.Books
                .Join(
                    _context.Authors,
                    book => book.AuthorId,
                    author => author.Id,
                    (book, author) => new { book, author }
                ).Join(
                    _context.BookGenres,
                    o => o.book.BookGenreId,
                    bookGenre => bookGenre.Id,
                    (o, bookGenre) => new BooksViewModel { Book = o.book, BookGenre = bookGenre, Author = o.author}
                    ).
                ToListAsync();

            return result;
        }

        public BooksViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public BooksViewModel Update(int id, Book newBook)
        {
            throw new NotImplementedException();
        }
    }
}
