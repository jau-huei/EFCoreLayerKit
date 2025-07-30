using EFCoreLayerKit.Data;
using EFCoreLayerKit.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// 提供自动注册所有 BaseDbContext 及 BaseRepository 派生类到依赖注入容器的扩展方法。
    /// 支持递归注册所有抽象基类与实现类本身，并自动迁移所有数据库。
    /// </summary>
    public static class EFCoreLayerKitServiceCollectionExtensions
    {
        /// <summary>
        /// 注册所有继承自指定基类的派生类型（包括递归注册所有抽象基类与实现类本身），生命周期为 Scoped。
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="type">基类类型</param>
        public static void RegisterAllDerivedTypes(this IServiceCollection services, Type type)
        {
            // 查找所有继承自 type 的子类
            var serviceTypes = TypeDiscoveryUtil.GetAllDerivedTypes(type)
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList();

            // 将找到的每个子类注册为 Scoped 服务
            foreach (var serviceType in serviceTypes)
            {
                var baseType = serviceType.BaseType;

                // 遍历每个基类，直到找到 type
                while (baseType != null && baseType != typeof(object))
                {
                    // 检查基类是否为抽象类，如果是，注册其与子类的关系
                    if (baseType.IsAbstract)
                    {
                        services.AddScoped(baseType, serviceType);
                    }
                    baseType = baseType.BaseType;
                }

                // 最后注册具体的子类本身
                services.AddScoped(serviceType);
            }
        }

        /// <summary>
        /// 注册所有继承自指定泛型基类的派生类型（包括递归注册所有抽象基类与实现类本身），生命周期为 Scoped。
        /// </summary>
        /// <typeparam name="T">基类类型</typeparam>
        /// <param name="services">服务集合</param>
        public static void RegisterAllDerivedTypes<T>(this IServiceCollection services)
        {
            services.RegisterAllDerivedTypes(typeof(T));
        }

        /// <summary>
        /// 自动注册所有 BaseDbContext 及 BaseRepository 派生类到依赖注入容器，并自动迁移所有数据库结构。
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddEFCoreLayerKit(this IServiceCollection services)
        {
            // 获取所有已加载的程序集（包括主程序和测试/外部程序集）
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            // 注册所有 BaseDbContext 派生类（包括外部程序集）
            services.RegisterAllDerivedTypes<BaseDbContext>();

            // 注册所有 BaseRepository<> 派生类（包括外部程序集），并递归注册其所有抽象基类
            services.RegisterAllDerivedTypes(typeof(BaseRepository<>));

            // 自动迁移所有 DbContext
            using (var provider = services.BuildServiceProvider())
            {
                foreach (var dbContextType in TypeDiscoveryUtil.GetAllDerivedTypes<BaseDbContext>())
                {
                    var db = provider.GetService(dbContextType) as BaseDbContext;
                    db?.EnsureDatabaseMigrated();
                }
            }

            return services;
        }
    }
}
