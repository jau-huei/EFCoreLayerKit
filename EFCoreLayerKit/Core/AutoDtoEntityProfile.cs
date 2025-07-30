using AutoMapper;
using EFCoreLayerKit.Attributes;
using EFCoreLayerKit.DTOs;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// �ṩ�Զ�Ϊ���� DtoForEntityAttribute �� DTO �� Entity ע�� AutoMapper ӳ��ľ�̬������
    /// <para>ʹ�÷�ʽ��</para>
    /// <code>
    /// public class MyProfile : Profile
    /// {
    ///     public MyProfile()
    ///     {
    ///         AutoDtoEntityProfile.RegisterDtoEntityMaps(this);
    ///     }
    /// }
    /// </code>
    /// </summary>
    public static class AutoDtoEntityProfile
    {
        /// <summary>
        /// �Զ��������м̳� BaseDto �Ҵ��� DtoForEntityAttribute �����ͣ�
        /// ��Ϊ�����Ӧ EntityType ע�� AutoMapper ˫��ӳ�䡣
        /// </summary>
        /// <param name="profile">AutoMapper Profile ʵ��</param>
        public static void RegisterDtoEntityMaps(this Profile profile)
        {
            // �������� BaseDto ������
            var dtoTypes = TypeDiscoveryUtil.GetAllDerivedTypes<BaseDto>();
            foreach (var dtoType in dtoTypes)
            {
                // ���Ҵ��� DtoForEntityAttribute �� DTO
                var attr = dtoType.GetCustomAttributes(typeof(DtoForEntityAttribute), false)
                    .FirstOrDefault() as DtoForEntityAttribute;
                if (attr != null && attr.EntityType != null)
                {
                    // ע��˫��ӳ��
                    profile.CreateMap(dtoType, attr.EntityType);
                    profile.CreateMap(attr.EntityType, dtoType);
                }
            }
        }
    }
}
