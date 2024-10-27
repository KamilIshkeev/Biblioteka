using Microsoft.EntityFrameworkCore;
using ReaderServ.Model;

namespace ReaderServ.DatabContext
{
    public class ReaderDbContext : DbContext
    {
        public ReaderDbContext(DbContextOptions<ReaderDbContext> options) : base(options) { }
        public DbSet<Readers> Readers { get; set; } = null!;

    }
}