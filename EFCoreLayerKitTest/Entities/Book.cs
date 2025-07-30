using EFCoreLayerKit.Entities;

namespace EFCoreLayerKitTest.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public long AuthorId { get; set; }
        public virtual Author Author { get; set; } = null!;
    }
}