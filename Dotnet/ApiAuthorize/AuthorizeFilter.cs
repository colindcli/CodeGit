using System.Web.Http;

/// <summary>
/// 权限过滤
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeFilter : AuthorizeAttribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionContext"></param>
    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        base.HandleUnauthorizedRequest(actionContext);

        //自定义返回错误消息到前端
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionContext"></param>
    /// <returns></returns>
    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        var baseAuthor = base.IsAuthorized(actionContext);
        if (!baseAuthor)
            return false;

        //获取用户内容

        return true;
    }
}
