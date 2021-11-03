using Meditec.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Meditec.Data
{
    public interface ILibraryDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
