using Microsoft.EntityFrameworkCore;
using mybookish.Database;
using mybookish.Models;
using System.Collections.Generic;
using System.Linq;

namespace mybookish.Repository
{
    public class AuthorRepository
    {
        private readonly LibraryContext _context = null;

        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }
        public int AddAuthor(AuthorModel authorModel)
        {
            var author = new AuthorData()
            {
                AuthorName = authorModel.AuthorName
            };
            _context.Authors.Add(author);

            _context.SaveChanges();
            return author.Id;
        }
        public List<AuthorModel> GetListAuthors()
        {
            return _context.Authors.Select(x => new AuthorModel()
            {
                AuthorName = x.AuthorName,
                Id = x.Id,
            }).ToList();
        }

    }
}