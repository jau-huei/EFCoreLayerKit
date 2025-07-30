namespace EFCoreLayerKit.QueryModels
{
    /// <summary>
    /// ����ѡ�֧�ֶ༶����
    /// </summary>
    public class OrderByOption
    {
        /// <summary>
        /// ������������ơ�
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool Ascending { get; set; } = true;

        /// <summary>
        /// ����һ������ѡ�
        /// </summary>
        /// <param name="propertyName">������������ơ�</param>
        /// <param name="ascending">�Ƿ�����</param>
        public OrderByOption(string propertyName, bool ascending = true)
        {
            PropertyName = propertyName;
            Ascending = ascending;
        }
    }
}
