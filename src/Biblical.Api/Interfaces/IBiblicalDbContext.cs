using Biblical.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Biblical.Api.Interfaces
{
    public interface IBiblicalDbContext
    {
        DbSet<Book> Books { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
