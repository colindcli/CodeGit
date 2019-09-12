//https://www.nuget.org/packages/DwrUtility
//Install-Package DwrUtility
using System.Web;

public class IpHelper
{
    /// <summary>
    /// 获取Web客户端IP地址(有反向代理是跳过代理获取客户端IP)
    /// </summary>
    /// <param name="current">System.Web.HttpContext.Current</param>
    /// <returns></returns>
    public static string GetWebClientIpWithProxy(HttpContext current)
    {
        if (current?.Request.ServerVariables == null)
        {
            return null;
        }

        var request = current.Request;
        return request.Headers["Cdn-Src-Ip"] ?? request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.ServerVariables["REMOTE_ADDR"] ?? request.UserHostAddress;
    }

    /// <summary>
    /// 获取Web客户端IP地址(直接获取REMOTE_ADDR的IP地址)
    /// </summary>
    /// <param name="current">System.Web.HttpContext.Current</param>
    /// <returns></returns>
    public static string GetWebClientIp(HttpContext current)
    {
        if (current?.Request.ServerVariables == null)
        {
            return null;
        }

        var request = current.Request;
        return request.ServerVariables["REMOTE_ADDR"] ?? request.UserHostAddress;
    }

    //ip 获取地址：http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=
}


// ip地址库: https://github.com/lionsoul2014/ip2region 下载ip库ip2region.db
// 安装：Install-Package IP2Region

var path = $"{AppDomain.CurrentDomain.BaseDirectory}App_Data/ip2region.db";
var db = new DbSearcher(path);
var region = db.BtreeSearch(ip)?.Region ?? "";