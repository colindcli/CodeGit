using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

/// <summary>
/// 
/// </summary>
public class WebApiApplication : HttpApplication
{
    /// <summary>
    /// 
    /// </summary>
    protected void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();
        GlobalConfiguration.Configure(WebApiConfig.Register);
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        HttpFilterConfig.RegisterGlobalFilters(GlobalConfiguration.Configuration.Filters);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
        //json.MediaTypeMappings.Add(new QueryStringMapping("format", "json", "application/json"));
        //json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        //json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        //json.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        //json.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
        json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
        json.SerializerSettings.Culture = new CultureInfo("zh-CN");

        GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

        //log4net
        var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config");
        log4net.Config.XmlConfigurator.Configure(fileInfo);
    }
}
