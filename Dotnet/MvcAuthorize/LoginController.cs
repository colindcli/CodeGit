public class LoginController : Controller
{
    private void SetAdmin(int adminId)
    {
        var model = new TokenModel()
        {
            AdminId = adminId,
            ExpiryDate = DateTime.Today.AddDays(1)
        };
        var token = TokenHelper.GetToken(model);
        CookieHelper.SetCookie("Access_Token", token, model.ExpiryDate);
    }

    // GET: Admin/Login
    public ActionResult Index()
    {
        SetAdmin(100);
        return View();
    }
}
