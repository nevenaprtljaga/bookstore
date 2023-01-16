using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class BookGenresController : Controller
    {
        private readonly AppDbContext _context;

        public BookGenresController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var allBookGenres = _context.BookGenres.ToList();
            return View(allBookGenres);
        }
    }
}
