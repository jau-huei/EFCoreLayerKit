namespace EFCoreLayerKit.QueryModels
{
    /// <summary>
    /// 字段的单个逻辑条件。
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// 操作类型。
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// 查询值1（如等于、包含、开头为、结尾为、介于的起始值）。
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// 查询值2（仅介于时使用）。
        /// </summary>
        public string Value2 { get; set; } = "";

        /// <summary>
        /// 字段内多个连接关系的 AND/OR。
        /// </summary>
        public JoinType AndOr { get; set; } = JoinType.And;
    }
}
