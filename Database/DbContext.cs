
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace mybookish.Database
{
    public class LibraryContext : IdentityDbContext
    {
        public DbSet<BookData> Books { get; set; }
        public DbSet<AuthorData> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=mybookishDB;Trusted_Connection=True;");
        }
    }
}