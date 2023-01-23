﻿using bookstore.Entities;
using bookstore.Models;
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
        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Authors.FirstOrDefaultAsync(n => n.Id == id);
            _context.Authors.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorsViewModel>> GetAllAsync()
        {
            List<AuthorsViewModel> vm = new List<AuthorsViewModel>();
            var result = await _context.Authors.ToListAsync();
            foreach (var item in result)
            {
                vm.Add(new AuthorsViewModel
                {
                    Author = item
                });
            }
            return vm;
        }

        public async Task<AuthorsViewModel> GetByIdAsync(int id)
        {
            var result = await _context.Authors.FirstOrDefaultAsync(n => n.Id == id);
            return new AuthorsViewModel { Author = result };
        }

        public async Task<AuthorsViewModel> UpdateAsync(int id, Author newAuthor)
        {
            _context.Authors.Update(newAuthor);
            await _context.SaveChangesAsync();
            return new AuthorsViewModel { Author = newAuthor };
        }
    }
}
