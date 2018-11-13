//var ip = Request?.ServerVariables?.Get("REMOTE_ADDR");

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// IP拦截
/// </summary>
public class BlackIpHelper
{
    /// <summary>
    /// 启用拦截ip
    /// </summary>
    private static readonly bool InterceptIp = true;
    private static readonly int Seconds = 60;
    private static readonly List<Ips> BlackIpLists = new List<Ips>();
    private static readonly List<Ips> IpLists = new List<Ips>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsRobot(string ip)
    {
        if (!InterceptIp)
        {
            return true;
        }

        if (string.IsNullOrWhiteSpace(ip))
        {
            return false;
        }

        if (BlackIpLists.Exists(p => p.Ip.Equals(ip)))
        {
            return false;
        }

        var item = new Ips()
        {
            Ip = ip,
            Ticks = DateTime.Now.Ticks
        };
        var overLine = item.Ticks - Seconds * 10000000;

        Task.Run(() =>
        {
            lock (IpLists)
            {
                IpLists.RemoveAll(p => p.Ticks < overLine);
            }
        });

        var total = 0;
        lock (IpLists)
        {
            IpLists.Add(item);
            total = IpLists.Count(p => p.Ip.Equals(ip) && p.Ticks > overLine);
        }

        if (total > Seconds)
        {
            BlackIpLists.Add(item);
            return false;
        }

        return true;
    }

    public class Ips
    {
        public string Ip { get; set; }
        public long Ticks { get; set; }
    }
}