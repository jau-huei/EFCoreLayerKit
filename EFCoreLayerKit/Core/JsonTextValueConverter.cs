using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// ͨ�� JSON �ı�ֵת������������������ JSON �ַ�����ת��
    /// </summary>
    /// <typeparam name="T">Ҫת��������</typeparam>
    public class JsonTextValueConverter<T> : ValueConverter<T?, string?>
    {
        /// <summary>
        /// ����һ���µ� JsonTextValueConverter ʵ����
        /// </summary>
        public JsonTextValueConverter()
            : base(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => string.IsNullOrEmpty(v) ? default! : JsonSerializer.Deserialize<T>(v, (JsonSerializerOptions?)null)!)
        {
        }
    }
}
