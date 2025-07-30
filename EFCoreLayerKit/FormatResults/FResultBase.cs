using FormatLog;
using System.Text.Json.Serialization;

namespace EFCoreLayerKit.Results
{
    /// <summary>
    /// 通用结果基类，包含操作结果的通用属性。
    /// 支持参数化消息，便于多语言和数据库存储。
    /// </summary>
    public abstract class FResultBase
    {
        /// <summary>
        /// 是否成功。
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息模板（如资源键或格式化字符串）。
        /// </summary>
        [JsonIgnore]
        public string? MessageFormat { get; set; }

        /// <summary>
        /// 消息参数数组，用于格式化消息内容。
        /// </summary>
        [JsonIgnore]
        public object?[]? MessageArgs { get; set; }

        /// <summary>
        /// 错误码。
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 结果创建时间。
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 获取格式化后的消息内容。
        /// </summary>
        public string Message => ToString();

        /// <summary>
        /// 获取格式化后的字符串表示，包含时间、级别与消息内容。
        /// </summary>
        /// <returns>格式化后的结果字符串。</returns>
        public override string ToString()
        {
            var level = "Info";
            if (!Success) level = "Error";

                return $"[{CreatedAt:yy-MM-dd HH:mm:ss.fff}] [{level}] {string.Format(MessageFormat ?? "", MessageArgs!)}";
        }

        /// <summary>
        /// 转换为日志对象 Log，便于统一日志记录。
        /// </summary>
        /// <returns>Log 对象。</returns>
        public Log ToLog()
        {
            var level = LogLevel.Info;
            if (!Success) level = LogLevel.Error;

            return new Log(level, MessageFormat ?? "", MessageArgs!) { CreatedAt = CreatedAt };
        }
    }
}
