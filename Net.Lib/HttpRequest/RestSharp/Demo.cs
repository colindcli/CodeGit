using RestSharp;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public class RestSharpReq
{
    private static IRestResponse GetRequest(string url, string userAgent, string cookies, string acceptLanguage = "zh-CN", string cerPath = null)
    {
        var client = new RestClient(url);
        client.ConfigureWebRequest(req =>
        {
            req.Accept = "*/*";
            req.Headers.Add("Accept-Language", acceptLanguage);
            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                req.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                if (!string.IsNullOrWhiteSpace(cerPath) && File.Exists(cerPath))
                {
                    req.ClientCertificates = new X509CertificateCollection { new X509Certificate(cerPath) };
                }
            }
            if (!string.IsNullOrWhiteSpace(userAgent))
            {
                req.Headers.Set("UserAgent", userAgent);
            }
            if (!string.IsNullOrWhiteSpace(cookies))
            {
                req.Headers.Set("Cookie", cookies);
            }
        });
        var request = new RestRequest();
        var response = client.Execute(request);
        return response;
    }
}