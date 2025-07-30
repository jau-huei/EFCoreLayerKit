using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using EFCoreLayerKit.Core;
using EFCoreLayerKit.Data;
using EFCoreLayerKit.Repositories;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// ��չ�������Զ�ע������ BaseDbContext �� BaseRepository �����ൽ����ע��������
    /// </summary>
    public static class EFCoreLayerKitServiceCollectionExtensions
    {
        /// <summary>
        /// �Զ�ע������ BaseDbContext �� BaseRepository �����ൽ����ע�����������Զ�Ǩ�����ݿ⡣
        /// </summary>
        /// <param name="services">���񼯺�</param>
        /// <returns>���񼯺�</returns>
        public static IServiceCollection AddEFCoreLayerKit(this IServiceCollection services)
        {
            // ע������ BaseDbContext ������
            var dbContextTypes = TypeDiscoveryUtil.GetAllDerivedTypes<BaseDbContext>();
            foreach (var dbContextType in dbContextTypes)
            {
                // AddDbContext<T>()
                var method = typeof(EntityFrameworkServiceCollectionExtensions)
                    .GetMethods()
                    .FirstOrDefault(m => m.Name == "AddDbContext" && m.IsGenericMethodDefinition && m.GetParameters().Length == 2);
                if (method != null)
                {
                    var generic = method.MakeGenericMethod(dbContextType);
                    // Ĭ���� options => {}
                    generic.Invoke(null, new object[] { services, (Action<DbContextOptionsBuilder>)(_ => { }) });
                }
            }

            // ע������ BaseRepository<> ������
            var repoTypes = TypeDiscoveryUtil.GetAllDerivedTypes(typeof(object))
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));
            foreach (var repoType in repoTypes)
            {
                services.AddScoped(repoType);
            }

            // �Զ�Ǩ������ DbContext
            using (var provider = services.BuildServiceProvider())
            {
                foreach (var dbContextType in dbContextTypes)
                {
                    var db = provider.GetService(dbContextType) as BaseDbContext;
                    db?.EnsureDatabaseMigrated();
                }
            }

            return services;
        }
    }
}
