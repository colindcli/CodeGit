// 获取list的前一条记录、后一条记录
using System.Collections.Generic;
using System.Linq;

public static class ObjectExtension
{
    /// <summary>
    /// 获取当前索引值的前一条
    /// </summary>
    /// <param name="list"></param>
    /// <param name="index">当前索引值</param>
    /// <returns></returns>
    public static T GetPrevious<T>(this List<T> list, int index) where T : class
    {
        return index - 1 < 0 ? null : list[index - 1];
    }

    /// <summary>
    /// 获取当前索引值的后一条
    /// </summary>
    /// <param name="list"></param>
    /// <param name="index">当前索引值</param>
    /// <returns></returns>
    public static T GetNext<T>(this List<T> list, int index) where T : class
    {
        return index + 1 > list.Count - 1 ? null : list[index + 1];
    }

    /// <summary>
    /// 获取当前索引值的前n条
    /// </summary>
    /// <param name="list"></param>
    /// <param name="index">当前索引值</param>
    /// <param name="n">获取数量</param>
    /// <returns></returns>
    public static List<T> GetPreviousList<T>(this List<T> list, int index, int n = 1) where T : new()
    {
        if (n < 1)
        {
            return new List<T>();
        }

        var start = index - n;
        return list.Skip(start < 0 ? 0 : start).Take(start < 0 ? n + start : n).ToList();
    }

    /// <summary>
    /// 获取当前索引值的后n条
    /// </summary>
    /// <param name="list"></param>
    /// <param name="index">当前索引值</param>
    /// <param name="n">获取数量</param>
    /// <returns></returns>
    public static List<T> GetNextList<T>(this List<T> list, int index, int n = 1) where T : new()
    {
        if (n < 1)
        {
            return new List<T>();
        }

        return list.Skip(index + 1).Take(n).ToList();
    }
}