using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;

/// <summary>
/// 输出内容
/// </summary>
public class ResponseHelper
{
    /// <summary>
    /// Api输出图片
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns></returns>
    public static HttpResponseMessage ResponseImageForApi(string path)
    {
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StreamContent(File.OpenRead(path))
        };
        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
        return httpResponseMessage;
    }

    /// <summary>
    /// Api下载文件
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    public static HttpResponseMessage ResponseFileForApi(string path, string fileName)
    {
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StreamContent(File.OpenRead(path))
        };
        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = fileName,
            FileNameStar = fileName
        };
        return httpResponseMessage;
    }

    /// <summary>
    /// Mvc输出图片
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns></returns>
    public static FilePathResult ResponseImageForMvc(string path)
    {
        return new FilePathResult(path, "image/jpg");
    }

    /// <summary>
    /// Mvc下载文件
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="fileName">文件名称</param>
    /// <returns></returns>
    public static FilePathResult ResponseFileForMvc(string path, string fileName)
    {
        return new FilePathResult(path, "application/octet-stream")
        {
            FileDownloadName = fileName
        };
    }

    /// <summary>
    /// Mvc输出Html
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <returns></returns>
    public static ContentResult ResponseHtmlForMvc(string content, Encoding contentEncoding = null)
    {
        return new ContentResult()
        {
            Content = content,
            ContentType = "text/html",
            ContentEncoding = contentEncoding ?? Encoding.UTF8
        };
    }

    /// <summary>
    /// Mvc输出Css
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <returns></returns>
    public static ContentResult ResponseCssForMvc(string content, Encoding contentEncoding = null)
    {
        return new ContentResult()
        {
            Content = content,
            ContentType = "text/css",
            ContentEncoding = contentEncoding ?? Encoding.UTF8
        };
    }

    /// <summary>
    /// Mvc输出Js
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="contentEncoding">内容编码</param>
    /// <returns></returns>
    public static ContentResult ResponseJsForMvc(string content, Encoding contentEncoding = null)
    {
        return new ContentResult()
        {
            Content = content,
            ContentType = "application/x-javascript",
            ContentEncoding = contentEncoding ?? Encoding.UTF8
        };
    }
}