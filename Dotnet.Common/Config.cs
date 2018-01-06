using System.Configuration;

public class Config
{
    private static string GetValue(string name)
    {
        return ConfigurationManager.AppSettings[name];
    }

    /// <summary>
    /// 产生Token的密钥
    /// </summary>
    public static readonly string Secret = GetValue("Secret");
}
