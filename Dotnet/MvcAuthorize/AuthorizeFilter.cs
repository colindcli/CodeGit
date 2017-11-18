using System;
using System.Security.Claims;
using System.Web.Mvc;

/// <summary>
/// 权限过滤
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeFilter : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        var token = CookieHelper.GetCookie("Access_Token");
        if (string.IsNullOrWhiteSpace(token))
        {
            filterContext.Result = new HttpUnauthorizedResult();
            return;
        }
        var m = TokenHelper.GetModel<TokenModel>(token);
        if (m == null)
        {
            filterContext.Result = new HttpUnauthorizedResult();
            return;
        }
        if (m.ExpiryDate < DateTime.Now)
        {
            filterContext.Result = new HttpUnauthorizedResult();
            return;
        }

        filterContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(m.AdminId.ToString()));
        base.OnAuthorization(filterContext);
    }
}
