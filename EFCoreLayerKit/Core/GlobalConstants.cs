using System;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// ȫ�ֳ��������ࡣ
    /// </summary>
    public static class GlobalConstants
    {
        /// <summary>
        /// ֧�ֵĻ������ͼ��ϡ�
        /// </summary>
        public static readonly Type[] PrimitiveTypes = new[]
        {
            typeof(bool), typeof(byte), typeof(char), typeof(decimal), typeof(double), typeof(float),
            typeof(int), typeof(long), typeof(sbyte), typeof(short), typeof(uint), typeof(ulong), typeof(ushort), typeof(string)
        };
    }
}
