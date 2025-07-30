using EFCoreLayerKit.Entities;
using System.Collections.Generic;

namespace EFCoreLayerKitTest.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}