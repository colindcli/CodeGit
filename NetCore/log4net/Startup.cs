public void ConfigureServices(IServiceCollection services)
{
    var repository = LogManager.CreateRepository("NetCoreRepository");
    XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
}