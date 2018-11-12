// api开启Session

public class WebApiApplication : HttpApplication
{
    /// <summary>
    /// api中开启Session
    /// </summary>
    public override void Init()
    {
        PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        base.Init();
    }
}


//api中调用

HttpContext.Current.Session["SafeCode"]