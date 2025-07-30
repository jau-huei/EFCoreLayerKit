using EFCoreLayerKit.Attributes;
using EFCoreLayerKit.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EFCoreLayerKit.Data
{
    /// <summary>
    /// 应用程序数据库上下文抽象基类，负责实体与数据库的交互。
    /// </summary>
    public abstract class BaseDbContext : DbContext
    {
        /// <summary>
        /// 获取数据库名称（去除 DbContext 后缀）。
        /// </summary>
        public string DbName => GetType().Name.Replace("DbContext", "");

        /// <summary>
        /// 数据库存放目录（默认 DB 目录，程序根目录下）。
        /// </summary>
        public static string DbDirectory { get; set; } = CreateDbDirectory();

        /// <summary>
        /// 创建数据库目录（如不存在则自动创建），并返回目录路径。
        /// </summary>
        /// <returns>数据库目录的绝对路径。</returns>
        private static string CreateDbDirectory()
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "DB");
            Directory.CreateDirectory(dir);
            return dir;
        }

        /// <summary>
        /// 配置实体映射关系。
        /// </summary>
        /// <param name="modelBuilder">模型构建器。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 自动为标记 ListToStringColumnAttribute 的 List<T> 属性添加转换器，T 为基础类型
            var supportedTypes = GlobalConstants.PrimitiveTypes;
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                foreach (var prop in clrType.GetProperties())
                {
                    var attr = prop.GetCustomAttribute<ListToStringColumnAttribute>();
                    if (attr != null && prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var elementType = prop.PropertyType.GetGenericArguments()[0];
                        if (supportedTypes.Contains(elementType))
                        {
                            var converterType = typeof(ListToStringValueConverter<>).MakeGenericType(elementType);
                            var converter = Activator.CreateInstance(converterType);
                            modelBuilder.Entity(clrType)
                                .Property(prop.Name)
                                .HasConversion((ValueConverter)converter!);
                        }
                    }
                }
            }

            // 自动为标记 JsonTextColumnAttribute 的属性添加 JSON 序列化转换器
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                foreach (var prop in clrType.GetProperties())
                {
                    var attr = prop.GetCustomAttribute<JsonTextColumnAttribute>();
                    if (attr != null)
                    {
                        var converterType = typeof(ValueConverter<,>).MakeGenericType(prop.PropertyType, typeof(string));
                        var converter = Activator.CreateInstance(
                            typeof(JsonTextValueConverter<>).MakeGenericType(prop.PropertyType));
                        modelBuilder.Entity(clrType)
                            .Property(prop.Name)
                            .HasConversion((ValueConverter)converter!);
                    }
                }
            }

            // 为所有包含 IsDeleted 属性的实体添加软删除全局过滤器
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                var isDeletedProp = clrType.GetProperty("IsDeleted");
                if (isDeletedProp != null && isDeletedProp.PropertyType == typeof(bool))
                {
                    // 构建 e => !e.IsDeleted 的表达式
                    var parameter = System.Linq.Expressions.Expression.Parameter(clrType, "e");
                    var property = System.Linq.Expressions.Expression.Property(parameter, isDeletedProp);
                    var notDeleted = System.Linq.Expressions.Expression.Equal(property, System.Linq.Expressions.Expression.Constant(false));
                    var lambda = System.Linq.Expressions.Expression.Lambda(notDeleted, parameter);
                    modelBuilder.Entity(clrType).HasQueryFilter(lambda);
                }
            }
        }

        /// <summary>
        /// 配置数据库连接（默认使用 Sqlite，数据库文件位于 DB 目录下）。
        /// </summary>
        /// <param name="optionsBuilder">数据库选项构建器。</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var file = Path.Combine(DbDirectory, $"{DbName}.sqlite");
            optionsBuilder.UseSqlite($"Data Source={file}");
        }

        /// <summary>
        /// 确保数据库文件存在并自动迁移到最新结构。
        /// 数据库文件位于 DB 目录下，文件名为 {上下文名}.sqlite。
        /// </summary>
        public virtual void EnsureDatabaseMigrated()
        {
            var file = Path.Combine(DbDirectory, $"{DbName}.sqlite");

            var connection = Database.GetDbConnection();
            connection.ConnectionString = $"Data Source={file}";

            Database.Migrate();
            connection.Open();
        }
    }
}