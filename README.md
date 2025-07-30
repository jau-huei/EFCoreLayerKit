# EFCoreLayerKit

EFCoreLayerKit 是一个基于 Entity Framework Core 的通用数据访问层工具包，支持自动仓储、DTO 映射、软删除、自动迁移等功能，适用于 .NET 8+ 项目。

## 主要特性
- 自动注册所有继承自 `BaseDbContext` 的上下文，并自动迁移数据库结构
- 自动注册所有继承自 `BaseRepository<TEntity>` 的仓储类
- 支持软删除、乐观锁、批量操作等常用数据访问模式
- 支持 DTO 与实体的自动双向映射（基于 AutoMapper）
- 适用于 Blazor、ASP.NET Core 等依赖注入场景

## 快速开始

1. **安装依赖**

确保你的项目已引用 EntityFrameworkCore、AutoMapper 及本库。

2. **定义 DbContext 和 Repository**
public class MyDbContext : BaseDbContext { /* ... */ }
public class MyEntityRepository : BaseRepository<MyEntity> { public MyEntityRepository(MyDbContext ctx) : base(ctx) { } }
3. **在程序启动时注册服务**
// 在 Program.cs 或 Startup.cs
services.AddEFCoreLayerKit();
4. **自动迁移数据库**

注册时会自动调用所有 DbContext 的 `EnsureDatabaseMigrated()`，无需手动迁移。

5. **使用仓储和上下文**
public class MyService
{
    private readonly MyEntityRepository _repo;
    public MyService(MyEntityRepository repo) { _repo = repo; }
    // ...
}
## 进阶用法
- 支持自定义仓储、DTO 映射、全局查询过滤等
- 详见源码注释与各类扩展方法

## 许可证
MIT
