using EFCoreLayerKit.QueryModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using EFCoreLayerKit.Entities;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// IQueryable<TEntity> 的扩展方法，支持根据 QueryOptions<TEntity> 应用导航属性包含、排序和查询过滤器控制。
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 根据 QueryOptions<TEntity> 配置对查询进行导航属性 Include、排序（支持多级）、忽略全局过滤器等操作。
        /// </summary>
        /// <typeparam name="TEntity">实体类型，必须继承自 BaseEntity。</typeparam>
        /// <param name="query">要操作的 IQueryable 查询对象。</param>
        /// <param name="option">查询选项。</param>
        /// <returns>应用选项后的 IQueryable 查询对象。</returns>
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
