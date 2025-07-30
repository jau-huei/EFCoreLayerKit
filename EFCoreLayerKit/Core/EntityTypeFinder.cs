using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EFCoreLayerKit.Core
{
    /// <summary>
    /// �����ࣺ���ڲ��Ҽ̳�ָ����������зǳ������ࡣ
    /// </summary>
    public static class TypeDiscoveryUtil
    {
        /// <summary>
        /// ��ȡ���м̳�ָ�����ࣨ��ʵ��ָ���ӿڣ��ķǳ����������͡�
        /// Ĭ�ϻ�ɨ�赱ǰ AppDomain �������Ѽ��صĳ��򼯣�֧�ַ������ͻ��ࣨ�� MyBase&lt;&gt;����
        /// 
        /// ע�����
        /// - ��� <paramref name="baseType"/> ����ͨ���ͣ���ȼ��� baseType.IsAssignableFrom(targetType)��
        /// - ��� <paramref name="baseType"/> �ǿ��ŷ������ͣ��� typeof(MyBase&lt;&gt;)����
        ///   ����������Ƿ�̳��˷������Ͷ��壨֧�ֶ��̳У���
        /// - ���صĽ�����������������౾��
        /// </summary>
        /// <param name="baseType">�������ͻ�ӿ����ͣ���Ϊ���ŷ������͡�</param>
        /// <returns>���м̳л�ʵ�� baseType �ķǳ��������ʵ���༯�ϡ�</returns>
        public static IEnumerable<Type> GetAllDerivedTypes(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Type.EmptyTypes; } // ĳЩ��̬���򼯿����׳��쳣
                })
                .Where(t => t.IsClass && !t.IsAbstract && IsDerivedFrom(t, baseType));
        }

        /// <summary>
        /// �ж�һ�������Ƿ�̳���ָ�����ࣨ�������ͻ��ඨ�壩��ʵ����ָ���ӿڡ�
        /// </summary>
        /// <param name="type">Ҫ�������͡�</param>
        /// <param name="baseType">�����ӿ����ͣ������ǿ��ŷ��͡�</param>
        /// <returns>����̳л�ʵ���ˣ����� true��</returns>
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
        /// ��������Ƿ�̳���ָ���������Ͷ��壨�� MyBase&lt;&gt;����
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
        /// ��������Ƿ�ʵ����ָ�����ͽӿڶ��壨�� IRepository&lt;&gt;����
        /// </summary>
        private static bool ImplementsGenericInterface(Type type, Type genericInterfaceType)
        {
            return type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType);
        }

        /// <summary>
        /// ���Ͱ汾����ȡ���м̳� T �ķǳ����������͡�
        /// Ĭ�ϲ��������Ѽ��صĳ��򼯡�
        /// </summary>
        public static IEnumerable<Type> GetAllDerivedTypes<T>()
        {
            return GetAllDerivedTypes(typeof(T));
        }
    }
}
