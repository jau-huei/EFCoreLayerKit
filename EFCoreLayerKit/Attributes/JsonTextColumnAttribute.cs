using System;

namespace EFCoreLayerKit.Attributes
{
    /// <summary>
    /// ��������� JSON/TXT ��ʽ���л��洢�����ݿ�����ԡ�
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonTextColumnAttribute : Attribute
    {
    }
}