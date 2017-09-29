using System.Web.Http.Filters;
using WeProject.Common;

namespace WeProject.WebApiPc.filters
{
    public class ErrorHandlerFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            LogHelper.Fatal(actionExecutedContext.Exception.Message + "; URL：" + actionExecutedContext.Request.RequestUri, actionExecutedContext.Exception);
        }
    }
}