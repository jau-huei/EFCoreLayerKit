using EFCoreLayerKit.Data;
using EFCoreLayerKit.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// �ṩ�Զ�ע������ BaseDbContext �� BaseRepository �����ൽ����ע����������չ������
    /// ֧�ֵݹ�ע�����г��������ʵ���౾�����Զ�Ǩ���������ݿ⡣
    /// </summary>
    public static class EFCoreLayerKitServiceCollectionExtensions
    {
        /// <summary>
        /// ע�����м̳���ָ��������������ͣ������ݹ�ע�����г��������ʵ���౾������������Ϊ Scoped��
        /// </summary>
        /// <param name="services">���񼯺�</param>
        /// <param name="type">��������</param>
        public static void RegisterAllDerivedTypes(this IServiceCollection services, Type type)
        {
            // �������м̳��� type ������
            var serviceTypes = TypeDiscoveryUtil.GetAllDerivedTypes(type)
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList();

            // ���ҵ���ÿ������ע��Ϊ Scoped ����
            foreach (var serviceType in serviceTypes)
            {
                var baseType = serviceType.BaseType;

                // ����ÿ�����ֱ࣬���ҵ� type
                while (baseType != null && baseType != typeof(object))
                {
                    // �������Ƿ�Ϊ�����࣬����ǣ�ע����������Ĺ�ϵ
                    if (baseType.IsAbstract)
                    {
                        services.AddScoped(baseType, serviceType);
                    }
                    baseType = baseType.BaseType;
                }

                // ���ע���������౾��
                services.AddScoped(serviceType);
            }
        }

        /// <summary>
        /// ע�����м̳���ָ�����ͻ�����������ͣ������ݹ�ע�����г��������ʵ���౾������������Ϊ Scoped��
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="services">���񼯺�</param>
        public static void RegisterAllDerivedTypes<T>(this IServiceCollection services)
        {
            services.RegisterAllDerivedTypes(typeof(T));
        }

        /// <summary>
        /// �Զ�ע������ BaseDbContext �� BaseRepository �����ൽ����ע�����������Զ�Ǩ���������ݿ�ṹ��
        /// </summary>
        /// <param name="services">���񼯺�</param>
        /// <returns>���񼯺�</returns>
        public static IServiceCollection AddEFCoreLayerKit(this IServiceCollection services)
        {
            // ��ȡ�����Ѽ��صĳ��򼯣�����������Ͳ���/�ⲿ���򼯣�
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            // ע������ BaseDbContext �����ࣨ�����ⲿ���򼯣�
            services.RegisterAllDerivedTypes<BaseDbContext>();

            // ע������ BaseRepository<> �����ࣨ�����ⲿ���򼯣������ݹ�ע�������г������
            services.RegisterAllDerivedTypes(typeof(BaseRepository<>));

            // �Զ�Ǩ������ DbContext
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
