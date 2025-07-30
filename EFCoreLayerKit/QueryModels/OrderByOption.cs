namespace EFCoreLayerKit.QueryModels
{
    /// <summary>
    /// 排序选项，支持多级排序。
    /// </summary>
    public class OrderByOption
    {
        /// <summary>
        /// 排序的属性名称。
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// 是否升序。
        /// </summary>
        public bool Ascending { get; set; } = true;

        /// <summary>
        /// 构造一个排序选项。
        /// </summary>
        /// <param name="propertyName">排序的属性名称。</param>
        /// <param name="ascending">是否升序。</param>
        public OrderByOption(string propertyName, bool ascending = true)
        {
            PropertyName = propertyName;
            Ascending = ascending;
        }
    }
}
