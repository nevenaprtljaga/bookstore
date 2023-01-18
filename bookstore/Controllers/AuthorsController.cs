using bookstore.Entities;
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

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Surname,DateOfBirth")]Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _service.AddAsync(author);
            return RedirectToAction(nameof(Index));
        }



    }
}
