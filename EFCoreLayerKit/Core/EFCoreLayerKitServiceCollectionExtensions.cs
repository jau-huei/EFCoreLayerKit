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
    /// 扩展方法：自动注册所有 BaseDbContext 及 BaseRepository 派生类到依赖注入容器。
    /// </summary>
    public static class EFCoreLayerKitServiceCollectionExtensions
    {
        /// <summary>
        /// 自动注册所有 BaseDbContext 及 BaseRepository 派生类到依赖注入容器，并自动迁移数据库。
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddEFCoreLayerKit(this IServiceCollection services)
        {
            // 注册所有 BaseDbContext 派生类
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
                    // 默认用 options => {}
                    generic.Invoke(null, new object[] { services, (Action<DbContextOptionsBuilder>)(_ => { }) });
                }
            }

            // 注册所有 BaseRepository<> 派生类
            var repoTypes = TypeDiscoveryUtil.GetAllDerivedTypes(typeof(object))
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));
            foreach (var repoType in repoTypes)
            {
                services.AddScoped(repoType);
            }

            // 自动迁移所有 DbContext
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
