using System.IO.Compression;
using System.Web.Mvc;

/// <summary>
/// MVC压缩
/// </summary>
public class CompressionFilter : ActionFilterAttribute
{
    private const string Gzip = "gzip";
    private const string Deflate = "deflate";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterContext"></param>
    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
        if (filterContext.Exception != null)
            return;

        var response = filterContext.HttpContext.Response;
        if (response.Filter is GZipStream || response.Filter is DeflateStream)
            return;

        var acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
        if (string.IsNullOrWhiteSpace(acceptEncoding))
            return;

        if (acceptEncoding.Contains(Gzip))
        {
            response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            response.Headers.Remove("Content-Encoding");
            response.AppendHeader("Content-Encoding", Gzip);
        }
        else if (acceptEncoding.Contains(Deflate))
        {
            response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            response.Headers.Remove("Content-Encoding");
            response.AppendHeader("Content-Encoding", Deflate);
        }
    }
}
