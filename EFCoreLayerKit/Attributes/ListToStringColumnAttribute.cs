namespace EFCoreLayerKit.Attributes
{
    /// <summary>
    /// 标记 List&lt;T&gt; 属性以 TEXT 字段存储并自动与字符串互转的特性。
    /// 适用于 <see cref="EFCoreLayerKit.Core.GlobalConstants.PrimitiveTypes"/> 支持的基础类型的 List 属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ListToStringColumnAttribute : Attribute
    {
    }
}