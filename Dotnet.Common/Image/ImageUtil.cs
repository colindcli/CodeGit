using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
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

    /// <summary>
    /// 根据文字生成图片
    /// </summary>
    /// <param name="text">文字内容</param>
    /// <param name="fontName">字体名称</param>
    /// <param name="fontSize">字体大小</param>
    /// <param name="fontColor">字体颜色,如Brushes.Black</param>
    /// <param name="bgColor">背景颜色,如Brushes.Transparent(透明色)</param>
    /// <param name="savePath">保存路径</param>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public static string CreateTextPic(string text, string fontName, float fontSize, Color fontColor, Color bgColor, string savePath,string fileName)
    {
        var bgBrush = new SolidBrush(bgColor);
        var fontBrush = Brushes.White;
        try
        {
            Graphics g;
            var format = new StringFormat(StringFormatFlags.NoClip)
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            if (string.IsNullOrEmpty(fontName))
            {
                fontName = "宋体";
            }
            var font = new Font(fontName, fontSize);

            var bmp = new Bitmap(1, 1);
            g = Graphics.FromImage(bmp);
            var sizef = g.MeasureString(text, font, PointF.Empty, format);

            var width = Convert.ToInt32(sizef.Width) + 20;
            var rect = new RectangleF(0, 0, width, width);
            bmp.Dispose();
            bmp = new Bitmap(width, width);

            g = Graphics.FromImage(bmp);
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.FillRectangle(bgBrush, rect);
            g.DrawString(text, font, fontBrush, rect, format);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var filePath = savePath + fileName + Config.ExtName;

            bmp.Save(filePath, ImageFormat.Png);

            return filePath;
        }
        catch (Exception ex)
        {
            LogHelper.Error("在ImageUtil.CreateTextPic出错", ex);
            return "";
        }
    }
}
