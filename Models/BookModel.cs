using System;
using System.ComponentModel.DataAnnotations;

namespace mybookish.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public int AuthorId { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

    }
}
