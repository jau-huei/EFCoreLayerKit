[简体中文](https://github.com/jau-huei/EFCoreLayerKit/blob/master/README.md) | [English](https://github.com/jau-huei/EFCoreLayerKit/blob/master/README.en.md)

[![NuGet](https://img.shields.io/nuget/v/EFCoreLayerKit.svg?label=NuGet)](https://www.nuget.org/packages/EFCoreLayerKit)
[![NuGet Downloads](https://img.shields.io/nuget/dt/EFCoreLayerKit)](https://www.nuget.org/packages/EFCoreLayerKit)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://licenses.nuget.org/MIT)
[![GitHub stars](https://img.shields.io/github/stars/jau-huei/EFCoreLayerKit?style=social)](https://github.com/jau-huei/EFCoreLayerKit)


# EFCoreLayerKit

EFCoreLayerKit is a general-purpose data access layer toolkit based on Entity Framework Core. It supports automatic repository registration, DTO mapping, soft deletion, automatic migration, and more. Suitable for .NET 8+ projects.

---

## Features
- Automatically register all DbContexts inheriting from `BaseDbContext` and migrate database schema
- Automatically register all repositories inheriting from `BaseRepository<TEntity>`
- Support for soft delete, optimistic concurrency, batch operations, and other common data access patterns
- Automatic two-way mapping between DTOs and entities (based on AutoMapper)
- Rich result types (`FResult`, `FResult<T>`, `FPagedResult<T>`)
- Designed for DI scenarios such as Blazor and ASP.NET Core

---

## Installation

Make sure your project references the following NuGet packages:
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- AutoMapper
- System.Linq.Dynamic.Core
- EFCoreLayerKit (this library)

---

## Quick Start

1. **Define Entities and DbContext**
public class Author : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}

public class Book : BaseEntity
{
    public string Title { get; set; }
    public long AuthorId { get; set; }
    public virtual Author Author { get; set; }
}

public class TestDbContext : BaseDbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}
2. **Define Repository Classes**
public class AuthorRepository : BaseRepository<Author>
{
    public AuthorRepository(TestDbContext ctx) : base(ctx) { }
}

public class BookRepository : BaseRepository<Book>
{
    public BookRepository(TestDbContext ctx) : base(ctx) { }
}
3. **Register Services and Auto-Migrate**

In `Program.cs`:
var services = new ServiceCollection();
services.AddEFCoreLayerKit();
var provider = services.BuildServiceProvider();
All DbContexts will automatically call `EnsureDatabaseMigrated()` during registration, so you do not need to manually migrate.

4. **Use Repositories for Data Operations**
var authorRepo = provider.GetRequiredService<AuthorRepository>();
var bookRepo = provider.GetRequiredService<BookRepository>();

// Add data
var author = new Author { Name = "Zhang San" };
var addAuthorResult = await authorRepo.AddAsync(author);

var book = new Book { Title = "C# Beginner", AuthorId = author.Id };
var addBookResult = await bookRepo.AddAsync(book);

// Query
authors = await authorRepo.GetAllAsync();

// Update
author.Name = "Li Si";
var updateAuthorResult = await authorRepo.UpdateAsync(author);

// Delete
var deleteAuthorResult = await authorRepo.DeleteAsync(author.Id);
---

## Result Types

- `FResult`: General operation result, including success flag, message, error code, etc.
- `FResult<T>`: Operation result with data.
- `FPagedResult<T>`: Paged result with data and pagination info.

All result types support formatted messages, error codes, and log conversion.

---

## Advanced Usage
- Custom repositories, DTO mapping, global query filters
- Batch operations, soft delete, optimistic concurrency
- See source code comments and extension methods for more

---

## Testing & Examples

A complete test project (`EFCoreLayerKitTest`) is included. You can run `Program.cs` to see CRUD and other features in action.

---

## License
MIT
