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
            var datPath =$"{root}163.dat";

            if (LoadWmFromFile(datPath, "163"))
            {
                //SetWmOption(6, 90);
                var imgPath = $"{root}code.jpg";

                var result = new StringBuilder();
                GetImageFromFile(imgPath, result);
                Console.WriteLine(result.ToString());

                Console.ReadKey();
            }
        }
    }
}
