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
