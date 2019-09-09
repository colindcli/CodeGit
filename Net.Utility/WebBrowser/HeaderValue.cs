//获取浏览器UserAgent或Cookie

private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
{
    var wb = (WebBrowser)sender;
    //userAgent
    var window = wb.Document?.Window?.DomWindow;
    var wt = window?.GetType();
    var navigator = wt?.InvokeMember("navigator", BindingFlags.GetProperty, null, window, new object[] { });
    var nt = navigator?.GetType();
    var userAgent = nt?.InvokeMember("userAgent", BindingFlags.GetProperty, null, navigator, new object[] { });
    var ua = userAgent?.ToString();
    
    //Cookie
    var cookie = wb.Document?.Cookie;
}