using Microsoft.EntityFrameworkCore;
using PhotoService.Model;

namespace PhotoService.DatabContext
{
    public class PhotoDbContext : DbContext
    {

        public PhotoDbContext(DbContextOptions<PhotoDbContext> options) : base(options)
        {
        }
        public DbSet<Photo> Photos { get; set; }


    }
}
