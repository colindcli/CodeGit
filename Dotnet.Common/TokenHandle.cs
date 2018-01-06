//（引用：CookieHelper.cs/TokenHelper.cs）
public class TokenHandle
{
    private const string TokenName = "Access_Token";

    public static void SetToken(User user, bool isLogin)
    {
        var model = new TokenModel()
        {
            UserModel = new UserModel()
            {
                IsLogin = isLogin,
                UserId = user.UserId
            },
            ExpiryDate = DateTime.Today.AddDays(1).AddHours(5)
        };
        var token = TokenHelper.GetToken(model);
        CookieHelper.SetCookie(TokenName, token, model.ExpiryDate);
    }

    public static TokenModel GetToken()
    {
        var token = CookieHelper.GetCookie(TokenName);
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var m = TokenHelper.GetModel<TokenModel>(token);
        if (m == null)
            return null;

        if (m.ExpiryDate < DateTime.Now)
            return null;

        return m;
    }

    public static void ClearTaken()
    {
        CookieHelper.ClearCookie(TokenName);
    }
}

public class UserModel
{
    public bool IsLogin { get; set; }
    public Guid UserId { get; set; }
}