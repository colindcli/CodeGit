using System.Diagnostics;
using System.Text;

public class ExcuteExeHelper
{
    /// <summary>
    /// 执行exe文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string RunProcess(string fileName)
    {
        var p = new Process
        {
            StartInfo =
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8,
                FileName = fileName,
                CreateNoWindow = true,
            }
        };
        p.Start();
        p.WaitForExit();
        return p.StandardOutput.ReadToEnd();
    }
}