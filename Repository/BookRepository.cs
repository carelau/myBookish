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
                AuthorId = bookModel.AuthorId,
                TotalCopies = 1,
                AvailableCopies = 1,
            };
            _context.Books.Add(book);

            _context.SaveChanges();
            return book.Id;
        }
        public List<BookModel> GetListBooks()
        {
            return _context.Books.Select(x => new BookModel()
            {
                Title = x.Title,
                AuthorId = x.AuthorId,
                AuthorName = x.Author.AuthorName,
                Id = x.Id,
                TotalCopies = x.TotalCopies,
                AvailableCopies = x.AvailableCopies
            }).ToList();
        }

        public BookModel GetSingleBookById(int bookId)
        {
            return _context.Books
                     .Where(b => b.Id == bookId)
                     .Select(x => new BookModel()
                     {
                         Title = x.Title,
                         AuthorId = x.AuthorId,
                         AuthorName = x.Author.AuthorName,
                         Id = x.Id,
                         TotalCopies = x.TotalCopies,
                         AvailableCopies = x.AvailableCopies
                     })
                     .FirstOrDefault();
        }

        public bool UpdateBookInDatabase(BookModel bookModel)
        {
            var bookDb = _context.Books
                        .Where(b => b.Id == bookModel.Id)
                        .FirstOrDefault();

            bookDb.Title = bookModel.Title;
            bookDb.AuthorId = bookModel.AuthorId;

            if (_context.Entry(bookDb).State != EntityState.Unchanged)
            {
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public void DeleteBookInDatabase(BookModel bookModel)
        {
            var bookDb = _context.Books
                        .Where(b => b.Id == bookModel.Id)
                        .FirstOrDefault();

            _context.Books.Remove(bookDb);
            _context.SaveChanges();

        }
        public List<BookModel> SearchBookInDatabase(string bookName, int authorId)
        {
            return _context.Books
                                .Where(x => x.Title.StartsWith(bookName) && x.Author.Id == authorId)
                                .Select(x => new BookModel()
                                {
                                    Title = x.Title,
                                    AuthorId = x.AuthorId,
                                    AuthorName = x.Author.AuthorName,
                                    Id = x.Id,
                                    TotalCopies = x.TotalCopies,
                                    AvailableCopies = x.AvailableCopies
                                })
                                .ToList();
        }

        public void AddCopyInDatabase(BookModel bookModel)
        {
            var bookDb = _context.Books
                       .Where(b => b.Id == bookModel.Id)
                       .FirstOrDefault();

            bookDb.TotalCopies += 1;
            bookDb.AvailableCopies += 1;
            _context.SaveChanges();
        }
        public void DeleteCopyInDatabase(BookModel bookModel)
        {
            var bookDb = _context.Books
                       .Where(b => b.Id == bookModel.Id)
                       .FirstOrDefault();

            if (bookDb.TotalCopies <= 1 && bookDb.AvailableCopies <= 1)
            {
                _context.Books.Remove(bookDb);
            }
            bookDb.TotalCopies -= 1;
            bookDb.AvailableCopies -= 1;
            _context.SaveChanges();
        }
    }
}