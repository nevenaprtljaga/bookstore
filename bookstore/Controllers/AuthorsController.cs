﻿using bookstore.Entities;
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
            var allAuthors = await _service.GetAllAsync();
            return View(allAuthors);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _service.AddAsync(author);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if(authorDetails == null)
            {
                return View("NotFound");
            }
            return View(authorDetails);
        }

        public async Task<IActionResult> Update(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null)
            {
                return View("NotFound");
            }
            return View(authorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _service.UpdateAsync(id, author);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null)
            {
                return View("NotFound");
            }
            return View(authorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);
            if (authorDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
