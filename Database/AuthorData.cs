using System;
using System.Collections.Generic;

namespace mybookish.Database
{
    public class AuthorData
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public ICollection<BookData> Books {get;set;}
    }
}