using System.Configuration;

public class Config
{
    private static string GetValue(string name)
    {
        return ConfigurationManager.AppSettings[name];
    }
}
