using EFCoreLayerKit.Data;
using EFCoreLayerKit.Repositories;
using EFCoreLayerKit.Services;
using Microsoft.Extensions.DependencyInjection;

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
        /// ��չ IServiceCollection���Զ�ע������ BaseDbContext��BaseRepository �� BaseService �������ൽ����ע��������
        /// ���ݹ�ע�������г�����֧࣬�ֿ�����Զ����֡�ע����������Ϊ Scoped��
        /// ͬʱ�Զ�ע�� AutoMapper ���ã���������ʱ�Զ�Ǩ���������ݿ�ṹ������ EnsureDatabaseMigrated����
        /// </summary>
        /// <param name="services">���񼯺�</param>
        /// <returns>���񼯺ϱ���������ʽ����</returns>
        public static IServiceCollection AddEFCoreLayerKit(this IServiceCollection services)
        {
            // ��ȡ�����Ѽ��صĳ��򼯣�����������Ͳ���/�ⲿ���򼯣�
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            // ע�� AutoMapper ����
            services.AddAutoMapper(allAssemblies);

            // ע������ BaseDbContext �����ࣨ�����ⲿ���򼯣�
            services.RegisterAllDerivedTypes<BaseDbContext>();

            // ע������ BaseRepository<> �����ࣨ�����ⲿ���򼯣������ݹ�ע�������г������
            services.RegisterAllDerivedTypes(typeof(BaseRepository<>));

            // ע������ BaseService �����ࣨ�����ⲿ���򼯣������ݹ�ע�������г������
            services.RegisterAllDerivedTypes<BaseService>();

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
