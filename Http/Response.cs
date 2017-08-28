using System.Net.Http;
using System.Net.Http.Headers;

public class Response
{
    /// <summary>
    /// API Response
    /// </summary>
    /// <param name="bt"></param>
    /// <param name="fileName"></param>
    public static HttpResponseMessage ResponseFile(byte[] bt, string fileName)
    {
        //var userAgent = System.Web.Http.Request.Headers.UserAgent.ToString();
        //if (userAgent.IndexOf("AppleWebKit", StringComparison.OrdinalIgnoreCase) > -1)
        //{
        //    fileName = System.Web.HttpUtility.UrlEncode(fileName);
        //}
        //else if (userAgent.IndexOf("MSIE", StringComparison.OrdinalIgnoreCase) > -1)
        //{
        //    fileName = System.Web.HttpUtility.UrlEncode(fileName);
        //}

        var response = new HttpResponseMessage { Content = new ByteArrayContent(bt) };
        response.Content.Headers.ContentLength = bt.Length;
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = fileName
        };
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        return response;
    }
}
