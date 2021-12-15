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

        [Required]

        [StringLength(100, MinimumLength = 3)]
        public string Author { get; set; }
    }
}
