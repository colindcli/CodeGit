using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// 
/// </summary>
public class HttpService
{
    private static string _userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.75 Safari/537.36";
    private static string _cookie = "";
    private static string _acceptLanguage = "zh-CN,zh;q=0.9";
    private static string _cerPath = "";

    public HttpService(string userAgent = null, string cookie = null, string acceptLanguage = null, string cerPath = null)
    {
        if (!string.IsNullOrWhiteSpace(userAgent))
        {
            _userAgent = userAgent;
        }
        if (!string.IsNullOrWhiteSpace(cookie))
        {
            _cookie = cookie;
        }
        if (!string.IsNullOrWhiteSpace(acceptLanguage))
        {
            _acceptLanguage = acceptLanguage;
        }
        if (!string.IsNullOrWhiteSpace(cerPath))
        {
            _cerPath = cerPath;
        }
    }

    /// <summary>
    /// Cookie字符串转CookieContainer
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cookie"></param>
    /// <returns></returns>
    private static CookieContainer ToCookieCollection(Uri uri, string cookie)
    {
        if (string.IsNullOrWhiteSpace(cookie))
        {
            return null;
        }

        var cookies = new CookieCollection();
        var domain = uri.Host;
        var items = cookie.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        items.ForEach(item =>
        {
            var kvs = item.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (kvs.Count == 1)
            {
                var k = kvs[0].Trim();
                cookies.Add(new Cookie(k, "", "/", domain));
            }
            else if (kvs.Count == 2)
            {
                var k = kvs[0].Trim();
                var v = kvs[1].Trim();
                cookies.Add(new Cookie(k, v, "/", domain));
            }
            else if (kvs.Count > 2)
            {
                var k = kvs[0].Trim();
                var list = new List<string>();
                for (var i = 1; i < kvs.Count; i++)
                {
                    list.Add(kvs[i]);
                }
                var v = string.Join("=", list);
                cookies.Add(new Cookie(k, v, "/", domain));
            }
        });

        var cc = new CookieContainer();
        cc.Add(cookies);
        return cc;
    }

    /// <summary>
    /// 请求页面
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static IRestResponse GetRequest(string url)
    {
        var client = new RestClient(url);
        //userAgent
        if (!string.IsNullOrWhiteSpace(_userAgent))
        {
            client.UserAgent = _userAgent;
        }
        //cookie
        client.CookieContainer = ToCookieCollection(new Uri(url), _cookie);
        client.ConfigureWebRequest(req =>
        {
            req.Accept = "*/*";
            req.Referer = url;
            if (!string.IsNullOrWhiteSpace(_acceptLanguage))
            {
                req.Headers.Add("Accept-Language", _acceptLanguage);
            }
            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                req.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                if (!string.IsNullOrWhiteSpace(_cerPath) && File.Exists(_cerPath))
                {
                    req.ClientCertificates = new X509CertificateCollection { new X509Certificate(_cerPath) };
                }
            }
        });
        var request = new RestRequest();
        var response = client.Execute(request);

        return response;
    }

    /// <summary>
    /// 登录获取Cookie
    /// </summary>
    /// <param name="url"></param>
    /// <param name="formData"></param>
    public static string GetLoginCookie(string url, Dictionary<string, string> formData)
    {
        var client = new RestClient(url);
        //userAgent
        if (!string.IsNullOrWhiteSpace(_userAgent))
        {
            client.UserAgent = _userAgent;
        }

        client.ConfigureWebRequest(req =>
        {
            req.Accept = "*/*";
            if (!string.IsNullOrWhiteSpace(_acceptLanguage))
            {
                req.Headers.Add("Accept-Language", _acceptLanguage);
            }
            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                req.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                if (!string.IsNullOrWhiteSpace(_cerPath) && File.Exists(_cerPath))
                {
                    req.ClientCertificates = new X509CertificateCollection { new X509Certificate(_cerPath) };
                }
            }
        });
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        foreach (var kv in formData)
        {
            request.AddParameter(kv.Key, kv.Value);
        }
        var response = client.Execute(request);

        var cookie = string.Empty;
        if (response.IsSuccessful)
        {
            cookie = string.Join(";", response.Cookies?.Select(p => $"{p.Name}={p.Value}") ?? new List<string>());
        }
        return cookie;
    }
}