//递归获取父节点、子节点
//https://www.nuget.org/packages/DwrUtility
//Install-Package DwrUtility
using System;
using System.Collections.Generic;
using System.Linq;

public static class ObjectExtension
{
    /// <summary>
    /// 获取表达式字段名
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TR"></typeparam>
    /// <returns></returns>
    public static string GetPropertyName<T, TR>(Expression<Func<T, TR>> expression)
    {
        if (expression == null)
        {
            return "";
        }

        var rtn = "";
        var body = expression.Body as UnaryExpression;
        if (body != null)
        {
            rtn = ((MemberExpression)body.Operand).Member.Name;
        }
        else if (expression.Body is MemberExpression)
        {
            rtn = ((MemberExpression)expression.Body).Member.Name;
        }
        else if (expression.Body is ParameterExpression)
        {
            rtn = ((ParameterExpression)expression.Body).Type.Name;
        }
        return rtn;
    }

    /// <summary>
    /// 获取所有父节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPk">Id字段数据类型</typeparam>
    /// <param name="list">数据列表</param>
    /// <param name="id">当前Id值</param>
    /// <param name="idField">Id字段</param>
    /// <param name="parentIdField">ParentId字段</param>
    /// <param name="includeSelf">是否包含自己节点</param>
    /// <param name="depth">深度</param>
    /// <returns></returns>
    public static List<T> GetParentNodes<T, TPk>(this List<T> list, TPk id, Func<T, TPk> idField, Func<T, TPk> parentIdField, bool includeSelf = false, Expression<Func<T, int>> depth = null)
    {
        var obj = list.Find(li => idField.Invoke(li).Equals(id));
        if (obj == null)
        {
            return null;
        }

        var type = typeof(T);
        var depthProperty = type.GetProperty(GetPropertyName(depth));

        var rows = new List<T>();
        bool Predicate(T li, T o) => idField.Invoke(li).Equals(parentIdField.Invoke(o));
        RecursiveNode(list, obj, Predicate, ref rows, 2, depthProperty);
        rows.Reverse();
        if (includeSelf)
        {
            rows.Add(obj);
        }

        if (depthProperty != null)
        {
            for (var i = 0; i < rows.Count; i++)
            {
                depthProperty.SetValue(rows[i], i + 1);
            }
        }
        return rows;
    }

    /// <summary>
    /// 获取所有子节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPk">Id字段数据类型</typeparam>
    /// <param name="list">数据列表</param>
    /// <param name="id">从list中找此Id及以下节点</param>
    /// <param name="idField">Id字段</param>
    /// <param name="parentIdField">ParentId字段</param>
    /// <param name="includeSelf">是否包含自己节点</param>
    /// <param name="depth">深度</param>
    /// <returns></returns>
    public static List<T> GetChildNodes<T, TPk>(this List<T> list, TPk id, Func<T, TPk> idField, Func<T, TPk> parentIdField, bool includeSelf = false, Expression<Func<T, int>> depth = null)
    {
        var obj = list.Find(li => idField.Invoke(li).Equals(id));
        if (obj == null)
        {
            return null;
        }

        var type = typeof(T);
        var depthProperty = type.GetProperty(GetPropertyName(depth));

        var rows = new List<T>();
        if (includeSelf)
        {
            depthProperty?.SetValue(obj, 1);
            rows.Add(obj);
        }
        bool Predicate(T li, T o) => parentIdField.Invoke(li).Equals(idField.Invoke(o));
        RecursiveNode(list, obj, Predicate, ref rows, 2, depthProperty);
        return rows;
    }

    /// <summary>
    /// 递归节点
    /// </summary>
    private static void RecursiveNode<T>(this List<T> list, T obj, Func<T, T, bool> predicate, ref List<T> child, int depthVal, PropertyInfo depthProperty)
    {
        var items = list.Where(li => predicate.Invoke(li, obj)).ToList();
        if (items.Count == 0)
        {
            return;
        }
        foreach (var item in items)
        {
            depthProperty?.SetValue(item, depthVal);
            child.Add(item);
            RecursiveNode(list, item, predicate, ref child, depthVal + 1, depthProperty);
        }
    }
}