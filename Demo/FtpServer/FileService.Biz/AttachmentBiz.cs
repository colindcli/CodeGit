using FileService.DataAccess;
using FileService.Entity;
using System;
using System.Collections.Generic;
using FileService.Common;

namespace FileService.Biz
{
    public class AttachmentBiz
    {
        private static readonly AttachmentDa Da = new AttachmentDa();
        public AttachmentModel GetAttachment(Guid attachmentId)
        {
            return Da.GetAttachment(attachmentId);
        }

        public void DeleteAttachment()
        {
            var lists = Da.GetDeleteAttachments();
            var deleteIds = new List<Guid>();
            foreach (var list in lists)
            {
                var paths = new List<string>
                {
                    $"{Config.FileRoot}/Files/{list.Folder}/SmallThumbnails/{list.AttachmentId}",
                    $"{Config.FileRoot}/Files/{list.Folder}/BigThumbnails/{list.AttachmentId}",
                    $"{Config.FileRoot}/Files/{list.Folder}/Original/{list.AttachmentId}"
                };
                if (FileUtil.DeleteFile(paths))
                {
                    deleteIds.Add(list.AttachmentId);
                }
            }
            Da.DeleteAttachments(deleteIds);
        }
    }
}
