using System.Collections.Generic;
using System.Configuration;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var section = (Dictionary<string, string>)ConfigurationManager.GetSection("pays/alipay");
            var appId = section["appId"];
            var gatewayUrl = section["gatewayUrl"];
        }
    }
}
