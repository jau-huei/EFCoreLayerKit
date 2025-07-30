namespace EFCoreLayerKit.Results
{
    /// <summary>
    /// 错误码定义。
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 无错误。
        /// </summary>
        None = 0,

        // 通用错误
        /// <summary>
        /// 未知错误。
        /// </summary>
        Unknown = 1,
        /// <summary>
        /// 参数无效。
        /// </summary>
        InvalidParameter = 2,
        /// <summary>
        /// 操作超时。
        /// </summary>
        Timeout = 3,
        /// <summary>
        /// 未授权。
        /// </summary>
        Unauthorized = 4,
        /// <summary>
        /// 禁止访问。
        /// </summary>
        Forbidden = 5,
        /// <summary>
        /// 未找到资源。
        /// </summary>
        NotFound = 6,
        /// <summary>
        /// 资源冲突。
        /// </summary>
        Conflict = 7,
        /// <summary>
        /// 操作失败。
        /// </summary>
        OperationFailed = 8,
        /// <summary>
        /// 发生异常。
        /// </summary>
        Exception = 9,

        // PLC相关错误
        /// <summary>
        /// PLC连接失败。
        /// </summary>
        PlcConnectionFailed = 1001,
        /// <summary>
        /// PLC读取失败。
        /// </summary>
        PlcReadFailed = 1002,
        /// <summary>
        /// PLC写入失败。
        /// </summary>
        PlcWriteFailed = 1003,
        /// <summary>
        /// PLC操作超时。
        /// </summary>
        PlcTimeout = 1004,
        /// <summary>
        /// PLC数据格式错误。
        /// </summary>
        PlcDataFormatError = 1005,

        // 数据库相关错误
        /// <summary>
        /// 数据库连接失败。
        /// </summary>
        DatabaseConnectionFailed = 2001,
        /// <summary>
        /// 数据库查询失败。
        /// </summary>
        DatabaseQueryFailed = 2002,
        /// <summary>
        /// 数据库更新失败。
        /// </summary>
        DatabaseUpdateFailed = 2003,
        /// <summary>
        /// 数据库操作超时。
        /// </summary>
        DatabaseTimeout = 2004,
        /// <summary>
        /// 数据库数据格式错误。
        /// </summary>
        DatabaseDataFormatError = 2005,

        // HTTP相关错误
        /// <summary>
        /// HTTP请求失败。
        /// </summary>
        HttpRequestFailed = 3001,
        /// <summary>
        /// HTTP请求超时。
        /// </summary>
        HttpTimeout = 3002,
        /// <summary>
        /// HTTP未授权。
        /// </summary>
        HttpUnauthorized = 3003,
        /// <summary>
        /// HTTP禁止访问。
        /// </summary>
        HttpForbidden = 3004,
        /// <summary>
        /// HTTP未找到资源。
        /// </summary>
        HttpNotFound = 3005,
        /// <summary>
        /// HTTP服务器错误。
        /// </summary>
        HttpServerError = 3006,

        // 文件与IO相关错误
        /// <summary>
        /// 文件未找到。
        /// </summary>
        FileNotFound = 4001,
        /// <summary>
        /// 文件读取失败。
        /// </summary>
        FileReadFailed = 4002,
        /// <summary>
        /// 文件写入失败。
        /// </summary>
        FileWriteFailed = 4003,
        /// <summary>
        /// 目录未找到。
        /// </summary>
        DirectoryNotFound = 4004,
        /// <summary>
        /// 磁盘空间不足。
        /// </summary>
        DiskFull = 4005,
        /// <summary>
        /// IO异常。
        /// </summary>
        IoException = 4006,

        // 网络与通信相关错误
        /// <summary>
        /// 网络不可用。
        /// </summary>
        NetworkUnavailable = 5001,
        /// <summary>
        /// DNS解析失败。
        /// </summary>
        DnsResolveFailed = 5002,
        /// <summary>
        /// Socket错误。
        /// </summary>
        SocketError = 5003,
        /// <summary>
        /// 连接被拒绝。
        /// </summary>
        ConnectionRefused = 5004,
        /// <summary>
        /// 连接被重置。
        /// </summary>
        ConnectionReset = 5005,

        // 认证与授权相关错误
        /// <summary>
        /// Token已过期。
        /// </summary>
        TokenExpired = 6001,
        /// <summary>
        /// Token无效。
        /// </summary>
        InvalidToken = 6002,
        /// <summary>
        /// 权限不足。
        /// </summary>
        PermissionDenied = 6003,

        // 业务逻辑相关错误
        /// <summary>
        /// 违反业务规则。
        /// </summary>
        BusinessRuleViolated = 7001,
        /// <summary>
        /// 数据校验失败。
        /// </summary>
        DataValidationFailed = 7002,
        /// <summary>
        /// 数据重复。
        /// </summary>
        DuplicateData = 7003,

        // 缓存相关错误
        /// <summary>
        /// 缓存未命中。
        /// </summary>
        CacheMiss = 8001,
        /// <summary>
        /// 缓存写入失败。
        /// </summary>
        CacheWriteFailed = 8002,

        // 服务相关错误
        /// <summary>
        /// 服务不可用。
        /// </summary>
        ServiceUnavailable = 9001,
        /// <summary>
        /// 服务超时。
        /// </summary>
        ServiceTimeout = 9002,
        /// <summary>
        /// 依赖服务失败。
        /// </summary>
        DependencyFailure = 9003,

        // 序列化/反序列化相关错误
        /// <summary>
        /// 序列化失败。
        /// </summary>
        SerializationFailed = 10001,
        /// <summary>
        /// 反序列化失败。
        /// </summary>
        DeserializationFailed = 10002,

        // 消息队列/中间件相关错误
        /// <summary>
        /// 消息队列不可用。
        /// </summary>
        MessageQueueUnavailable = 11001,
        /// <summary>
        /// 消息发布失败。
        /// </summary>
        MessagePublishFailed = 11002,
        /// <summary>
        /// 消息消费失败。
        /// </summary>
        MessageConsumeFailed = 11003,
    }
}
