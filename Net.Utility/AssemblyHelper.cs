using System;
using System.Reflection;

public class AssemblyHelper
{
    /// <summary>
    /// 创建对象
    /// </summary>
    /// <typeparam name="T">返回对象</typeparam>
    /// <param name="assemblyString">一般是Dll名称(不带后缀名)</param>
    /// <param name="namespaceClass">命名空间.类名</param>
    /// <returns></returns>
    public static T CreateObject<T>(string assemblyString, string namespaceClass)
    {
        return (T)Assembly.Load(assemblyString).CreateInstance(namespaceClass);
    }

    /// <summary>
    /// 调用对象的私有方法
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="methodName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static object CallObjectMethod<T>(T instance, string methodName, object[] param = null)
    {
        var method = instance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        return method?.Invoke(instance, param);
    }

    /// <summary>
    /// 创建静态类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assemblyString"></param>
    /// <param name="namespaceClass"></param>
    /// <returns></returns>
    public static Type CreateStaticClass<T>(string assemblyString, string namespaceClass)
    {
        return Assembly.Load(assemblyString).GetType(namespaceClass);
    }

    /// <summary>
    /// 调用类静态方法
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static object CallStaticMethod(Type type, string methodName, object[] param = null)
    {
        return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, param);
    }
}
