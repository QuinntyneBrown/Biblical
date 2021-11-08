using Biblical.Api.Models;
using Biblical.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblical.Api.Data
{
    public class BiblicalDbContext : DbContext, IBiblicalDbContext
    {
        public DbSet<Book> Books { get; private set; }
        public BiblicalDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BiblicalDbContext).Assembly);
        }

    }
}
