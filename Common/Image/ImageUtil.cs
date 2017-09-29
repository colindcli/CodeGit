using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

public class ImageUtil
{
    /// <summary>
    /// 是否图片
    /// </summary>
    /// <param name="fileTitle"></param>
    /// <returns></returns>
    public static bool IsPic(string fileTitle)
    {
        var exts = ".jpg;.png;.gif;.jpeg;.bmp";
        var fileType = Path.GetExtension(fileTitle);
        return !string.IsNullOrWhiteSpace(fileType) && exts.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Contains(fileType, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="oldPath"></param>
    /// <param name="newPath"></param>
    /// <param name="intWidth"></param>
    /// <param name="intHeight"></param>
    /// <param name="qty">质量</param>
    /// <returns></returns>
    public static bool CreateThumbnail(string oldPath, string newPath, int intWidth, int intHeight, int qty = 60)
    {
        if (File.Exists(oldPath))
        {
            var thFileinfo = new FileInfo(oldPath);
            if (thFileinfo.Length <= 200 * 1024)
            {
                var newFile = thFileinfo.CopyTo(newPath, true);
                return newFile.Exists;
            }
        }

        var path = AppDomain.CurrentDomain.BaseDirectory + "Files";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += "\\Temp_" + Guid.NewGuid();

        var isExists = GenerateThumbnail(oldPath, path, intWidth, intHeight);
        var b = CompressionImage(isExists ? path : oldPath, newPath, qty);
        if (isExists)
        {
            File.Delete(path);
        }
        else
        {
            LogHelper.Info($"文件不存在:{path}");
        }
        return b;
    }

    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="oldPath"></param>
    /// <param name="newPath"></param>
    /// <param name="intWidth"></param>
    /// <param name="intHeight"></param>
    /// <returns></returns>
    private static bool GenerateThumbnail(string oldPath, string newPath, int intWidth, int intHeight)
    {
        var b = false;
        Bitmap objPic = null;
        Bitmap objNewPic = null;
        try
        {
            objPic = new Bitmap(oldPath);
            int width;
            int height;
            if ((objPic.Width * 1.0000) / objPic.Height > intWidth * 1.0000 / intHeight)
            {
                width = intWidth;
                height = intWidth * objPic.Height / objPic.Width;
            }
            else
            {
                height = intHeight;
                width = intHeight * objPic.Width / objPic.Height;
            }

            objNewPic = new Bitmap(objPic, width, height);
            objNewPic.Save(newPath);
            objPic.Dispose();
            objNewPic.Dispose();

            if (File.Exists(newPath))
            {
                b = true;
            }
            else
            {
                LogHelper.Info("生成缩略图不存在");
            }
        }
        catch (Exception ex)
        {
            objPic?.Dispose();
            objNewPic?.Dispose();

            LogHelper.Error("生成缩略图", ex);
        }
        return b;
    }

    /// <summary> 
    /// jpeg图片压缩 
    /// </summary> 
    /// <param name="sFile"></param> 
    /// <param name="outPath"></param> 
    /// <param name="flag"></param> 
    /// <returns></returns> 
    private static bool CompressionImage(string sFile, string outPath, int flag)
    {

        var iSource = Image.FromFile(sFile);
        var tFormat = iSource.RawFormat;
        var ep = new EncoderParameters();
        var qy = new long[1];
        qy[0] = flag;
        var eParam = new EncoderParameter(Encoder.Quality, qy);
        ep.Param[0] = eParam;
        var b = false;
        try
        {
            var arrayIci = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegIcIinfo = null;
            for (var x = 0; x < arrayIci.Length; x++)
            {
                if (!arrayIci[x].FormatDescription.Equals("JPEG")) continue;

                jpegIcIinfo = arrayIci[x];
                break;
            }
            if (jpegIcIinfo != null)
            {
                iSource.Save(outPath, jpegIcIinfo, ep);
            }
            else
            {
                iSource.Save(outPath, tFormat);
            }
            b = true;
        }
        catch (Exception ex)
        {
            LogHelper.Error("jpeg图片压缩", ex);
            b = false;
        }
        finally
        {
            iSource.Dispose();
            iSource.Dispose();
        }
        return b && File.Exists(outPath);
    }

}
