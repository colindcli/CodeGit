//递归获取父节点、子节点
using System;
using System.Collections.Generic;
using System.Linq;

public static class ObjectExtension
{
    /// <summary>
    /// 获取所有父节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPkDataType">Id字段数据类型</typeparam>
    /// <param name="list">数据列表</param>
    /// <param name="id">当前Id值</param>
    /// <param name="idField">Id字段</param>
    /// <param name="parentIdField">ParentId字段</param>
    /// <param name="includeSelf">是否包含自己节点</param>
    /// <param name="depthAct">设置深度值: (obj, depth) => obj.Depth = depth</param>
    /// <returns></returns>
    public static List<T> GetParentNodes<T, TPkDataType>(this List<T> list, TPkDataType id, Func<T, TPkDataType> idField, Func<T, TPkDataType> parentIdField, bool includeSelf = false, Action<T, int> depthAct = null)
    {
        var obj = list.Find(li => idField.Invoke(li).Equals(id));
        if (obj == null)
        {
            return null;
        }

        var rows = new List<T>();
        bool Predicate(T li, T o) => idField.Invoke(li).Equals(parentIdField.Invoke(o));
        RecursiveNode(list, obj, Predicate, ref rows, 2);
        rows.Reverse();
        if (includeSelf)
        {
            rows.Add(obj);
        }

        if (depthAct != null)
        {
            for (var i = 0; i < rows.Count; i++)
            {
                depthAct.Invoke(rows[i], i + 1);
            }
        }
        return rows;
    }

    /// <summary>
    /// 获取所有子节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPkDataType">Id字段数据类型</typeparam>
    /// <param name="list">数据列表</param>
    /// <param name="id">当前id值</param>
    /// <param name="idField">Id字段</param>
    /// <param name="parentIdField">ParentId字段</param>
    /// <param name="includeSelf">是否包含自己节点</param>
    /// <param name="depthAct">设置深度值: (obj, depth) => obj.Depth = depth</param>
    /// <returns></returns>
    public static List<T> GetChildNodes<T, TPkDataType>(this List<T> list, TPkDataType id, Func<T, TPkDataType> idField, Func<T, TPkDataType> parentIdField, bool includeSelf = false, Action<T, int> depthAct = null)
    {
        var obj = list.Find(li => idField.Invoke(li).Equals(id));
        if (obj == null)
        {
            return null;
        }

        var rows = new List<T>();
        if (includeSelf)
        {
            depthAct?.Invoke(obj, 1);
            rows.Add(obj);
        }
        bool Predicate(T li, T o) => parentIdField.Invoke(li).Equals(idField.Invoke(o));
        RecursiveNode(list, obj, Predicate, ref rows, 2, depthAct);
        return rows;
    }

    /// <summary>
    /// 递归节点
    /// </summary>
    private static void RecursiveNode<T>(this List<T> list, T obj, Func<T, T, bool> predicate, ref List<T> child, int depthVal, Action<T, int> depthAct = null)
    {
        var items = list.Where(li => predicate.Invoke(li, obj)).ToList();
        if (items.Count == 0)
        {
            return;
        }
        foreach (var item in items)
        {
            depthAct?.Invoke(item, depthVal);
            child.Add(item);
            RecursiveNode(list, item, predicate, ref child, depthVal + 1, depthAct);
        }
    }
}