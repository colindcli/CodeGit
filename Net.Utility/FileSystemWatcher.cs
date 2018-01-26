using System;
using System.IO;

/// <summary>
/// 监视文件夹变化
/// </summary>
/// <param name="directory"></param>
public void FileWatch(string directory)
{
    var fw = new FileSystemWatcher(directory);

    fw.Created += (o, f) =>
    {

    };
    fw.Changed += (o, f) =>
    {

    };
    fw.Renamed += (o, f) =>
    {

    };
    fw.Deleted += (o, f) =>
    {

    };

    fw.EnableRaisingEvents = true;
}


//例子
public class XmlConfig
{
    private static readonly string Directory = $@"{AppDomain.CurrentDomain.BaseDirectory}App_Data/ConfigData";
    public static void WatchXml()
    {
        var fw = new FileSystemWatcher(Directory);
        fw.Changed += (o, f) =>
        {
            ConfigData = GetSqlMapper<ConfigData>("PageConfig");
        };
        fw.EnableRaisingEvents = true;
    }

    private static T GetSqlMapper<T>(string xmlFileName) where T : new()
    {
        return (T)new YAXLib.YAXSerializer(typeof(T)).DeserializeFromFile($@"{Directory}/{xmlFileName}.xml");
    }

    public static ConfigData ConfigData = GetSqlMapper<ConfigData>("PageConfig");
}
