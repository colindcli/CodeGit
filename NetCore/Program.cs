using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

public class Program
{
    
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            //.ConfigureLogging(factory =>
            //{
            //    factory.AddConsole();
            //    factory.AddFilter("Console", level => level >= LogLevel.Information);
            //})
            //.UseKestrel()
            //.UseContentRoot(Directory.GetCurrentDirectory())
            //.UseIISIntegration()
            .UseStartup<Startup>();
}