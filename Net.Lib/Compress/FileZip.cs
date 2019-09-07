//系统自带解压缩功能，引用：System.IO.Compression和System.IO.Compression.FileSystem


using System;
using System.IO;
using System.IO.Compression;

/// <summary>
/// 解压缩
/// </summary>
/// <param name="zipPath">压缩文件</param>
/// <param name="extractPath">解压目录</param>
private static void Decompression(string zipPath, string extractPath)
{
    using (var archive = ZipFile.OpenRead(zipPath))
    {
        foreach (var entry in archive.Entries)
        {
            var destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));
            entry.ExtractToFile(destinationPath, true);
        }
    }
}