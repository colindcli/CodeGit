using System;
using System.Text;

namespace VerificationCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://api2-287.zcgsrg.com:65/cloud/api.do?pa=captcha.next&key=94824340
            var imgPath = $"{AppDomain.CurrentDomain.BaseDirectory}safeCode.jpg";

            var code = VerificationCodeHelper.GetCode(imgPath, "123");
            Console.WriteLine(code);

            Console.ReadKey();
        }
    }
}
