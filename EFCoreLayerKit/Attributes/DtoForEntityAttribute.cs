using System;

namespace EFCoreLayerKit.Attributes
{
    /// <summary>
    /// ��� DTO ���� Entity ���͹��������ԣ��������Զ�ӳ�䡣
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DtoForEntityAttribute : Attribute
    {
        /// <summary>
        /// ������ʵ�����͡�
        /// </summary>
        public Type EntityType { get; }

        /// <summary>
        /// ����һ���µ� DtoForEntityAttribute ʵ����
        /// </summary>
        /// <param name="entityType">������ʵ�����͡�</param>
        public DtoForEntityAttribute(Type entityType)
        {
            EntityType = entityType;
        }
    }
}