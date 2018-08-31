using IdGen;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 生成Id类
/// 第一部分，1位为标识位，不用。
/// 第二部分，41位，用来记录当前时间与标记时间twepoch的毫秒数的差值，41位的时间截，可以使用69年，T = (1L 右位移 41) / (1000L * 60 * 60 * 24 * 365) = 69
/// 第三部分，10位，用来记录当前节点的信息，支持2的10次方台机器
/// 第四部分，12位，用来支持每个节点每毫秒(同一机器，同一时间截)产生4096个ID序号
/// </summary>
public class IdHelper
{
    /// <summary>
    /// new MaskConfig(50, 0, 13), 根据Js最大值计算，可用34年，每秒产生8192个Id
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