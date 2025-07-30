namespace EFCoreLayerKit.Results
{
    /// <summary>
    /// 通用回传结果。
    /// </summary>
    public class FResult : FResultBase
    {
        /// <summary>
        /// 创建一个成功的结果。
        /// </summary>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>成功的Result对象。</returns>
        public static FResult Ok(string? messageFormat = null, params object?[] messageArgs)
            => new FResult { Success = true, MessageFormat = messageFormat, MessageArgs = messageArgs, Code = 0 };

        /// <summary>
        /// 创建一个失败的结果。
        /// </summary>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="code">错误码，默认为-1。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>失败的Result对象。</returns>
        public static FResult Fail(string? messageFormat = null, int code = -1, params object?[] messageArgs)
            => new FResult { Success = false, MessageFormat = messageFormat, MessageArgs = messageArgs, Code = code };

        /// <summary>
        /// 创建一个失败的结果（使用ErrorCode枚举）。
        /// </summary>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="code">错误码枚举，默认为Unknown。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>失败的Result对象。</returns>
        public static FResult Fail(string? messageFormat = null, ErrorCode code = ErrorCode.Unknown, params object?[] messageArgs)
            => new FResult { Success = false, MessageFormat = messageFormat, MessageArgs = messageArgs, Code = (int)code };
    }

    /// <summary>
    /// 通用回传结果（带数据）。
    /// </summary>
    /// <typeparam name="T">返回数据类型。</typeparam>
    public class FResult<T> : FResultBase
    {
        /// <summary>
        /// 返回数据。
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 创建一个带数据的成功结果。
        /// </summary>
        /// <param name="data">返回的数据。</param>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>成功的Result&lt;T&gt;对象。</returns>
        public static FResult<T> Ok(T? data, string? messageFormat = null, params object?[] messageArgs)
            => new FResult<T> { Success = true, Data = data, MessageFormat = messageFormat, MessageArgs = messageArgs, Code = 0 };

        /// <summary>
        /// 创建一个带数据的失败结果。
        /// </summary>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="code">错误码，默认为-1。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>失败的Result&lt;T&gt;对象。</returns>
        public static FResult<T> Fail(string? messageFormat = null, int code = -1, params object?[] messageArgs)
            => new FResult<T> { Success = false, Data = default, MessageFormat = messageFormat, MessageArgs = messageArgs, Code = code };

        /// <summary>
        /// 创建一个带数据的失败结果（使用ErrorCode枚举）。
        /// </summary>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="code">错误码枚举，默认为Unknown。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>失败的Result&lt;T&gt;对象。</returns>
        public static FResult<T> Fail(string? messageFormat = null, ErrorCode code = ErrorCode.Unknown, params object?[] messageArgs)
         => new FResult<T> { Success = false, Data = default, MessageFormat = messageFormat, MessageArgs = messageArgs, Code = (int)code };

        /// <summary>
        /// 支持从T类型隐式转换为Result&lt;T&gt;，自动包装为成功结果。
        /// </summary>
        /// <param name="value">要包装的数据。</param>
        public static implicit operator FResult<T>(T value) => Ok(value);
    }
}
