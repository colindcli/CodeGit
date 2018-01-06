using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

/// <summary>
/// 权限过滤
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeApiFilter : AuthorizeAttribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionContext"></param>
    /// <returns></returns>
    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        base.IsAuthorized(actionContext);

        var token = CookieHelper.GetCookie("Access_Token");
        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }
        var m = TokenHelper.GetModel<TokenModel>(token);
        if (m == null)
        {
            return false;
        }
        if (m.ExpiryDate < DateTime.Now)
        {
            return false;
        }
        var controller = actionContext.ControllerContext.Controller as BaseApiController;
        if (controller == null)
        {
            return false;
        }
        controller.AdminId = m.AdminId;
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionContext"></param>
    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        base.HandleUnauthorizedRequest(actionContext);

        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
        {
            Content = new StringContent("没有授权")
        };
    }
}
