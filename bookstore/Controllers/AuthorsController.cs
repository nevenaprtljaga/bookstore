using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;
        public AuthorController(AppDbContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Authors.ToList();
            return View(data);
        }

    }
}
