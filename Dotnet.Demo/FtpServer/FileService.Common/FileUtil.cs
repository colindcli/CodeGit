using System;
using System.Collections.Generic;
using System.IO;

namespace FileService.Common
{
    public class FileUtil
    {
        public static bool DeleteFile(List<string> files)
        {
            var b = true;
            foreach (var file in files)
            {
                if (!File.Exists(file)) continue;
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    LogHelper.Fatal($"文件删除失败：{file}", ex);
                    b = false;
                }
            }
            return b;
        }
    }
}
