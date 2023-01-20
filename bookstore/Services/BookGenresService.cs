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

        public void Add(BookGenre bookGenre)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookGenresViewModel>> GetAll()
        {
            List<BookGenresViewModel> vm = new List<BookGenresViewModel>();
            var result = await _context.BookGenres.ToListAsync();
            foreach(var item in result)
            {
                vm.Add(new BookGenresViewModel
                {
                    BookGenre = item
                });
            }
            return vm;
        }

        public BookGenresViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public BookGenresViewModel Update(int id, BookGenre newBookGenre)
        {
            throw new NotImplementedException();
        }
    }
}
