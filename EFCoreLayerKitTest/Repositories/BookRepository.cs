using EFCoreLayerKit.Repositories;
using EFCoreLayerKitTest.Data;
using EFCoreLayerKitTest.Entities;

namespace EFCoreLayerKitTest.Repositories
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(TestDbContext ctx) : base(ctx) { }
    }
}