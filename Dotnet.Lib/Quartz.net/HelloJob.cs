using Quartz;
using System;
using System.Threading.Tasks;

public class HelloJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync(DateTime.Now.ToLongTimeString() + "Greetings from HelloJob!");
    }
}
