using bookstore.Entities;
using bookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _service;
        public BooksController(IBooksService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAll();
            return View(allBooks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            await _service.AddAsync(book);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null)
            {
                return View("NotFound");
            }
            return View(bookDetails);
        }

        public async Task<IActionResult> Update(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null)
            {
                return View("NotFound");
            }
            return View(bookDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            await _service.UpdateAsync(id, book);
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
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
