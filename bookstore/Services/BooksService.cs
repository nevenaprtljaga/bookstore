using bookstore.Entities;
using bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services
{
    public class BooksService : IBooksService
    {

        private readonly AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NewBookViewModel book)
        {

            var newBook = new Book()
            {
                Title = book.Title,
                ImageURL = book.ImageURL,
                Price = book.Price,
                YearOfPublication = book.YearOfPublication,
                AuthorId = book.AuthorId,
                BookGenreId = book.BookGenreId
            };
            await _context.Books.AddAsync(newBook);
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
                    _context.BookInfos,
                    o => o.book.Id,
                    bookInfo => bookInfo.BookId,
                    (o, bookInfo) => new { o.book, bookInfo, o.author }
                ).Join(
                    _context.BookGenres,
                    n => n.book.BookGenreId,
                    bookGenre => bookGenre.Id,
                    (n, bookGenre) => new BooksViewModel { Book = n.book, BookGenre = bookGenre, Author = n.author, BookInfo = n.bookInfo }
                    ).
                ToListAsync();

            return result;
        }


        public async Task<BooksViewModel> UpdateAsync(NewBookViewModel data)
        {
            var dbBook = await _context.Books.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbBook != null)
            {
                dbBook.Title = data.Title;
                dbBook.ImageURL = data.ImageURL;
                dbBook.Price = data.Price;
                dbBook.YearOfPublication = data.YearOfPublication;
                dbBook.AuthorId = data.AuthorId;
                dbBook.BookGenreId = data.BookGenreId;
                await _context.SaveChangesAsync();
            }


            await _context.SaveChangesAsync();
            return new BooksViewModel { Book = dbBook };
        }

        public async Task<BookDropdownViewModel> GetBookDropdownValues()
        {
            var response = new BookDropdownViewModel()
            {
                Authors = await _context.Authors.ToListAsync(),
                BookGenres = await _context.BookGenres.ToListAsync()
            };

            return response;
        }


    }
}
