using System;
using System.Web;
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
        base.OnAuthorization(filterContext);

        var controller = (BaseController)filterContext.Controller;
        controller.AdminId = int.Parse(Users);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        base.AuthorizeCore(httpContext);

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
        Users = m.AdminId.ToString();
        return true;
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
