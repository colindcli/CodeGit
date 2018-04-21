using System.Web;

public class IpHelper
{
    /// <summary>
    /// 获取Web客户端IP地址
    /// </summary>
    /// <param name="current">System.Web.HttpContext.Current</param>
    /// <returns></returns>
    public static string GetWebClientIp(HttpContext current)
    {
        if (current?.Request.ServerVariables == null)
            return null;
        var request = current.Request;
        return request.Headers["Cdn-Src-Ip"] ?? request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.ServerVariables["REMOTE_ADDR"] ?? request.UserHostAddress;
    }

    //ip 获取地址：http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=
}
