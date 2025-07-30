using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// 工具类：用于查找继承指定基类的所有非抽象子类。
    /// </summary>
    public static class TypeDiscoveryUtil
    {
        /// <summary>
        /// 获取所有继承指定基类的非抽象子类类型。
        /// 默认查找所有已加载的程序集。
        /// </summary>
        /// <param name="baseType">基类类型。</param>
        /// <returns>所有继承 baseType 的非抽象子类类型集合。</returns>
        public static IEnumerable<Type> GetAllDerivedTypes(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));
        }

        /// <summary>
        /// 泛型版本，获取所有继承 T 的非抽象子类类型。
        /// 默认查找所有已加载的程序集。
        /// </summary>
        public static IEnumerable<Type> GetAllDerivedTypes<T>()
        {
            return GetAllDerivedTypes(typeof(T));
        }
    }
}
