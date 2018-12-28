## IdGen  Id生成器

- IdGen: [NuGet](https://www.nuget.org/packages/IdGen/) / [doc](https://github.com/RobThree/IdGen) MIT


> 第一部分，1位为标识位，不用。
> 第二部分，41位，用来记录当前时间与标记时间twepoch的毫秒数的差值，41位的时间截，可以使用69年，T = (1L 右位移 41) / (1000L * 60 * 60 * 24 * 365) = 69
> 第三部分，10位，用来记录当前节点的信息，支持2的10次方台机器
> 第四部分，12位，用来支持每个节点每毫秒(同一机器，同一时间截)产生4096个ID序号


- 根据Js最大值（9007199254740992）计算: Main.cs
> new MaskConfig(47, 0, 16);  4年，每秒65536个
> new MaskConfig(48, 0, 15);  8年，每秒32768个
> new MaskConfig(49, 0, 14);  17年，每秒16384个
> new MaskConfig(50, 0, 13);  34年，每秒8192个
> new MaskConfig(51, 0, 12);  69年，每秒4096个
> new MaskConfig(52, 0, 11);  139年，每秒2048个
> new MaskConfig(53, 0, 10);  278年，每秒1024个


- 如果突破Js最大值限制，需要在Json返回值将long转string，参考：Global.asax.cs


## 有顺序的Guid生成器

- 有序Guid：[NewId](https://www.nuget.org/packages/NewId/) / [doc](https://github.com/phatboyg/NewId) Apache2.0
- 另一个生成Guid：[SequentialGuid](https://github.com/jhtodd/SequentialGuid)
- 相关文档：https://www.cnblogs.com/supersnowyao/p/8335397.html



## 分布式/独立/有序/无状态/线程安全/随机Id生成器

- Funcular.IdGenerators: [NuGet](https://www.nuget.org/packages/Funcular.IdGenerators/) / [doc](https://github.com/piranout/Funcular.IdGenerators)



## 短Id (低重复)

- shortid: [NuGet](https://www.nuget.org/packages/shortid/) / [doc](https://github.com/bolorundurowb/shortid/) MIT
