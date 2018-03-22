//代码仅供参考

/// <summary>
/// 刷新授权
/// </summary>
/// <param name="returnUrl"></param>
/// <returns></returns>
public ActionResult ToAuthorize(string returnUrl)
{
    var token = CookieHelper.GetCookie("MobileToken");
    var m = string.IsNullOrWhiteSpace(token) ? null : TokenHelper.GetModel<TokenModel>(token);
    if (m != null && m.ExpiresIn > DateTime.Now)
    {
        return Redirect(HttpUtility.UrlDecode(returnUrl));
    }

    var toUrl = $"{Common.Config.MobileApi}/Wechat/AuthorizeRedirect";
    Session["ReturnUrl"] = returnUrl;
    return RedirectToAction("ToUrl", new { path = toUrl });
}

/// <summary>
/// 授权后从定向
/// </summary>
/// <returns></returns>
public ActionResult AuthorizeRedirect(string code)
{
    var result = OAuth.GetAccessToken(Common.Config.AppId, Common.Config.AppSecret, code);
    if (result.errcode == ReturnCode.请求成功)
    {
        var expiresIn = DateTime.Now.AddSeconds(result.expires_in);
        var token = TokenHelper.GetToken(new TokenModel()
        {
            AccessToken = result.access_token,
            OpenId = result.openid,
            RefreshToken = result.refresh_token,
            ExpiresIn = expiresIn
        });
        CookieHelper.SetCookie("MobileToken", token, expiresIn);
    }

    var toUrl = Session["ReturnUrl"]?.ToString();
    toUrl = $"{toUrl}{(toUrl != null && toUrl.Contains("?") ? "&" : "?")}ver{new Random().Next(0, 100)}";
    return Redirect(toUrl);
}