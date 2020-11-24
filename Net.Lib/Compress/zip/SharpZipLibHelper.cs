using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

public class SharpZipLibHelper
{
    /// <summary>
    ///　压缩文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="zipPath"></param>
    /// <param name="fileName">压缩包里的文件名</param>
    public static void CompressFile(string path, string zipPath, string fileName)
    {
        using (var zip = ZipFile.Create(zipPath))
        {
            zip.BeginUpdate();
            zip.Add(path, fileName);
            zip.CommitUpdate();
        }
    }

    #region 压缩文件夹
    /// <summary>
    ///　压缩文件夹
    /// </summary>
    /// <param name="dir">待压缩的文件夹</param>
    /// <param name="targetFileName">压缩后文件路径（包括文件名）</param>
    /// <param name="recursive">是否递归压缩</param>
    /// <returns></returns>
    public static bool Compress(string dir, string targetFileName, bool recursive)
    {
        if (string.IsNullOrWhiteSpace(dir) || !Directory.Exists(dir)) return false;

        if (recursive == false)
            return ZipFileDictory(dir, targetFileName);

        var zipFile = File.Create(targetFileName);
        var zipStream = new ZipOutputStream(zipFile);
        _CompressFolder(dir, zipStream, dir.Substring(3));
        zipStream.Finish();
        zipStream.Close();

        return File.Exists(targetFileName);
    }

    /// <summary>
    /// 压缩目录
    /// </summary>
    private static bool ZipFileDictory(string dir, string targetFileName)
    {
        ZipOutputStream s = null;
        var b = true;
        try
        {
            var filenames = Directory.GetFiles(dir);
            var crc = new Crc32();
            s = new ZipOutputStream(File.Create(targetFileName));
            s.SetLevel(6);

            foreach (var file in filenames)
            {
                var fs = File.OpenRead(file);
                var buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                var entry = new ZipEntry(file);
                entry.DateTime = DateTime.Now;
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                s.PutNextEntry(entry);
                s.Write(buffer, 0, buffer.Length);
            }
        }
        catch
        {
            b = false;
        }
        finally
        {
            if (s != null)
            {
                s.Finish();
                s.Close();
            }
        }
        return b;
    }

    /// <summary>
    /// 压缩某个子文件夹
    /// </summary>
    /// <param name="basePath"></param>
    /// <param name="zips"></param>
    /// <param name="zipfolername"></param>     
    private static void _CompressFolder(string basePath, ZipOutputStream zips, string zipfolername)
    {
        if (File.Exists(basePath))
        {
            _CreateZipFile(basePath, zips, zipfolername);
            return;
        }
        var names = Directory.GetFiles(basePath);
        foreach (var fileName in names)
        {
            _CreateZipFile(fileName, zips, zipfolername);
        }

        names = Directory.GetDirectories(basePath);
        foreach (var folderName in names)
        {
            _CompressFolder(folderName, zips, zipfolername);
        }

    }

    /// <summary>
    /// 压缩单独文件
    /// </summary>
    /// <param name="fileToZip"></param>
    /// <param name="zips"></param>
    /// <param name="zipfolername"></param>
    private static void _CreateZipFile(string fileToZip, ZipOutputStream zips, string zipfolername)
    {
        if (!File.Exists(fileToZip)) return;

        var streamToZip = new FileStream(fileToZip, FileMode.Open, FileAccess.Read);
        var temp = fileToZip;
        var temp1 = zipfolername;
        if (temp1.Length > 0)
        {
            var i = temp1.LastIndexOf("//", StringComparison.Ordinal) + 1;
            var j = temp.Length - i;
            temp = temp.Substring(i, j);
        }
        var zipEn = new ZipEntry(temp.Substring(3));

        zips.PutNextEntry(zipEn);
        var buffer = new byte[16384];
        var size = streamToZip.Read(buffer, 0, buffer.Length);
        zips.Write(buffer, 0, size);
        try
        {
            while (size < streamToZip.Length)
            {
                var sizeRead = streamToZip.Read(buffer, 0, buffer.Length);
                zips.Write(buffer, 0, sizeRead);
                size += sizeRead;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        streamToZip.Close();
        streamToZip.Dispose();
    }
    #endregion

    #region 解压缩
    /// <summary>
    /// 解压缩目录
    /// </summary>
    /// <param name="zipDirectoryPath">压缩包文件路径</param>
    /// <param name="unZipDirecotyPath">解压缩目录路径</param>
    /// <param name="password"></param>
    public static void UnZipDirectory(string zipDirectoryPath, string unZipDirecotyPath, string password = null)
    {
        unZipDirecotyPath = unZipDirecotyPath.Trim('/', '\\');

        using (var zipStream = new ZipInputStream(File.OpenRead(zipDirectoryPath)))
        {
            //判断Password
            if (!string.IsNullOrWhiteSpace(password))
            {
                zipStream.Password = password;
            }

            ZipEntry zipEntry;
            while ((zipEntry = zipStream.GetNextEntry()) != null)
            {
                var directoryName = Path.GetDirectoryName(zipEntry.Name);
                var fileName = Path.GetFileName(zipEntry.Name);

                if (!string.IsNullOrWhiteSpace(directoryName))
                {
                    Directory.CreateDirectory(unZipDirecotyPath + @"\" + directoryName);
                }

                if (string.IsNullOrWhiteSpace(fileName)) continue;
                if (zipEntry.CompressedSize == 0)
                    break;
                if (zipEntry.IsDirectory)
                {
                    directoryName = Path.GetDirectoryName(unZipDirecotyPath + @"\" + zipEntry.Name);
                    if (directoryName != null && !Directory.Exists(directoryName))
                        Directory.CreateDirectory(directoryName);
                }

                using (var stream = File.Create(unZipDirecotyPath + @"\" + zipEntry.Name))
                {
                    var buffer = new byte[2048];
                    while (true)
                    {
                        var size = zipStream.Read(buffer, 0, buffer.Length);
                        if (size > 0)
                        {
                            stream.Write(buffer, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
    #endregion
}
