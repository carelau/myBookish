using System;

namespace mybookish.Database
{
    public class BookData
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public AuthorData Author { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }
    }
}
