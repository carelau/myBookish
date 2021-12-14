
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mybookish.Database;
using mybookish.Models;
using mybookish.Repository;

namespace mybookish.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult AddNewBook(bool isSuccess = false)
        {
            ViewBag.isSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewBook(BookModel bookModel)
        {

            if (ModelState.IsValid)
            {
                int id = _bookRepository.CreateBook(bookModel);

                if (id > 0)
                {
                    return RedirectToAction("AddNewBook", "Book", new { isSuccess = true });
                }
            }
            return View();

        }

        public IActionResult GetAllBooks()
        {

            var books = _bookRepository.GetListBooks();

            return View(books);
        }

        public IActionResult EditBook(int id)
        {
            var book = _bookRepository.GetSingleBookById(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult EditBook(BookModel bookModel)
        {
            bool isUpdate = _bookRepository.UpdateBookInDatabase(bookModel);

            if (isUpdate == true)

            { return RedirectToAction("GetAllBooks", "Book"); }

            else
            {
                return View(bookModel);
            }

        }
    }
}
