namespace EFCoreLayerKit.QueryModels
{
    /// <summary>
    /// 单个字段的查询条件。
    /// </summary>
    public class QueryCondition
    {
        /// <summary>
        /// 字段名称。
        /// </summary>
        public string Field { get; set; } = "";

        /// <summary>
        /// 该字段的多个逻辑条件（同字段内可为 AND/OR）。
        /// </summary>
        public List<FieldLogic> Logics { get; set; } = new List<FieldLogic>();
    }
}
