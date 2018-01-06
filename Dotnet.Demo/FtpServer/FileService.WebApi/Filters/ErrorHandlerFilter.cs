using FileService.Common;
using System.Web.Http.Filters;

namespace FileService.WebApi.Filters
{
    public class ErrorHandlerFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            LogHelper.Fatal(actionExecutedContext.Exception.Message + "; URL：" + actionExecutedContext.Request.RequestUri, actionExecutedContext.Exception);
        }
    }
}