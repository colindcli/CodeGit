using FileService.Common;
using FileService.WebApi.Filters;
using System;
using System.IO;
using System.Web.Http;

namespace FileService.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new ErrorHandlerFilter());

            //任务调度
            TaskConfig.RegisterQuartz();

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
