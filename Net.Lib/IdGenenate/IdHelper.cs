using IdGen;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 生成Id类
/// </summary>
public class IdHelper
{
    /// <summary>
    /// 
    /// </summary>
    private static readonly IdGenerator Generator = new IdGenerator(0, new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), new MaskConfig(50, 0, 13));

    /// <summary>
    /// 生成一个Id
    /// </summary>
    public static long Id => Generator.CreateId();

    /// <summary>
    /// 生成指定个数连续的Id
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static List<long> Take(int number)
    {
        return Generator.Take(number).ToList();
    }
}