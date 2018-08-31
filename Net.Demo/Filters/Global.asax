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

        var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
        settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        settings.Culture = new CultureInfo("zh-CN");
        settings.Converters.Add(new LongJsonConvert());

        GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

        //log4net
        var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config");
        log4net.Config.XmlConfigurator.Configure(fileInfo);
    }
}

/// <summary>
/// long转string
/// </summary>
public class LongJsonConvert : JsonConverter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return reader.Value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objectType"></param>
    /// <returns></returns>
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(long);
    }
}
