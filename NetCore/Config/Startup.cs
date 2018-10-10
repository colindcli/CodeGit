public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //配置
        Config.SetAppSettings(Configuration);
    }
}