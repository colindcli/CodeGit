using IdGen;
using System;
using System.Collections.Generic;
using System.Linq;

public class IdHelper
{
    private static readonly IdGenerator Generator = new IdGenerator(0, new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Utc), new MaskConfig(45, 2, 16));

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