namespace EFCoreLayerKit.QueryModels
{
    /// <summary>
    /// 逻辑关系类型。
    /// </summary>
    public enum LogicType
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// 包含
        /// </summary>
        Contains,

        /// <summary>
        /// 开头为
        /// </summary>
        StartsWith,

        /// <summary>
        /// 结尾为
        /// </summary>
        EndsWith,

        /// <summary>
        /// 介于
        /// </summary>
        Between
    }
}
