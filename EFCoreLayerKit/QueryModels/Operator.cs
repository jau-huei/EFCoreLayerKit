namespace EFCoreLayerKit.QueryModels
{
    /// <summary>
    /// 操作类型。
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equals = 1,

        /// <summary>
        /// 包含
        /// </summary>
        Contains = 2,

        /// <summary>
        /// 以...开始
        /// </summary>
        StartsWith = 3,

        /// <summary>
        /// 以...结束
        /// </summary>
        EndsWith = 4,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan = 5,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan = 6,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEquals = 7,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEquals = 8,

        /// <summary>
        /// 条件表达式
        /// </summary>
        Condition = 9,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEquals = 10,

        /// <summary>
        /// 为空
        /// </summary>
        IsNull = 11,

        /// <summary>
        /// 不为空
        /// </summary>
        IsNotNull = 12,

        /// <summary>
        /// 不包含
        /// </summary>
        NotContains = 13,

        /// <summary>
        /// 同一天
        /// </summary>
        TheSameDateWith = 14,

        /// <summary>
        /// 区间（Between）
        /// </summary>
        Between = 15
    }
}
