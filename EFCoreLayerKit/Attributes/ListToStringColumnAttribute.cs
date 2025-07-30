namespace EFCoreLayerKit.Attributes
{
    /// <summary>
    /// ��� List&lt;T&gt; ������ TEXT �ֶδ洢���Զ����ַ�����ת�����ԡ�
    /// ������ <see cref="EFCoreLayerKit.Core.GlobalConstants.PrimitiveTypes"/> ֧�ֵĻ������͵� List ���ԡ�
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ListToStringColumnAttribute : Attribute
    {
    }
}