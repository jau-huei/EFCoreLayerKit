using EFCoreLayerKit.Entities;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;

namespace EFCoreLayerKit.QueryModels
{
    /// <summary>
    /// 提供用于实体查询的选项，包括导航属性包含、排序和查询过滤器控制。
    /// </summary>
    /// <typeparam name="TEntity">被查询的实体类型，必须继承自 BaseEntity。</typeparam>
    public class QueryOptions<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 获取或设置是否忽略全局查询过滤器。
        /// </summary>
        public bool IgnoreQueryFilters { get; set; } = false;

        /// <summary>
        /// 获取或设置查询中要包含的导航属性。
        /// </summary>
        public List<Expression<Func<TEntity, object>>>? Includes { get; set; }

        /// <summary>
        /// 获取或设置排序规则集合，支持多级排序。
        /// </summary>
        public List<OrderByOption> OrderBys { get; set; } = new();

        /// <summary>
        /// 设置是否忽略全局查询过滤器。
        /// </summary>
        /// <param name="ignore">是否忽略查询过滤器。</param>
        /// <returns>当前 <see cref="QueryOptions{TEntity}"/> 实例。</returns>
        public QueryOptions<TEntity> WithIgnoreQueryFilters(bool ignore = true)
        {
            IgnoreQueryFilters = ignore;
            return this;
        }

        /// <summary>
        /// 设置查询中要包含的导航属性。
        /// </summary>
        /// <param name="includes">要包含的导航属性。</param>
        /// <returns>当前 <see cref="QueryOptions{TEntity}"/> 实例。</returns>
        public QueryOptions<TEntity> WithIncludes(params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes == null || includes.Length == 0)
            {
                Includes = null;
            }
            else
            {
                Includes = includes.ToList();
            }

            return this;
        }

        /// <summary>
        /// 添加排序规则（OrderBy）。
        /// </summary>
        /// <param name="propertyName">排序的属性名称。</param>
        /// <param name="ascending">是否升序排序。</param>
        /// <returns>当前 <see cref="QueryOptions{TEntity}"/> 实例。</returns>
        public QueryOptions<TEntity> WithOrderBy(string propertyName, bool ascending = true)
        {
            OrderBys.Add(new OrderByOption(propertyName, ascending));
            return this;
        }

        /// <summary>
        /// 使用 <see cref="PropertyInfo"/> 设置排序规则。
        /// </summary>
        /// <param name="property">用于排序的属性。</param>
        /// <param name="ascending">是否升序排序。</param>
        /// <returns>当前 <see cref="QueryOptions{TEntity}"/> 实例。</returns>
        public QueryOptions<TEntity> WithOrderBy(PropertyInfo property, bool ascending = true)
        {
            return WithOrderBy(property.Name, ascending);
        }
    }
}