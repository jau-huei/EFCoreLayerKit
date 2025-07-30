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
    /// Ӧ�ó������ݿ������ĳ�����࣬����ʵ�������ݿ�Ľ�����
    /// </summary>
    public abstract class BaseDbContext : DbContext
    {
        /// <summary>
        /// ��ȡ���ݿ����ƣ�ȥ�� DbContext ��׺����
        /// </summary>
        public string DbName => GetType().Name.Replace("DbContext", "");

        /// <summary>
        /// ���ݿ���Ŀ¼��Ĭ�� DB Ŀ¼�������Ŀ¼�£���
        /// </summary>
        public static string DbDirectory { get; set; } = CreateDbDirectory();

        /// <summary>
        /// �������ݿ�Ŀ¼���粻�������Զ���������������Ŀ¼·����
        /// </summary>
        /// <returns>���ݿ�Ŀ¼�ľ���·����</returns>
        private static string CreateDbDirectory()
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "DB");
            Directory.CreateDirectory(dir);
            return dir;
        }

        /// <summary>
        /// ����ʵ��ӳ���ϵ��
        /// </summary>
        /// <param name="modelBuilder">ģ�͹�������</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // �Զ�Ϊ��� ListToStringColumnAttribute �� List<T> �������ת������T Ϊ��������
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

            // �Զ�Ϊ��� JsonTextColumnAttribute ��������� JSON ���л�ת����
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

            // Ϊ���а��� IsDeleted ���Ե�ʵ�������ɾ��ȫ�ֹ�����
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                var isDeletedProp = clrType.GetProperty("IsDeleted");
                if (isDeletedProp != null && isDeletedProp.PropertyType == typeof(bool))
                {
                    // ���� e => !e.IsDeleted �ı��ʽ
                    var parameter = System.Linq.Expressions.Expression.Parameter(clrType, "e");
                    var property = System.Linq.Expressions.Expression.Property(parameter, isDeletedProp);
                    var notDeleted = System.Linq.Expressions.Expression.Equal(property, System.Linq.Expressions.Expression.Constant(false));
                    var lambda = System.Linq.Expressions.Expression.Lambda(notDeleted, parameter);
                    modelBuilder.Entity(clrType).HasQueryFilter(lambda);
                }
            }
        }

        /// <summary>
        /// �������ݿ����ӣ�Ĭ��ʹ�� Sqlite�����ݿ��ļ�λ�� DB Ŀ¼�£���
        /// </summary>
        /// <param name="optionsBuilder">���ݿ�ѡ�������</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var file = Path.Combine(DbDirectory, $"{DbName}.sqlite");
            optionsBuilder.UseSqlite($"Data Source={file}");
        }

        /// <summary>
        /// ȷ�����ݿ��ļ����ڲ��Զ�Ǩ�Ƶ����½ṹ��
        /// ���ݿ��ļ�λ�� DB Ŀ¼�£��ļ���Ϊ {��������}.sqlite��
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