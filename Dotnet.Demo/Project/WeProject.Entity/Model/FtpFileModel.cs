using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeProject.Entity.Model
{
    public class FtpFileModel
    {
        /// <summary>
        /// 本地文件
        /// </summary>
        public string LocalPath { get; set; }

        public string RemotePath { get; set; }
    }
}
