namespace EFCoreLayerKit.Entities
{
    /// <summary>
    /// 包含数据更新时间的抽象基类，继承自 BaseEntity。
    /// 适用于需要在更新时检查数据更新时间的实体比较数据版本。
    /// </summary>
    public abstract class UpdatableEntity : BaseEntity
    {
        /// <summary>
        /// 数据最后更新时间。
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
