
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

            int id = _bookRepository.CreateBook(bookModel);
            
            if (id > 0)
            {
                return RedirectToAction("AddNewBook", "Book", new { isSuccess = true});
            }
            return View();
        }

        public string GetAllBooks()
        {
            return "All books";
        }

    }
}
