using EFCoreLayerKit.QueryModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using EFCoreLayerKit.Entities;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// IQueryable<TEntity> ����չ������֧�ָ��� QueryOptions<TEntity> Ӧ�õ������԰���������Ͳ�ѯ���������ơ�
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// ���� QueryOptions<TEntity> ���öԲ�ѯ���е������� Include������֧�ֶ༶��������ȫ�ֹ������Ȳ�����
        /// </summary>
        /// <typeparam name="TEntity">ʵ�����ͣ�����̳��� BaseEntity��</typeparam>
        /// <param name="query">Ҫ������ IQueryable ��ѯ����</param>
        /// <param name="option">��ѯѡ�</param>
        /// <returns>Ӧ��ѡ���� IQueryable ��ѯ����</returns>
        public static IQueryable<TEntity> ApplyQueryOption<TEntity>(this IQueryable<TEntity> query, QueryOptions<TEntity>? option) where TEntity : BaseEntity
        {
            if (option == null) return query;

            // Apply Includes
            if (option.Includes != null)
            {
                foreach (var include in option.Includes)
                {
                    query = query.Include(include);
                }
            }

            // Ignore global query filters if needed
            if (option.IgnoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            // Apply multi-level OrderBy if specified
            if (option.OrderBys != null && option.OrderBys.Count > 0)
            {
                string? orderString = null;
                foreach (var order in option.OrderBys)
                {
                    if (string.IsNullOrWhiteSpace(order.PropertyName)) continue;
                    if (orderString == null)
                        orderString = $"{order.PropertyName} {(order.Ascending ? "ascending" : "descending")}";
                    else
                        orderString += $", {order.PropertyName} {(order.Ascending ? "ascending" : "descending")}";
                }
                if (!string.IsNullOrWhiteSpace(orderString))
                {
                    query = query.OrderBy(orderString);
                }
            }

            return query;
        }
    }
}
