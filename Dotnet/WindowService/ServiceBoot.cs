using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http.Formatting;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;//Microsoft.AspNet.WebApi.SelfHost

namespace ConsoleApp1
{
    public class ServiceBoot
    {
        static HttpSelfHostServer _server = null;

        public static async Task StartAsync()
        {
            var baseAddress = "http://127.0.0.1:8586";

            var config = new HttpSelfHostConfiguration(baseAddress);
            config.MapHttpAttributeRoutes();
            //针对所有异常的显示策略
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            var serializerSettings = config.Formatters.JsonFormatter.SerializerSettings;
            var contractResolver = (DefaultContractResolver)serializerSettings.ContractResolver;
            contractResolver.IgnoreSerializableAttribute = true;

            config.MaxReceivedMessageSize = int.MaxValue;

            config.TransferMode = TransferMode.Buffered;

            //config.Filters.Add(new ErrorFilter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = RouteParameter.Optional }
            );

            _server = new HttpSelfHostServer(config);
            await _server.OpenAsync();

            Console.WriteLine(baseAddress);
        }

        public static async Task StopAsync()
        {
            if (_server != null)
            {
                await _server.CloseAsync();
                _server.Dispose();
            }
        }
    }
}
