using System.Net.Http;
using System.Net.Http.Headers;

//输出文件、输出图片
public class Response
{
    /// <summary>
    /// API Response 下载文件、下载图片
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
            FileNameStar = fileName //IE、firefox文件名乱码问题
        };
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        return response;
    }
    
    /// <summary>
    /// 输出附件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    private static HttpResponseMessage ResponseAttachment(string fileName, string title)
    {
        var fs = new FileStream(fileName, FileMode.Open);
        var len = (int)fs.Length;
        var bt = new byte[len];
        fs.Read(bt, 0, len);
        fs.Close();
        fs.Dispose();

        var response = new HttpResponseMessage { Content = new ByteArrayContent(bt) };
        response.Content.Headers.ContentLength = bt.Length;
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = title,
            FileNameStar = title
        };
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        response.Headers.CacheControl = new CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = new TimeSpan(30, 0, 0, 0)
        };
        return response;
    }
}
