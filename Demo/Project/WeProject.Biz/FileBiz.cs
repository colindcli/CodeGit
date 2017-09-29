using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WeProject.Common;
using WeProject.Common.Files;
using WeProject.DataAccess;
using WeProject.Entity;
using WeProject.Entity.Model;
using WeProject.Service;

namespace WeProject.Biz
{
    public class FileBiz
    {
        private static readonly FileDa Da = new FileDa();

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="staffModel"></param>
        /// <param name="path"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public Guid? Upload(StaffModel staffModel, string path, string title)
        {
            var attachmentId = Guid.NewGuid();
            var lists = new List<FtpFileModel>
            {
                new FtpFileModel()
                {
                    LocalPath = path,
                    RemotePath = attachmentId.GetPathOriginal()
                }
            };
            var isPic = ImageUtil.IsPic(title);
            var smallThumbnails = path + "_small";
            var bigThumbnails = path + "_big";
            var sizeOriginal = FileUtil.GetFileSize(path);
            long sizeSmall = 0;
            long sizeBig = 0;
            var fileExt = Path.GetExtension(title)?.TrimStart('.').ToLower();
            if (isPic)
            {
                var thumbnail = Config.Thumbnails;
                if (ImageUtil.CreateThumbnail(path, smallThumbnails, thumbnail.SmallWidth, thumbnail.SmallHeight))
                {
                    lists.Add(new FtpFileModel()
                    {
                        LocalPath = smallThumbnails,
                        RemotePath = attachmentId.GetPathSmallThumbnails()
                    });
                    sizeSmall = FileUtil.GetFileSize(smallThumbnails);
                }
                if (ImageUtil.CreateThumbnail(path, bigThumbnails, thumbnail.BigWidth, thumbnail.BigHeight))
                {
                    lists.Add(new FtpFileModel()
                    {
                        LocalPath = bigThumbnails,
                        RemotePath = attachmentId.GetPathBigThumbnails()
                    });
                    sizeBig = FileUtil.GetFileSize(bigThumbnails);
                }

            }
            var n = FtpHelper.UploadFiles(lists);
            FileUtil.DeleteFile(lists.Select(p => p.LocalPath).ToList());
            if (n != lists.Count) return null;
            var model = new Attachment()
            {
                AttachmentId = attachmentId,
                Folder = $"{DateTime.Today:yyyyMM}",
                Title = title,
                SizeOriginal = sizeOriginal,
                SizeSmall = sizeSmall,
                SizeBig = sizeBig,
                FileExt = fileExt,
                Downloads = 0,
                CreateUserId = staffModel.UserId,
                CreateDate = DateTime.Now,
                CompanyId = staffModel.CompanyId,
                Status = 0
            };
            Da.Upload(model);
            return attachmentId;
        }


    }
}
