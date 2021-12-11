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
                    Author = book.Author
                });
            }

            return bookList;
        }

    }
}
