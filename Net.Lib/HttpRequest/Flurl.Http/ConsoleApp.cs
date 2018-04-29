using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(async () =>
            {
                await Get("https://www.baidu.com/");
            });
            t.Wait();

            Console.ReadKey();
        }

        public static async Task Get(string url)
        {
            var txt = @"Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9
Cache-Control: max-age=0
Connection: keep-alive
Cookie: BAIDUID=79CF7BCFFCA391A4C5CFD4850173DD80:FG=1; PSTM=1524384785; BIDUPSID=E8CD28F2EC42343D55F67B81FD196E07; BD_UPN=12314353; BDUSS=5sY3lPN1FHbVVyaUo2UlI3RkZBUFlDZ1hyeEhvRnZZcEtMRkVlVzNqSUNtZ3BiQVFBQUFBJCQAAAAAAAAAAAEAAACnLXdrAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIN41oCDeNaNm; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BD_HOME=1; H_PS_PSSID=1431_21109_26309_26106; BDRCVFR[feWj1Vr5u3D]=I67x6TjHwwYf0; BD_CK_SAM=1; PSINO=2; H_PS_645EC=1b8243bKk79hSmsRJ3UcIYOsu5vI6NvmqeUFVDKA4%2By0G%2B3Rr2Fuj1LsOAC3BlCcBrpm; sugstore=1
Host: www.baidu.com
Upgrade-Insecure-Requests: 1
User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36";
            var headResp = await url.SetHeader(txt).GetAsync();
            var code = headResp.StatusCode;
            
            Console.WriteLine(headResp.Content.ReadAsStringAsync().Result);
            Console.WriteLine(code);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class HeaderHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientOrRequest"></param>
        /// <param name="headerContent"></param>
        /// <returns></returns>
        public static IFlurlRequest SetHeader(this string clientOrRequest, string headerContent)
        {
            var dicts = new Dictionary<string, object>();
            var kvs = GetNameValue(headerContent, ref dicts);
            var result = clientOrRequest.WithHeaders(kvs).WithCookies(dicts);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookieString"></param>
        /// <returns></returns>
        private static Dictionary<string, object> GetCookieNameValue(string cookieString)
        {
            var dicts = new Dictionary<string, object>();
            if (string.IsNullOrWhiteSpace(cookieString))
            {
                return dicts;
            }
            var cookieNvs = cookieString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in cookieNvs)
            {
                var index = p.IndexOf('=');
                if (index <= -1)
                    continue;
                var name = p.Substring(0, index).Trim();
                var value = p.Substring(index + 1, p.Length - index - 1).Trim();
                do
                {
                    value = value.Replace(",", "");
                } while (value.IndexOf(",", StringComparison.OrdinalIgnoreCase) > -1);
                dicts.Add(name, value);
            }
            return dicts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerStr"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        private static Dictionary<string, object> GetNameValue(string headerStr, ref Dictionary<string, object> cookies)
        {
            var dicts = new Dictionary<string, object>();
            if (string.IsNullOrWhiteSpace(headerStr))
            {
                return dicts;
            }

            var lines = headerStr.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var index = line.IndexOf(":", StringComparison.OrdinalIgnoreCase);
                    if (index > -1)
                    {
                        var name = line.Substring(0, index).Trim().Replace(":", "");
                        var value = line.Substring(index + 1, line.Length - name.Length - 1).Trim();

                        if (string.Equals("Cookie", name, StringComparison.OrdinalIgnoreCase))
                        {
                            cookies = GetCookieNameValue(value);
                        }
                        else
                        {
                            dicts.Add(name, value);
                        }
                    }
                }
            }
            return dicts;
        }
    }
}
