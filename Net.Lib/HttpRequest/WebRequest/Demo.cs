using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

public class WebReq
{
    public static string GetRequest(string url, Encoding encoding, string userAgent, string cookies = null)
    {
        string content;
        if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
        }
        var webRequest = WebRequest.Create(url) as HttpWebRequest;
        HttpWebResponse webResponse = null;
        try
        {
            if (!string.IsNullOrWhiteSpace(cookies))
            {
                webRequest.Headers.Set("Cookie", cookies);
            }
            webRequest.Timeout = 30000;
            webRequest.Method = "GET";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            webRequest.Headers.Add("Accept-Language", "zh-CN");
            if (!string.IsNullOrEmpty(userAgent))
                webRequest.UserAgent = userAgent;
            webRequest.Accept = "text/html, text/css";

            webResponse = (HttpWebResponse)webRequest.GetResponse();
            var streamReceive = webResponse.GetResponseStream();
            if (streamReceive == null)
            {
                content = "";
            }
            else switch (webResponse.ContentEncoding.ToLower())
                {
                    case "gzip":
                        using (var zipStream = new GZipStream(streamReceive, CompressionMode.Decompress))
                        {
                            using (var sr = new StreamReader(zipStream, encoding))
                            {
                                content = sr.ReadToEnd();
                            }
                        }
                        break;
                    case "deflate":
                        using (var deflateStream = new DeflateStream(streamReceive, CompressionMode.Decompress))
                        {
                            using (var sr = new StreamReader(deflateStream, encoding))
                            {
                                content = sr.ReadToEnd();
                            }
                        }
                        break;
                    default:
                        using (var sr = new StreamReader(streamReceive, encoding))
                        {
                            content = sr.ReadToEnd();
                        }
                        break;
                }
        }
        catch (Exception e)
        {
            content = e.Message;
        }
        webResponse?.Close();
        webRequest?.Abort();
        return content;
    }
}