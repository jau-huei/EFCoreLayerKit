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
        /// ��ȡ���м̳�ָ������ķǳ����������͡�
        /// Ĭ�ϲ��������Ѽ��صĳ��򼯡�
        /// </summary>
        /// <param name="baseType">�������͡�</param>
        /// <returns>���м̳� baseType �ķǳ����������ͼ��ϡ�</returns>
        public static IEnumerable<Type> GetAllDerivedTypes(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));
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
