using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// 将 List&lt;T&gt; 与逗号分隔字符串互转的通用值转换器。
    /// </summary>
    /// <typeparam name="T">基础类型</typeparam>
    public class ListToStringValueConverter<T> : ValueConverter<List<T>, string>
    {
        public ListToStringValueConverter()
            : base(
                v => v == null ? "" : string.Join(",", v),
                v => string.IsNullOrEmpty(v) ? new List<T>() : v.Split(',', StringSplitOptions.None).Select(ConvertToT).ToList())
        {
        }

        private static T ConvertToT(string s)
        {
            if (typeof(T) == typeof(string))
                return (T)(object)s;
            return (T)Convert.ChangeType(s, typeof(T));
        }
    }
}
