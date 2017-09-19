using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

public class HttpClientHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="payload"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static string Post(string url, string payload = null, Dictionary<string, string> headers = null)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (headers != null && headers.Count > 0)
        {
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var httpContent = new StringContent(payload);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = client.PostAsync(url, httpContent);
        var content = response.Result.Content.ReadAsStringAsync();

        return content.Result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="formData"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static string Post(string url, Dictionary<string, string> formData = null, Dictionary<string, string> headers = null)
    {
        return Post(url, formData, null, headers);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="files">[FileName, stream]</param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static string Post(string url, Dictionary<string, byte[]> files = null, Dictionary<string, string> headers = null)
    {
        return Post(url, null, files, headers);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="formData"></param>
    /// <param name="files">[FileName, stream]</param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static string Post(string url, Dictionary<string, string> formData = null, Dictionary<string, byte[]> files = null, Dictionary<string, string> headers = null)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
        if (headers != null && headers.Count > 0)
        {
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var httpContent = new MultipartFormDataContent();
        if (formData != null && formData.Count > 0)
        {
            foreach (var form in formData)
            {
                httpContent.Add(new StringContent(form.Value) { Headers = { ContentType = new MediaTypeHeaderValue("text/html") } }, form.Key);
            }
        }

        if (files != null && files.Count > 0)
        {
            foreach (var file in files)
            {
                httpContent.Add(new ByteArrayContent(file.Value), file.Key, file.Key);
            }
        }

        var response = client.PostAsync(url, httpContent);
        var content = response.Result.Content.ReadAsStringAsync();

        return content.Result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static byte[] GetFileByte(string filename)
    {
        var fs = new FileStream(filename, FileMode.Open);
        var bt = new byte[fs.Length];
        fs.Read(bt, 0, (int)fs.Length);
        fs.Close();
        fs.Dispose();
        return bt;
    }
}
