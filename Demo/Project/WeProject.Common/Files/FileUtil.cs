using System;
using System.Collections.Generic;
using System.IO;

namespace WeProject.Common.Files
{
    public class FileUtil
    {
        public static void DeleteFile(List<string> files)
        {
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
                }
            }
        }

        public static long GetFileSize(string path)
        {
            try
            {
                return new FileInfo(path).Length;
            }
            catch (Exception ex)
            {
                LogHelper.Fatal("获取文件大小", ex);
                return 0;
            }
        }
    }
}
