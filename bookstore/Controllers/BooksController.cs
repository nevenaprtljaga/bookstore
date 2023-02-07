using bookstore.Models;
using bookstore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Create()
        {
            var bookDropdownsData = await _service.GetBookDropdownValues();


            ViewBag.BookGenres = new SelectList(bookDropdownsData.BookGenres, "Id", "Name");
            ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewBookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropdownsData = await _service.GetBookDropdownValues();

                ViewBag.BookGenres = new SelectList(bookDropdownsData.BookGenres, "Id", "Name");
                ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "FullName");
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
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var response = new NewBookViewModel()
            {
                Id = bookDetails.Book.Id,
                Title = bookDetails.Book.Title,
                ImageURL = bookDetails.Book.ImageURL,
                Price = bookDetails.Book.Price,
                YearOfPublication = bookDetails.Book.YearOfPublication,
                AuthorId = bookDetails.Book.AuthorId,
                BookGenreId = bookDetails.Book.BookGenreId,
            };

            var bookDropdownsData = await _service.GetBookDropdownValues();
            ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "FullName");
            ViewBag.BookGenres = new SelectList(bookDropdownsData.BookGenres, "Id", "Name");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, NewBookViewModel book)
        {
            if (id != book.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var bookDropdownData = await _service.GetBookDropdownValues();

                ViewBag.Authors = new SelectList(bookDropdownData.Authors, "Id", "FullName");
                ViewBag.BookGenres = new SelectList(bookDropdownData.BookGenres, "Id", "Name");

                return View(book);
            }

            await _service.UpdateAsync(book);
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
