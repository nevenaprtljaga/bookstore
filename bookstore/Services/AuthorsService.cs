using bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Services
{
    public class AuthorsService:IAuthorsService
    {
        private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Author author)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var result = await _context.Authors.ToListAsync();
            return result;
        }

        public Author GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Author Update(int id, Author newAuthor)
        {
            throw new NotImplementedException();
        }
    }
}
