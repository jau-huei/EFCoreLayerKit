using EFCoreLayerKit.Data;
using Microsoft.EntityFrameworkCore;
using EFCoreLayerKitTest.Entities;

namespace EFCoreLayerKitTest.Data
{
    public class TestDbContext : BaseDbContext
    {
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
    }
}