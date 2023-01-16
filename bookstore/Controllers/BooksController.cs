using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var allBooks = _context.Books.ToList();
            return View(allBooks);
        }
    }
}
