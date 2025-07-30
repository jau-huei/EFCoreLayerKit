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

        public DtoForEntityAttribute(Type entityType)
        {
            EntityType = entityType;
        }
    }
}