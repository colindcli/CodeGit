using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeProject.Common;
using WeProject.WebApiPc.filters;

namespace WeProject.WebApiPc
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configuration.Filters.Add(new ErrorHandlerFilter());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMapper();

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.MediaTypeMappings.Add(new QueryStringMapping("format", "json", "application/json"));
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.SerializerSettings.Culture = new CultureInfo("zh-CN");

            //log4net
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config");
            log4net.Config.XmlConfigurator.Configure(fileInfo);
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(Object sender, EventArgs e)
        {
            var ex = Server.GetLastError().GetBaseException();
            LogHelper.Fatal($"url:{Request.UrlReferrer} Application_Error:{ex.Message}", ex);
            Server.ClearError();
        }
    }
}
