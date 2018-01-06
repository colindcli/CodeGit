using System;
using System.Web.Mvc;

/// <summary>
/// 权限过滤
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeFilter : AuthorizeAttribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterContext"></param>
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        var token = CookieHelper.GetCookie("Access_Token");
        if (string.IsNullOrWhiteSpace(token))
        {
            base.OnAuthorization(filterContext);
            return;
        }
        var m = TokenHelper.GetModel<TokenModel>(token);
        if (m == null)
        {
            base.OnAuthorization(filterContext);
            return;
        }
        if (m.ExpiryDate < DateTime.Now)
        {
            base.OnAuthorization(filterContext);
            return;
        }
        var controller = filterContext.Controller as BaseController;
        if (controller == null)
        {
            base.OnAuthorization(filterContext);
            return;
        }
        controller.AdminId = m.AdminId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterContext"></param>
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        filterContext.Result = new RedirectResult("/Admin/Login");
    }
}
