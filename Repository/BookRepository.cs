using Microsoft.EntityFrameworkCore;
using mybookish.Database;
using mybookish.Models;
using System.Collections.Generic;
using System.Linq;

namespace mybookish.Repository
{
    public class BookRepository
    {
        private readonly LibraryContext _context = null;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }
        public int CreateBook(BookModel bookModel)
        {
            var book = new BookData()
            {
                Title = bookModel.Title,
                Author = bookModel.Author
            };
            _context.Books.Add(book);

            _context.SaveChanges();
            return book.Id;
        }
        public List<BookModel> GetListBooks()
        {
            var bookList = new List<BookModel>();
            var books = _context.Books.ToList();
            foreach (var book in books)
            {
                bookList.Add(new BookModel()
                {
                    Title = book.Title,
                    Author = book.Author,
                    Id = book.Id,
                });
            }
            return bookList;
        }

        public BookModel GetSingleBookById(int bookId)
        {
            var bookDb = _context.Books
                       .Where(b => b.Id == bookId)
                       .FirstOrDefault();

            var book = new BookModel()
            {
                Title = bookDb.Title,
                Author = bookDb.Author,
                Id = bookDb.Id,
            };

            return book;
        }

        public bool UpdateBookInDatabase(BookModel bookModel)
        {
            var book = new BookData()
            {
                Title = bookModel.Title,
                Author = bookModel.Author,
                Id = bookModel.Id,
            };

            var bookDb = _context.Books
                        .Where(b => b.Id == book.Id)
                        .FirstOrDefault();

            bookDb.Title = book.Title;
            bookDb.Author = book.Author;


            if (_context.Entry(bookDb).State != EntityState.Unchanged)
            {
                _context.SaveChanges();
                return true;
            }

            return false;



        }
    }
}
