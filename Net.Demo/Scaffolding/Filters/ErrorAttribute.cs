using System.Text;
using System.Web.Mvc;

/// <summary>
/// Mvc Error Handle
/// </summary>
public class ErrorAttribute : HandleErrorAttribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterContext"></param>
    public override void OnException(ExceptionContext filterContext)
    {
        LogHelper.Fatal(filterContext.Exception.Message, filterContext.Exception);

        filterContext.Result = new ContentResult()
        {
            Content = filterContext.Exception.Message,
            ContentEncoding = Encoding.UTF8,
            ContentType = "text/html"
        };
        filterContext.ExceptionHandled = true;

        base.OnException(filterContext);
    }
}
