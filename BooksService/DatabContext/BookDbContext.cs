using BooksService.Model;
using Microsoft.EntityFrameworkCore;

namespace BooksService.DatabContext
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}