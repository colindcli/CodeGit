using IdGen;
using System;

/// <summary>
/// 计算
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        var mc = new MaskConfig(49, 0, 14);

        //Js最大值：9007199254740992
        long jsMaxValue = 9007199254740992;
        for (var i = 0; i < 2000; i++)
        {
            var gen = new IdGenerator(0, DateTime.Now.AddYears(-i), mc);
            var id = gen.CreateId();

            if (id > jsMaxValue)
            {
                Console.WriteLine($"第{i}年: {id}，超出Javascript最大数值：{jsMaxValue}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"生成器个数: {mc.MaxGenerators}");
                Console.WriteLine($"每秒每台生成Id数: {mc.MaxSequenceIds}");
                Console.WriteLine($"每秒总生成Id数: {mc.MaxGenerators * mc.MaxSequenceIds}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"当前配置最多可用{i - 1}年哈");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"第{i}年: {id}");
            }
        }
        Console.WriteLine("可用超2000年");
        Console.ReadKey();
    }
}
