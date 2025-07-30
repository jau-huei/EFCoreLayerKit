namespace EFCoreLayerKit.Results
{
    /// <summary>
    /// �����붨�塣
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// �޴���
        /// </summary>
        None = 0,

        // ͨ�ô���
        /// <summary>
        /// δ֪����
        /// </summary>
        Unknown = 1,
        /// <summary>
        /// ������Ч��
        /// </summary>
        InvalidParameter = 2,
        /// <summary>
        /// ������ʱ��
        /// </summary>
        Timeout = 3,
        /// <summary>
        /// δ��Ȩ��
        /// </summary>
        Unauthorized = 4,
        /// <summary>
        /// ��ֹ���ʡ�
        /// </summary>
        Forbidden = 5,
        /// <summary>
        /// δ�ҵ���Դ��
        /// </summary>
        NotFound = 6,
        /// <summary>
        /// ��Դ��ͻ��
        /// </summary>
        Conflict = 7,
        /// <summary>
        /// ����ʧ�ܡ�
        /// </summary>
        OperationFailed = 8,
        /// <summary>
        /// �����쳣��
        /// </summary>
        Exception = 9,

        // PLC��ش���
        /// <summary>
        /// PLC����ʧ�ܡ�
        /// </summary>
        PlcConnectionFailed = 1001,
        /// <summary>
        /// PLC��ȡʧ�ܡ�
        /// </summary>
        PlcReadFailed = 1002,
        /// <summary>
        /// PLCд��ʧ�ܡ�
        /// </summary>
        PlcWriteFailed = 1003,
        /// <summary>
        /// PLC������ʱ��
        /// </summary>
        PlcTimeout = 1004,
        /// <summary>
        /// PLC���ݸ�ʽ����
        /// </summary>
        PlcDataFormatError = 1005,

        // ���ݿ���ش���
        /// <summary>
        /// ���ݿ�����ʧ�ܡ�
        /// </summary>
        DatabaseConnectionFailed = 2001,
        /// <summary>
        /// ���ݿ��ѯʧ�ܡ�
        /// </summary>
        DatabaseQueryFailed = 2002,
        /// <summary>
        /// ���ݿ����ʧ�ܡ�
        /// </summary>
        DatabaseUpdateFailed = 2003,
        /// <summary>
        /// ���ݿ������ʱ��
        /// </summary>
        DatabaseTimeout = 2004,
        /// <summary>
        /// ���ݿ����ݸ�ʽ����
        /// </summary>
        DatabaseDataFormatError = 2005,

        // HTTP��ش���
        /// <summary>
        /// HTTP����ʧ�ܡ�
        /// </summary>
        HttpRequestFailed = 3001,
        /// <summary>
        /// HTTP����ʱ��
        /// </summary>
        HttpTimeout = 3002,
        /// <summary>
        /// HTTPδ��Ȩ��
        /// </summary>
        HttpUnauthorized = 3003,
        /// <summary>
        /// HTTP��ֹ���ʡ�
        /// </summary>
        HttpForbidden = 3004,
        /// <summary>
        /// HTTPδ�ҵ���Դ��
        /// </summary>
        HttpNotFound = 3005,
        /// <summary>
        /// HTTP����������
        /// </summary>
        HttpServerError = 3006,

        // �ļ���IO��ش���
        /// <summary>
        /// �ļ�δ�ҵ���
        /// </summary>
        FileNotFound = 4001,
        /// <summary>
        /// �ļ���ȡʧ�ܡ�
        /// </summary>
        FileReadFailed = 4002,
        /// <summary>
        /// �ļ�д��ʧ�ܡ�
        /// </summary>
        FileWriteFailed = 4003,
        /// <summary>
        /// Ŀ¼δ�ҵ���
        /// </summary>
        DirectoryNotFound = 4004,
        /// <summary>
        /// ���̿ռ䲻�㡣
        /// </summary>
        DiskFull = 4005,
        /// <summary>
        /// IO�쳣��
        /// </summary>
        IoException = 4006,

        // ������ͨ����ش���
        /// <summary>
        /// ���粻���á�
        /// </summary>
        NetworkUnavailable = 5001,
        /// <summary>
        /// DNS����ʧ�ܡ�
        /// </summary>
        DnsResolveFailed = 5002,
        /// <summary>
        /// Socket����
        /// </summary>
        SocketError = 5003,
        /// <summary>
        /// ���ӱ��ܾ���
        /// </summary>
        ConnectionRefused = 5004,
        /// <summary>
        /// ���ӱ����á�
        /// </summary>
        ConnectionReset = 5005,

        // ��֤����Ȩ��ش���
        /// <summary>
        /// Token�ѹ��ڡ�
        /// </summary>
        TokenExpired = 6001,
        /// <summary>
        /// Token��Ч��
        /// </summary>
        InvalidToken = 6002,
        /// <summary>
        /// Ȩ�޲��㡣
        /// </summary>
        PermissionDenied = 6003,

        // ҵ���߼���ش���
        /// <summary>
        /// Υ��ҵ�����
        /// </summary>
        BusinessRuleViolated = 7001,
        /// <summary>
        /// ����У��ʧ�ܡ�
        /// </summary>
        DataValidationFailed = 7002,
        /// <summary>
        /// �����ظ���
        /// </summary>
        DuplicateData = 7003,

        // ������ش���
        /// <summary>
        /// ����δ���С�
        /// </summary>
        CacheMiss = 8001,
        /// <summary>
        /// ����д��ʧ�ܡ�
        /// </summary>
        CacheWriteFailed = 8002,

        // ������ش���
        /// <summary>
        /// ���񲻿��á�
        /// </summary>
        ServiceUnavailable = 9001,
        /// <summary>
        /// ����ʱ��
        /// </summary>
        ServiceTimeout = 9002,
        /// <summary>
        /// ��������ʧ�ܡ�
        /// </summary>
        DependencyFailure = 9003,

        // ���л�/�����л���ش���
        /// <summary>
        /// ���л�ʧ�ܡ�
        /// </summary>
        SerializationFailed = 10001,
        /// <summary>
        /// �����л�ʧ�ܡ�
        /// </summary>
        DeserializationFailed = 10002,

        // ��Ϣ����/�м����ش���
        /// <summary>
        /// ��Ϣ���в����á�
        /// </summary>
        MessageQueueUnavailable = 11001,
        /// <summary>
        /// ��Ϣ����ʧ�ܡ�
        /// </summary>
        MessagePublishFailed = 11002,
        /// <summary>
        /// ��Ϣ����ʧ�ܡ�
        /// </summary>
        MessageConsumeFailed = 11003,
    }
}
