using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VerificationCode
{
    class Program
    {
        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromFile(string filePath, string password);

        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromBuffer(byte[] fileBuffer, int fileBufLen, string password);

        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromFile(string filePath, StringBuilder vcode);

        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromBuffer(byte[] fileBuffer, int imgBufLen, StringBuilder vcode);

        [DllImport("WmCode.dll")]
        public static extern bool SetWmOption(int optionIndex, int optionValue);

        [DllImport("urlmon.dll", EntryPoint = "URLDownloadToFileA")]
        public static extern int URLDownloadToFile(int pCaller, string szUrl, string szFileName, int dwReserved, int lpfnCb);

        static void Main(string[] args)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var datPath =$"{root}test.dat";

            if (LoadWmFromFile(datPath, "123"))
            {
                //SetWmOption(6, 90);

                //http://api2-287.zcgsrg.com:65/cloud/api.do?pa=captcha.next&key=94824340
                var imgPath = $"{root}safeCode.jpg";

                var result = new StringBuilder();
                GetImageFromFile(imgPath, result);
                Console.WriteLine(result.ToString());

                Console.ReadKey();
            }
        }
    }
}
