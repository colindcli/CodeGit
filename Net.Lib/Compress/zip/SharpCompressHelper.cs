using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Readers;
using System.IO;

/// <summary>
/// 压缩解压缩
/// Install-Package sharpcompress
/// </summary>
public class SharpCompressHelper
{
    /// <summary>
    /// 压缩
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool Compress(string directory, string fileName)
    {
        using (var archive = ZipArchive.Create())
        {
            archive.AddAllFromDirectory(directory);
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                archive.SaveTo(fs);
            }
        }

        return File.Exists(fileName);
    }

    /// <summary>
    /// 解压缩
    /// </summary>
    /// <param name="directory"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool UnCompress(string fileName, string directory)
    {
        using (Stream stream = File.OpenRead(fileName))
        {
            var reader = ReaderFactory.Open(stream);
            while (reader.MoveToNextEntry())
            {
                if (!reader.Entry.IsDirectory)
                {
                    reader.WriteEntryToDirectory(directory, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                }
            }
        }

        return true;
    }
}
