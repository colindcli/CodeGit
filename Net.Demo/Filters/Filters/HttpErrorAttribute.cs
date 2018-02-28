using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

/// <summary>
/// Http Error Handle
/// </summary>
public class HttpErrorAttribute : ExceptionFilterAttribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionExecutedContext"></param>
    public override void OnException(HttpActionExecutedContext actionExecutedContext)
    {
        LogHelper.Fatal(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);

        var result = new ResponseResult<string>()
        {
            Code = (int)ReturnCode.SystemError,
            Message = actionExecutedContext.Exception.Message,
            Data = actionExecutedContext.Exception.ToString()
        };
        actionExecutedContext.Response = new HttpResponseMessage()
        {
            Content = new ObjectContent<IResponseResult>(result, new JsonMediaTypeFormatter(), "application/json")
        };

        base.OnException(actionExecutedContext);
    }
}
