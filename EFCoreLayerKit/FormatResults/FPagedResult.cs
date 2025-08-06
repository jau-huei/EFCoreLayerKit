namespace EFCoreLayerKit.Results
{
    /// <summary>
    /// 分页结果（带数据与分页信息）。
    /// </summary>
    /// <typeparam name="T">数据项类型。</typeparam>
    public class FPagedResult<T> : FResultBase
    {
        /// <summary>
        /// 返回数据。
        /// </summary>
        public List<T>? Data { get; set; }

        /// <summary>
        /// 总记录数。
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 当前页码（从1开始）。
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数量。
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 创建一个成功的分页结果。
        /// </summary>
        /// <param name="data">返回的数据列表。</param>
        /// <param name="total">总记录数。</param>
        /// <param name="pageIndex">当前页码（从1开始）。</param>
        /// <param name="pageSize">每页数量。</param>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>成功的 <see cref="FPagedResult{T}"/> 对象。</returns>
        public static FPagedResult<T> Ok(List<T>? data, int total, int pageIndex, int pageSize, string? messageFormat = null, params object?[] messageArgs)
            => new FPagedResult<T>
            {
                Success = true,
                Data = data,
                Total = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                MessageFormat = messageFormat,
                MessageArgs = messageArgs,
                Code = 0
            };

        /// <summary>
        /// 创建一个失败的分页结果。
        /// </summary>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="code">错误码，默认为-1。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>失败的 <see cref="FPagedResult{T}"/> 对象。</returns>
        public static FPagedResult<T> Fail(string? messageFormat = null, int code = -1, params object?[] messageArgs)
            => new FPagedResult<T>
            {
                Success = false,
                Data = new List<T>(),
                Total = 0,
                PageIndex = 1,
                PageSize = 0,
                MessageFormat = messageFormat,
                MessageArgs = messageArgs,
                Code = code,
            };

        /// <summary>
        /// 创建一个失败的分页结果（使用ErrorCode枚举）。
        /// </summary>
        /// <param name="messageFormat">消息模板键或格式化字符串。</param>
        /// <param name="code">错误码枚举，默认为Unknown。</param>
        /// <param name="messageArgs">可选的消息参数。</param>
        /// <returns>失败的 <see cref="FPagedResult{T}"/> 对象。</returns>
        public static FPagedResult<T> Fail(string? messageFormat = null, ErrorCode code = ErrorCode.Unknown, params object?[] messageArgs)
            => new FPagedResult<T>
            {
                Success = false,
                Data = new List<T>(),
                Total = 0,
                PageIndex = 1,
                PageSize = 0,
                MessageFormat = messageFormat,
                MessageArgs = messageArgs,
                Code = (int)code,
            };

        /// <summary>
        /// 将当前分页结果转换为另一种数据类型的分页结果，保留分页信息和状态，仅替换数据列表。
        /// </summary>
        /// <typeparam name="U">目标数据项类型。</typeparam>
        /// <param name="data">要设置的新数据列表。</param>
        /// <returns>包含新数据类型的 <see cref="FPagedResult{U}"/> 实例。</returns>
        public FPagedResult<U> AsResult<U>(List<U>? data)
        {
            return new FPagedResult<U>()
            {
                Success = this.Success,
                Code = this.Code,
                CreatedAt = this.CreatedAt,
                MessageFormat = this.MessageFormat,
                MessageArgs = this.MessageArgs,
                Data = data,
                Total = this.Total,
                PageIndex = this.PageIndex,
                PageSize = this.PageSize
            };
        }

        /// <summary>
        /// 将当前分页结果的数据列表通过指定转换函数转换为另一种类型，生成新的分页结果，保留原有分页信息和状态。
        /// </summary>
        /// <typeparam name="U">目标数据项类型。</typeparam>
        /// <param name="converter">用于将数据项从 T 转换为 U 的转换函数。</param>
        /// <returns>包含转换后数据类型的 <see cref="FPagedResult{U}"/> 实例。</returns>
        public FPagedResult<U> AsResult<U>(Func<T, U> converter)
        {
            return new FPagedResult<U>()
            {
                Success = this.Success,
                Code = this.Code,
                CreatedAt = this.CreatedAt,
                MessageFormat = this.MessageFormat,
                MessageArgs = this.MessageArgs,
                Data = this.Data?.Select(converter).ToList(),
                Total = this.Total,
                PageIndex = this.PageIndex,
                PageSize = this.PageSize
            };
        }
    }
}
