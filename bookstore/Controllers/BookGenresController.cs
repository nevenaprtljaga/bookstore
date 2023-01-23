using bookstore.Entities;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookGenre bookGenre)
        {
            if (!ModelState.IsValid)
            {
                return View(bookGenre);
            }
            await _service.AddAsync(bookGenre);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var bookGenreDetails = await _service.GetByIdAsync(id);
            if (bookGenreDetails == null)
            {
                return View("NotFound");
            }
            return View(bookGenreDetails);
        }

        public async Task<IActionResult> Update(int id)
        {
            var bookGenreDetails = await _service.GetByIdAsync(id);
            if (bookGenreDetails == null)
            {
                return View("NotFound");
            }
            return View(bookGenreDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BookGenre bookGenre)
        {
            if (!ModelState.IsValid)
            {
                return View(bookGenre);
            }
            await _service.UpdateAsync(id, bookGenre);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bookGenreDetails = await _service.GetByIdAsync(id);
            if (bookGenreDetails == null)
            {
                return View("NotFound");
            }
            return View(bookGenreDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookGenreDetails = await _service.GetByIdAsync(id);
            if (bookGenreDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
