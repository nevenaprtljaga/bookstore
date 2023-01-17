using bookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class BookGenresController : Controller
    {
        private readonly IBookGenresService _service;
        public BookGenresController(IBookGenresService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allBookGenres = await _service.GetAll();
            return View(allBookGenres);
        }
    }
}
