// 运行控制台

using System.Diagnostics;
using System.Text;

public class ProcessHelper
{
    /// <summary>
    /// 执行exe文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="arguments">多个用空格分隔</param>
    /// <returns></returns>
    public static string RunProcess(string fileName, string arguments = "")
    {
        var p = new Process
        {
            StartInfo =
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                StandardOutputEncoding = Encoding.UTF8,
                RedirectStandardError = true,
                FileName = fileName,
                Arguments = arguments,
                CreateNoWindow = true,
            }
        };
        p.Start();
        p.WaitForExit();
        return p.StandardOutput.ReadToEnd();
    }
}