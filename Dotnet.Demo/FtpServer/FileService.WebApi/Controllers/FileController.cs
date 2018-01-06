using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using FileService.Biz;
using System.Web.Http;
using FileService.Common;

namespace FileService.WebApi.Controllers
{
    public class FileController : ApiController
    {
        private static readonly AttachmentBiz Biz = new AttachmentBiz();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">0原图；1小缩略图；2大缩略图</param>
        /// <returns></returns>
        public HttpResponseMessage GetAttachment(Guid id, int type = 0)
        {
            var m = Biz.GetAttachment(id);

            if (m == null)
            {
                LogHelper.Warn($"AttachmentId不存在：{id}");
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("文件Id不存在")
                };
            };

            if (m.Status == 0 && m.CreateDate <= DateTime.Now.AddDays(-1))
            {
                LogHelper.Warn($"上传文件超过24小时未设置状态，资源已失效：{id}");
                return new HttpResponseMessage(HttpStatusCode.Gone)
                {
                    Content = new StringContent("上传文件超过24小时未设置状态，资源已失效")
                };
            }

            if (m.Status == 3)
            {
                LogHelper.Warn($"资源已不可用：{id}");
                return new HttpResponseMessage(HttpStatusCode.Gone)
                {
                    Content = new StringContent("资源已不可用")
                };
            }

            if (m.Status < 0 || m.Status > 3)
            {
                LogHelper.Warn($"状态不设置错误：{id}");
                return new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType)
                {
                    Content = new StringContent("状态不设置错误")
                };
            }

            string folder;
            switch (type)
            {
                case 1:
                    {
                        folder = "SmallThumbnails";
                        break;
                    }
                case 2:
                    {
                        folder = "BigThumbnails";
                        break;
                    }
                default:
                    {
                        folder = "Original";
                        break;
                    }
            }
            var path = Config.FileRoot + $"/Files/{m.Folder}/{folder}/{m.AttachmentId}";
            if (!File.Exists(path))
            {
                LogHelper.Warn($"文件不存在：{path}");
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("文件不存在")
                };
            }

            return ResponseAttachment(path, m.Title);
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
}
