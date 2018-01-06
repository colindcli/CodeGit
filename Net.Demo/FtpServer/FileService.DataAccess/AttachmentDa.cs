using Dapper;
using FileService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileService.DataAccess
{
    public class AttachmentDa : RepositoryBase
    {
        public AttachmentModel GetAttachment(Guid attachmentId)
        {
            const string sqlStr = @"SELECT * FROM dbo.Attachment a WHERE a.AttachmentId=@AttachmentId;";
            return Db(p => p.QueryFirstOrDefault<AttachmentModel>(sqlStr, new { AttachmentId = attachmentId }));
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public List<AttachmentModel> GetDeleteAttachments()
        {
            const string sqlStr = @"SELECT * FROM dbo.Attachment a WHERE a.Status=0 AND a.CreateDate<DATEADD(DAY,-1,GETDATE());";
            return Db(p => p.Query<AttachmentModel>(sqlStr).ToList());
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="attachmentIds"></param>
        public void DeleteAttachments(List<Guid> attachmentIds)
        {
            const string sqlStr = @"DELETE FROM dbo.Attachment WHERE AttachmentId IN @AttachmentIds;";
            Db(p => p.Execute(sqlStr, new { AttachmentIds = attachmentIds }));
        }
    }
}
