using System;

namespace FileService.Entity
{
    public class AttachmentModel
    {
        public virtual Guid AttachmentId { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public virtual long CompanyId { get; set; }
        /// <summary>
        /// 文件名：201709
        /// </summary>
        public virtual string Folder { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 源文件大小
        /// </summary>
        public virtual long SizeOriginal { get; set; }
        /// <summary>
        /// 小缩略图大小
        /// </summary>
        public virtual long SizeSmall { get; set; }
        /// <summary>
        /// 大缩略图大小
        /// </summary>
        public virtual long SizeBig { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public virtual string FileExt { get; set; }
        /// <summary>
        /// 下载量
        /// </summary>
        public virtual int Downloads { get; set; }
        /// <summary>
        /// 状态：0上传文件（超24小时就删除）；1可用的文件；2标志为不可用文件（保留系统文件，不会删除）
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual long CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
    }
}
