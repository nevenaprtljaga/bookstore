using bookstore.Entities;
using bookstore.Models;
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

        public async Task AddAsync(BookGenresViewModel bookGenreViewModel)
        {
            var newBookGenre = new BookGenre()
            {
                Name = bookGenreViewModel.BookGenre.Name,
                Description = bookGenreViewModel.BookGenre.Description
            };
            await _context.BookGenres.AddAsync(newBookGenre);
            await _context.SaveChangesAsync();
        }
        public async Task<BookGenresViewModel> GetByIdAsync(int id)
        {
            var result = await _context.BookGenres.FirstOrDefaultAsync(n => n.Id == id);
            return new BookGenresViewModel { BookGenre = result };
        }
        public async Task DeleteAsync(int id)
        {
            var result = await _context.BookGenres.FirstOrDefaultAsync(n => n.Id == id);
            _context.BookGenres.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookGenresViewModel>> GetAll()
        {
            List<BookGenresViewModel> vm = new List<BookGenresViewModel>();
            var result = await _context.BookGenres.ToListAsync();
            foreach (var item in result)
            {
                vm.Add(new BookGenresViewModel
                {
                    BookGenre = item
                });
            }
            return vm;
        }

        public async Task<BookGenresViewModel> UpdateAsync(int id, BookGenresViewModel bookGenreViewModel)
        {
            var newBookGenre = new BookGenre()
            {
                Id = id,
                Name = bookGenreViewModel.BookGenre.Name,
                Description = bookGenreViewModel.BookGenre.Description
            };
            _context.Update(newBookGenre);
            await _context.SaveChangesAsync();
            return new BookGenresViewModel { BookGenre = newBookGenre };
        }
    }
}
