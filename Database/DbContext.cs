
using Microsoft.EntityFrameworkCore;


namespace mybookish.Database
{
    public class LibraryContext : DbContext
    {
        public DbSet<BookData> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=mybookishDB;Trusted_Connection=True;");
        }
    }
}