## 随机数


```C#
var rd = new Random();
for (var i = 0; i < 100; i++)
{
    //输出值x为：x>=0 且 x<2的整数，即随机数只有0和1
    Console.WriteLine(rd.Next(0, 2));
}
```