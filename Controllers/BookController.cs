using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mybookish.Models;
using mybookish.Repository;

namespace mybookish.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly AuthorRepository _authorRepository;
        public BookController(BookRepository bookRepository, AuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        [Authorize]
        public IActionResult AddNewBook(bool isSuccess = false)
        {
            ViewBag.Author = new SelectList(_authorRepository.GetListAuthors(), "Id", "AuthorName");
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
            ViewBag.Author = new SelectList(_authorRepository.GetListAuthors(), "Id", "AuthorName");
            return View();

        }
        public IActionResult GetAllBooks(bool isDeleted = false, bool isAdded = false, bool isCopyDeleted = false)
        {

            ViewBag.isDeleted = isDeleted;
            ViewBag.isAdded = isAdded;
            ViewBag.isCopyDeleted = isCopyDeleted;
            var books = _bookRepository.GetListBooks();

            return View(books);
        }

        public IActionResult EditBook(int id)
        {
            var book = _bookRepository.GetSingleBookById(id);
            ViewBag.Author = new SelectList(_authorRepository.GetListAuthors(), "Id", "AuthorName");
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
                ViewBag.Author = new SelectList(_authorRepository.GetListAuthors(), "Id", "AuthorName");
                return View(bookModel);
            }
        }

        public IActionResult DeleteBook(int id)
        {
            var book = _bookRepository.GetSingleBookById(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult DeleteBook(BookModel bookModel)
        {
            _bookRepository.DeleteBookInDatabase(bookModel);

            return RedirectToAction("GetAllBooks", "Book", new { isDeleted = true });
        }

        public IActionResult SearchBook()
        {
            ViewBag.Author = new SelectList(_authorRepository.GetListAuthors(), "Id", "AuthorName");
            return View();
        }

        [HttpPost]
        public IActionResult SearchedBooks(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                var books = _bookRepository.SearchBookInDatabase(bookModel.Title, bookModel.AuthorId);
                ViewBag.isNull = false;
                if (books.Count == 0)
                {
                    ViewBag.isNull = true;
                }
                return View(books);
            }
            ViewBag.Author = new SelectList(_authorRepository.GetListAuthors(), "Id", "AuthorName");
            return View("SearchBook");
        }

        public IActionResult AddCopy(int id)
        {
            var book = _bookRepository.GetSingleBookById(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult AddCopy(BookModel bookModel)
        {
            _bookRepository.AddCopyInDatabase(bookModel);

            return RedirectToAction("GetAllBooks", "Book", new { isAdded = true });
        }

        public IActionResult DeleteCopy(int id)
        {
            var book = _bookRepository.GetSingleBookById(id);
            ViewBag.Message = false;
            if (book.TotalCopies <= 1 && book.AvailableCopies <= 1)
            {
                ViewBag.Message = true;
            }

            return View(book);
        }

        [HttpPost]
        public IActionResult DeleteCopy(BookModel bookModel)
        {
            _bookRepository.DeleteCopyInDatabase(bookModel);

            return RedirectToAction("GetAllBooks", "Book", new { isCopyDeleted = true });
        }
    }
}
