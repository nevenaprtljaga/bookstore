using bookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _service;
        public AuthorsController(IAuthorsService service) {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allAuthors = await _service.GetAll();
            return View(allAuthors);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

    }
}
