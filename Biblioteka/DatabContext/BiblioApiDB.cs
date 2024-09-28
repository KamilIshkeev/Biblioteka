 using Microsoft.EntityFrameworkCore;
using Biblioteka.Model;


namespace Biblioteka.DatabContext
{
    public class BiblioApiDB: DbContext
    {
        public BiblioApiDB(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Reader> Reader1 { get; set; }
        public DbSet<Book> Book { get; set; }
            public DbSet<Reader> Reader { get; set; }
            public DbSet<Genre> Genre { get; set; }
            public DbSet<Rental> Rental { get; set; }
        
    }



}