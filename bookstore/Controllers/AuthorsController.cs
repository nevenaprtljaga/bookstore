﻿using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;
        public AuthorsController(AppDbContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            var allAuthors = _context.Authors.ToList();
            return View(allAuthors);
        }

    }
}