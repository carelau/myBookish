using Microsoft.AspNetCore.Mvc;
using mybookish.Database;
using mybookish.Models;

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



    }
}
