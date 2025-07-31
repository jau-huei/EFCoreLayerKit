using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// �� List&lt;T&gt; �붺�ŷָ��ַ�����ת��ͨ��ֵת������
    /// </summary>
    /// <typeparam name="T">��������</typeparam>
    public class ListToStringValueConverter<T> : ValueConverter<List<T>, string>
    {
        /// <summary>
        /// ����һ���µ� ListToStringValueConverter ʵ����
        /// </summary>
        public ListToStringValueConverter()
            : base(
                v => v == null ? "" : string.Join(",", v),
                v => string.IsNullOrEmpty(v) ? new List<T>() : v.Split(',', StringSplitOptions.None).Select(ConvertToT).ToList())
        {
        }

        /// <summary>
        /// ���ַ���ת��Ϊ T ���͡�
        /// </summary>
        /// <param name="s">Ҫת�����ַ�����</param>
        /// <returns>ת����� T ����ֵ��</returns>
        private static T ConvertToT(string s)
        {
            if (typeof(T) == typeof(string))
                return (T)(object)s;
            return (T)Convert.ChangeType(s, typeof(T));
        }
    }
}
