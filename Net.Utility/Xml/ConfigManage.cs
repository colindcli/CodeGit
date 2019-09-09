//修改配置文件


/// <summary>
/// 修改配置文件
/// </summary>
/// <param name="key"></param>
/// <param name="value"></param>
private static void ChangeAppSettings(string key, string value)
{
    var assemblyConfigFile = Assembly.GetEntryAssembly()?.Location;
    var config = ConfigurationManager.OpenExeConfiguration(assemblyConfigFile);
    var appSettings = (AppSettingsSection)config.GetSection("appSettings");

    appSettings.Settings.Remove(key);
    appSettings.Settings.Add(key, value);

    config.Save();
}