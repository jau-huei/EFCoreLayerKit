using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// 通用 JSON 文本值转换器，将任意类型与 JSON 字符串互转。
    /// </summary>
    /// <typeparam name="T">要转换的类型</typeparam>
    public class JsonTextValueConverter<T> : ValueConverter<T?, string?>
    {
        public JsonTextValueConverter()
            : base(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => string.IsNullOrEmpty(v) ? default! : JsonSerializer.Deserialize<T>(v, (JsonSerializerOptions?)null)!)
        {
        }
    }
}
