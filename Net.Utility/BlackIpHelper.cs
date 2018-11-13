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
    private static readonly int Seconds = 60;
    private static readonly List<Ips> BlackIpLists = new List<Ips>();
    private static readonly List<Ips> IpLists = new List<Ips>();

    /// <summary>
    /// 是否机器人Ip
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsRobotIp(string ip)
    {
        if (!Config.InterceptIp)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(ip))
        {
            return true;
        }

        if (BlackIpLists.Exists(p => p.Ip.Equals(ip)))
        {
            return true;
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
            return true;
        }

        return false;
    }

    public class Ips
    {
        public string Ip { get; set; }
        public long Ticks { get; set; }
    }
}