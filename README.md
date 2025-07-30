[English](https://github.com/jau-huei/EFCoreLayerKit/blob/master/README.en.md) | [简体中文](https://github.com/jau-huei/EFCoreLayerKit/blob/master/README.md)

[![NuGet](https://img.shields.io/nuget/v/EFCoreLayerKit.svg?label=NuGet)](https://www.nuget.org/packages/EFCoreLayerKit)
[![NuGet Downloads](https://img.shields.io/nuget/dt/EFCoreLayerKit)](https://www.nuget.org/packages/EFCoreLayerKit)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://licenses.nuget.org/MIT)
[![GitHub stars](https://img.shields.io/github/stars/jau-huei/EFCoreLayerKit?style=social)](https://github.com/jau-huei/EFCoreLayerKit)


# EFCoreLayerKit

EFCoreLayerKit 是一个基于 Entity Framework Core 的通用数据访问层工具包，支持自动仓储、DTO 映射、软删除、自动迁移等功能，适用于 .NET 8+ 项目。

---

## 主要特性
- 自动注册所有继承自 `BaseDbContext` 的上下文，并自动迁移数据库结构
- 自动注册所有继承自 `BaseRepository<TEntity>` 的仓储类
- 支持软删除、乐观锁、批量操作等常用数据访问模式
- 支持 DTO 与实体的自动双向映射（基于 AutoMapper）
- 丰富的通用结果类型（FResult/FResult<T>/FPagedResult<T>）
- 适用于 Blazor、ASP.NET Core 等依赖注入场景

---

## 安装依赖

确保你的项目已引用以下 NuGet 包：
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite
- AutoMapper
- EFCoreLayerKit
- System.Linq.Dynamic.Core
- EFCoreLayerKit（本库）

---

## 快速开始

1. **定义实体和 DbContext**
```csharp
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
```
2. **定义仓储类**
```csharp
public class AuthorRepository : BaseRepository<Author>
{
    public AuthorRepository(TestDbContext ctx) : base(ctx) { }
}

public class BookRepository : BaseRepository<Book>
{
    public BookRepository(TestDbContext ctx) : base(ctx) { }
}
```
3. **注册服务与自动迁移**

在 `Program.cs` 中：
```csharp
var services = new ServiceCollection();
services.AddEFCoreLayerKit();
var provider = services.BuildServiceProvider();
```
注册时会自动调用所有 DbContext 的 `EnsureDatabaseMigrated()`，无需手动迁移。

4. **使用仓储进行数据操作**
```csharp
var authorRepo = provider.GetRequiredService<AuthorRepository>();
var bookRepo = provider.GetRequiredService<BookRepository>();

// 增加数据
var author = new Author { Name = "张三" };
var addAuthorResult = await authorRepo.AddAsync(author);

var book = new Book { Title = "C# 入门", AuthorId = author.Id };
var addBookResult = await bookRepo.AddAsync(book);

// 查询
var authors = await authorRepo.GetAllAsync();

// 修改
author.Name = "李四";
var updateAuthorResult = await authorRepo.UpdateAsync(author);

// 删除
var deleteAuthorResult = await authorRepo.DeleteAsync(author.Id);
```

## 结果类型说明

- `FResult`：通用操作结果，含成功标志、消息、错误码等。
- `FResult<T>`：带数据的操作结果。
- `FPagedResult<T>`：带分页信息的数据结果。

所有结果类型均支持格式化消息、错误码、日志转换等。

---

## 进阶用法
- 支持自定义仓储、DTO 映射、全局查询过滤等
- 支持批量操作、软删除、乐观锁等
- 详见源码注释与各类扩展方法

---

## 测试与示例

本仓库自带完整的测试项目（EFCoreLayerKitTest），可直接运行 `Program.cs` 查看增删改查等功能的实际效果。

---

## 许可证
MIT
