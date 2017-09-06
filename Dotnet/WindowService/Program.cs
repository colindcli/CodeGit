using System;
using System.ServiceProcess;

namespace ConsoleApp1
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var isDebug = false;
            if (isDebug)
            {
                ServiceBoot.StartAsync().Wait();
                Console.WriteLine("服务启动");
                Console.ReadLine();
                ServiceBoot.StopAsync().Wait();
            }
            else
            {
                ServiceBase.Run(new HostService());
            }
        }
    }
}
