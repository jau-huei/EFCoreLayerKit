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
        /// 获取所有继承指定基类（或实现指定接口）的非抽象子类类型。
        /// 默认会扫描当前 AppDomain 中所有已加载的程序集，支持泛型类型基类（如 MyBase&lt;&gt;）。
        /// 
        /// 注意事项：
        /// - 如果 <paramref name="baseType"/> 是普通类型，则等价于 baseType.IsAssignableFrom(targetType)。
        /// - 如果 <paramref name="baseType"/> 是开放泛型类型（如 typeof(MyBase&lt;&gt;)），
        ///   则会检查类型是否继承了泛型类型定义（支持多层继承）。
        /// - 返回的结果不包括抽象类或基类本身。
        /// </summary>
        /// <param name="baseType">基类类型或接口类型，可为开放泛型类型。</param>
        /// <returns>所有继承或实现 baseType 的非抽象子类或实现类集合。</returns>
        public static IEnumerable<Type> GetAllDerivedTypes(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Type.EmptyTypes; } // 某些动态程序集可能抛出异常
                })
                .Where(t => t.IsClass && !t.IsAbstract && IsDerivedFrom(t, baseType));
        }

        /// <summary>
        /// 判断一个类型是否继承自指定基类（包括泛型基类定义）或实现了指定接口。
        /// </summary>
        /// <param name="type">要检查的类型。</param>
        /// <param name="baseType">基类或接口类型，可以是开放泛型。</param>
        /// <returns>如果继承或实现了，返回 true。</returns>
        private static bool IsDerivedFrom(Type type, Type baseType)
        {
            if (baseType.IsGenericTypeDefinition)
            {
                return InheritsFromGenericDefinition(type, baseType)
                    || ImplementsGenericInterface(type, baseType);
            }
            else
            {
                return baseType.IsAssignableFrom(type);
            }
        }

        /// <summary>
        /// 检查类型是否继承自指定泛型类型定义（如 MyBase&lt;&gt;）。
        /// </summary>
        private static bool InheritsFromGenericDefinition(Type type, Type genericBaseType)
        {
            while (type != null && type != typeof(object))
            {
                var current = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (current == genericBaseType)
                    return true;

                type = type.BaseType!;
            }
            return false;
        }

        /// <summary>
        /// 检查类型是否实现了指定泛型接口定义（如 IRepository&lt;&gt;）。
        /// </summary>
        private static bool ImplementsGenericInterface(Type type, Type genericInterfaceType)
        {
            return type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType);
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
