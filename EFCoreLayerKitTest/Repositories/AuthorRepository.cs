using EFCoreLayerKit.Repositories;
using EFCoreLayerKitTest.Data;
using EFCoreLayerKitTest.Entities;

namespace EFCoreLayerKitTest.Repositories
{
    public class AuthorRepository : BaseRepository<Author>
    {
        public AuthorRepository(TestDbContext ctx) : base(ctx) { }
    }
}