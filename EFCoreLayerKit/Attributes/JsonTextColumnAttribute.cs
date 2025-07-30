using System;

namespace EFCoreLayerKit.Attributes
{
    /// <summary>
    /// 标记属性以 JSON/TXT 方式序列化存储到数据库的特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonTextColumnAttribute : Attribute
    {
    }
}