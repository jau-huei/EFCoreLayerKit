using System;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// 全局常量定义类。
    /// </summary>
    public static class GlobalConstants
    {
        /// <summary>
        /// 支持的基础类型集合。
        /// </summary>
        public static readonly Type[] PrimitiveTypes = new[]
        {
            typeof(bool), typeof(byte), typeof(char), typeof(decimal), typeof(double), typeof(float),
            typeof(int), typeof(long), typeof(sbyte), typeof(short), typeof(uint), typeof(ulong), typeof(ushort), typeof(string)
        };
    }
}
