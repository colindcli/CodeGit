using System;
using System.Collections.Generic;
using System.ComponentModel;

public class EnumHelper
{
    /// <summary>
    /// 获取枚举项的描述
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="value">数值</param>
    /// <returns></returns>
    public static string GetEnumDescription<TEnum>(int value)
    {
        var enumType = typeof(TEnum);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("不是枚举类型");
        }

        var enumItem = Enum.GetName(enumType, Convert.ToInt32(value));
        if (enumItem == null)
        {
            return string.Empty;
        }
        var objs = enumType.GetField(enumItem).GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (objs.Length == 0)
        {
            return string.Empty;
        }
        var attr = objs[0] as DescriptionAttribute;
        return attr?.Description;
    }

    /// <summary>
    /// 枚举列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Dictionary<int, string> GetEnumList<T>()
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("不是枚举类型");
        }
        var lists = new Dictionary<int, string>();
        foreach (int i in Enum.GetValues(enumType))
        {
            var name = GetEnumDescription<T>(i);
            lists.Add(i, name);
        }
        return lists;
    }
}
