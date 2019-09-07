//修改配置文件


private static void ChangeConfiguration()
{
    var assemblyConfigFile = Assembly.GetEntryAssembly()?.Location;
    var config = ConfigurationManager.OpenExeConfiguration(assemblyConfigFile);
    var appSettings = (AppSettingsSection)config.GetSection("appSettings");

    appSettings.Settings.Remove("Version");
    appSettings.Settings.Add("Version", DateTime.Now.ToString("HH:mm:ss"));

    config.Save();
}