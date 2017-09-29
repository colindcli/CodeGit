using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeProject.Common
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 生成FTP服务器小缩略图路径
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public static string GetPathSmallThumbnails(this Guid attachmentId)
        {
            return $"/Files/{DateTime.Today:yyyyMM}/SmallThumbnails/{attachmentId}";
        }
        /// <summary>
        /// 生成FTP服务器大缩略图路径
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public static string GetPathBigThumbnails(this Guid attachmentId)
        {
            return $"/Files/{DateTime.Today:yyyyMM}/BigThumbnails/{attachmentId}";
        }
        /// <summary>
        /// 生成FTP服务器源文件路径
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public static string GetPathOriginal(this Guid attachmentId)
        {
            return $"/Files/{DateTime.Today:yyyyMM}/Original/{attachmentId}";
        }

    }
}
