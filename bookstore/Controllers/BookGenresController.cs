using bookstore.Models;
using bookstore.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Index()
        {
            var allBookGenres = await _service.GetAll();
            return View(allBookGenres);
        }
        [Authorize(Roles = "Librarian")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookGenresViewModel bookGenreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookGenreViewModel);
            }
            await _service.AddAsync(bookGenreViewModel);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> Details(int id)
        {
            var bookGenreDetails = await _service.GetByIdAsync(id);
            if (bookGenreDetails == null)
            {
                return View("NotFound");
            }
            return View(bookGenreDetails);
        }
        [Authorize(Roles = "Librarian")]
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
        public async Task<IActionResult> Update(int id, BookGenresViewModel bookGenreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookGenreViewModel);
            }
            await _service.UpdateAsync(id, bookGenreViewModel);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Librarian")]
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
