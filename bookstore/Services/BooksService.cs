using bookstore.Entities;
using bookstore.Models;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace bookstore.Services
{
    public class BooksService : IBooksService
    {

        private readonly AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<BooksViewModel> GetByIdAsync(int id)
        {
            var result = _context.Books
                .Join(
                    _context.Authors,
                    book => book.AuthorId,
                    author => author.Id,
                    (book, author) => new { book, author }
                ).Join(
                    _context.BookGenres,
                    o => o.book.BookGenreId,
                    bookGenre => bookGenre.Id,
                    (o, bookGenre) => new BooksViewModel { Book = o.book, BookGenre = bookGenre, Author = o.author }
                    ).
                    Where(bwm => bwm.Book.Id == id)
                    .FirstOrDefault();
            

            return result;
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.Books.FirstOrDefaultAsync(n => n.Id == id);
            _context.Books.Remove(result);
            await _context.SaveChangesAsync();
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
                    (o, bookGenre) => new BooksViewModel { Book = o.book, BookGenre = bookGenre, Author = o.author }
                    ).
                ToListAsync();

            return result;
        }

        public async Task<BooksViewModel> UpdateAsync(int id, Book newBook)
        {
            newBook.Id = id;
            _context.Update(newBook);
            await _context.SaveChangesAsync();
            return new BooksViewModel { Book = newBook };
        }
    }
}
