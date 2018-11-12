## 验证码

/// <summary>
/// 验证码
/// </summary>
public class SafeCodeController : Controller
{
    /// <summary>
    /// 
    /// </summary>
    [HttpGet]
    public ActionResult Index()
    {
        Session["SafeCode"] = SafeCodeHelper.GenerateSafeCode(out var ms);
        return new FileStreamResult(ms, "image/Jpeg");
    }
}