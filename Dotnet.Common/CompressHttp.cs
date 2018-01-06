/// <summary>
/// 对js/css文件实现gzip/deflate压缩传输
/// <para>===========================</para>
/// <para>执行以下两步骤即可实现压缩css、js文件功能</para>
/// <para>1、在Web.config的节点httpHandlers添加以下代码：</para>
/// <para>＜add verb="*" path="*.css,*.js" type="DcUtility.DcHttpCompress, DcUtility" /＞</para>
/// <para>备注：如果使用Nuget安装，会自动添加了以上代码到Web.config</para>
/// <para>2、IIS设置*.css,*.js扩展名做Net映射。</para>
/// </summary>
public class CompressHttp : IHttpHandler
{
    private const string DEFLATE = "deflate";

    private const string GZIP = "gzip";

    /// <summary>
    ///
    /// </summary>
    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }

    /// <summary>
    /// 处理IHttpHandler请求
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        string path = context.Request.Path;
        string contentType = GetFileContentType(path);
        string s = File.ReadAllText(context.Server.MapPath(path), Encoding.UTF8);

        if (contentType != "" && IsEncodingAccepted(GZIP))
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s));
            context.Response.BinaryWrite(Compress(ms, GZIP));
            ms.Dispose();
            ms.Close();

            context.Response.ContentType = contentType;
            SetEncoding(GZIP);
        }
        else if (contentType != "" && IsEncodingAccepted(DEFLATE))
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s));
            context.Response.BinaryWrite(Compress(ms, DEFLATE));
            ms.Dispose();
            ms.Close();

            context.Response.ContentType = contentType;
            SetEncoding(DEFLATE);
        }
        else
        {
            context.Response.Write(s);
        }
        DcUtility.DcDataCache.ClientPoint.SetCache(DateTime.Now.AddDays(1));
        context.Response.End();
    }

    private static string GetFileContentType(string path)
    {
        string ext = Path.GetExtension(path).Trim().ToLower();
        string contentType = "";
        switch (ext)
        {
            case ".css":
                {
                    contentType = "text/css";
                    break;
                }
            case ".js":
                {
                    contentType = "application/x-javascript";
                    break;
                }
        }
        return contentType;
    }

    private static bool IsEncodingAccepted(string encoding)
    {
        HttpContext context = HttpContext.Current;
        return context.Request.Headers["Accept-encoding"] != null && context.Request.Headers["Accept-encoding"].Contains(encoding);
    }

    private static void SetEncoding(string encoding)
    {
        HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
    }

    private byte[] Compress(MemoryStream stream, string CompressType)
    {
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, (int)stream.Length);
        MemoryStream ms = new MemoryStream();
        switch (CompressType)
        {
            case GZIP:
                {
                    GZipStream gZipStream = new GZipStream(ms, CompressionMode.Compress);
                    gZipStream.Write(buffer, 0, buffer.Length);
                    gZipStream.Close();
                    break;
                }
            case DEFLATE:
                {
                    DeflateStream deflateStream = new DeflateStream(ms, CompressionMode.Compress);
                    deflateStream.Write(buffer, 0, buffer.Length);
                    deflateStream.Close();
                    break;
                }
        }
        ms.Flush();
        byte[] data = ms.ToArray();
        ms.Close();
        return data;
    }
}
