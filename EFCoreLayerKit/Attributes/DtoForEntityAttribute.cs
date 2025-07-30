using System;

namespace EFCoreLayerKit.Attributes
{
    /// <summary>
    /// 标记 DTO 类与 Entity 类型关联的特性，可用于自动映射。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DtoForEntityAttribute : Attribute
    {
        /// <summary>
        /// 关联的实体类型。
        /// </summary>
        public Type EntityType { get; }

        public DtoForEntityAttribute(Type entityType)
        {
            EntityType = entityType;
        }
    }
}