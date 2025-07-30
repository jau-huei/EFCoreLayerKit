using System.Collections.Generic;

namespace EFCoreLayerKit.QueryModels
{
    /// <summary>
    /// 分页查询输入参数。
    /// </summary>
    public class PagedQueryInput
    {
        /// <summary>
        /// 当前页码（从 1 开始）。
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页数据量。
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 查询条件集合。
        /// </summary>
        public List<QueryCondition> Conditions { get; set; } = new();
    }
}
