using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WeProject.Biz;

namespace WeProject.WebApiPc.Controllers
{
    public class FileController : BaseController
    {
        private static readonly FileBiz Biz = new FileBiz();

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid?> Upload()
        {
            var uploadFilesPath = Root + "/Files";
            if (!Directory.Exists(uploadFilesPath))
                Directory.CreateDirectory(uploadFilesPath);
            var provider = new MultipartFileStreamProvider(uploadFilesPath);
            await Request.Content.ReadAsMultipartAsync(provider);

            if (provider.FileData.Count == 0)
                return null;

            var file = provider.FileData[0];
            var path = file.LocalFileName;
            var title = file.Headers.ContentDisposition.FileName.Trim('"');

            return Biz.Upload(StaffModel, path, title);
        }
    }
}
