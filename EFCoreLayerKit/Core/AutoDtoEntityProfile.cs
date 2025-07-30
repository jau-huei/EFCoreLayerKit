using AutoMapper;
using EFCoreLayerKit.Attributes;
using EFCoreLayerKit.DTOs;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// 提供自动为带有 DtoForEntityAttribute 的 DTO 与 Entity 注册 AutoMapper 映射的静态方法。
    /// <para>使用方式：</para>
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
        /// 自动查找所有继承 BaseDto 且带有 DtoForEntityAttribute 的类型，
        /// 并为其与对应 EntityType 注册 AutoMapper 双向映射。
        /// </summary>
        /// <param name="profile">AutoMapper Profile 实例</param>
        public static void RegisterDtoEntityMaps(this Profile profile)
        {
            // 查找所有 BaseDto 派生类
            var dtoTypes = TypeDiscoveryUtil.GetAllDerivedTypes<BaseDto>();
            foreach (var dtoType in dtoTypes)
            {
                // 查找带有 DtoForEntityAttribute 的 DTO
                var attr = dtoType.GetCustomAttributes(typeof(DtoForEntityAttribute), false)
                    .FirstOrDefault() as DtoForEntityAttribute;
                if (attr != null && attr.EntityType != null)
                {
                    // 注册双向映射
                    profile.CreateMap(dtoType, attr.EntityType);
                    profile.CreateMap(attr.EntityType, dtoType);
                }
            }
        }
    }
}
