using System;
using System.Diagnostics;
using System.IO;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://www.baidu.com/";
            var fileName = "";
            if (ExportFile(url, out fileName))
            {
                Console.WriteLine(fileName);
            }
            Console.WriteLine("ok!");

            Console.ReadKey();
        }

        /// <summary>
        /// phantomjs生成文件(phantomjs version:2.1.1.0)
        /// </summary>
        /// <returns></returns>
        public static bool ExportFile(string url, out string fileName)
        {
            //目录（路径不含空格）
            var root = "D:\\";
            //root = AppDomain.CurrentDomain.BaseDirectory;
            fileName = root + Guid.NewGuid() + ".pdf";
            var cmd = $@"{root + "phantomjs.exe"} {root + "print.js"}  -url {url} -filename {fileName} -pdfSize A3 -timeout 1000";
            return RunCmd("cmd.exe", cmd) && File.Exists(fileName);
        }

        /// <summary>
        /// 运行cmd命令
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        private static bool RunCmd(string cmdExe, string cmdStr)
        {
            var result = false;
            try
            {
                using (var myPro = new Process())
                {
                    //指定启动进程是调用的应用程序和命令行参数
                    var psi = new ProcessStartInfo(cmdExe, cmdStr)
                    {
                        Arguments = " /c " + cmdStr,
                        // 不显示dos 窗口
                        CreateNoWindow = true,
                        // 是否指定操作系统外壳进程启动程序，没有这行，调试时编译器会通知你加上的...orz
                        UseShellExecute = false,
                        // 重新定向标准输入、输出流
                        RedirectStandardInput = true,
                        RedirectStandardOutput = false
                    };

                    myPro.StartInfo = psi;
                    myPro.Start();
                    myPro.WaitForExit();
                    result = true;
                }
            }
            catch
            {
                // ignored
            }
            return result;
        }
    }
}
