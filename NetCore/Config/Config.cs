using Microsoft.Extensions.Configuration;
using System;

/// <summary>
/// 配置
/// </summary>
public class Config
{
    public static IConfiguration Configuration { get; private set; }

    public static void SetAppSettings(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public static string GetValue(string key)
    {
        if (Configuration == null)
        {
            throw new Exception();
        }
        var section = Configuration.GetSection(key);
        return section?.Value ?? "";
    }

    public static string Demo => GetValue("AppSettings:Demo");
}