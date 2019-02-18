
protected void Application_Start(object sender, EventArgs e)
{
    LoginProvider.UseFaceBook("client_id", "client_secret");

    LoginProvider.UseQQ("client_id", "client_secret");

    LoginProvider.UseWechat("client_id", "client_secret");

    LoginProvider.UseWeibo("client_id", "client_secret");

    LoginProvider.UseKakao("client_id");
}