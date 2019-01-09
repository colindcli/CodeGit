//https://www.nuget.org/packages/Mime-Detective
using MimeDetective;
using MimeDetective.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FileTypeHelper
{
    /// <summary>
    /// 接受文件类型
    /// </summary>
    /// <param name="path"></param>
    /// <param name="acceptFileType"></param>
    /// <returns></returns>
    public static bool CheckFileType(string path, List<string> acceptFileType)
    {
        FileType fileType = null;
        lock (path)
        {
            var fileDataStream = File.Open(path, FileMode.Open);
            fileType = fileDataStream.GetFileType();
            fileDataStream.Close();
            fileDataStream.Dispose();
        }

        if (fileType != null)
        {
            return acceptFileType.Contains(fileType.Extension, StringComparer.OrdinalIgnoreCase);
        }

        var extension = (Path.GetExtension(path) ?? "").Trim(' ', '.');
        return acceptFileType.Contains(extension, StringComparer.OrdinalIgnoreCase);
    }
}