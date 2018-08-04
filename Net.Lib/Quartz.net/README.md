> 任务调度 v3.0.0
- https://github.com/colindcli/CodeGit/issues/6
- https://www.nuget.org/packages/Quartz/
- Cron表达式 http://cron.qqe2.com/

- 反编译Cron：[github](https://github.com/bradymholt/cron-expression-descriptor) / [Nuget](https://www.nuget.org/packages/CronExpressionDescriptor/)

- eg:
ExpressionDescriptor.GetDescription("0 0 0/1 * * ? ", new Options()
{
    DayOfWeekStartIndexZero = false, //false周一；true周日
    Use24HourTimeFormat = true,
    Verbose = true, //是否输出详细信息
    Locale = "zh-cn"
});