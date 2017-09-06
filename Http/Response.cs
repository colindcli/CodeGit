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
        var response = new HttpResponseMessage { Content = new ByteArrayContent(bt) };
        response.Content.Headers.ContentLength = bt.Length;
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = fileName,
            FileNameStar = fileName //文件名乱码问题
        };
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        return response;
    }
}
