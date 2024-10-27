using Microsoft.EntityFrameworkCore;
using PhotoService.Models;

namespace PhotoService.Data
{
    public class PhotoDbContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }

        public PhotoDbContext(DbContextOptions<PhotoDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add any custom model configurations here if needed.  For example:
            modelBuilder.Entity<Photo>()
                .Property(p => p.DataUrl)
                .HasMaxLength(255); // Adjust length as needed.
        }
    }
}












//using Microsoft.EntityFrameworkCore;
//using PhotoService.Model;

//namespace PhotoService.Data
//{
//    public class PhotoDbContext : DbContext
//    {
//        public PhotoDbContext(DbContextOptions options) : base(options)
//        {
//        }

//        public DbSet<Photo1> Photos { get; set; }
//    }
//}

