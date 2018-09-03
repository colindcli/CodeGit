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
        var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
        settings.Converters.Add(new LongJsonConvert());
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
