/// <summary> 
/// jpeg图片压缩 
/// </summary> 
/// <param name="sFile"></param> 
/// <param name="outPath"></param> 
/// <param name="flag"></param> 
/// <returns></returns> 
private static bool GetPicThumbnail(string sFile, string outPath, int flag)
{

    var iSource = Image.FromFile(sFile);
    var tFormat = iSource.RawFormat;
    var ep = new EncoderParameters();
    var qy = new long[1];
    qy[0] = flag;
    var eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
    ep.Param[0] = eParam;
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
        return true;
    }
    catch (Exception ex)
    {
        LogHelper.Error("jpeg图片压缩", ex);
        return false;
    }
    finally
    {
        iSource.Dispose();
        iSource.Dispose();
    }
}





/// <summary>
/// 缩放图片大小
/// </summary>
/// <param name="oldPath"></param>
/// <param name="newPath"></param>
/// <param name="maxWidth"></param>
/// <param name="maxSize"></param>
private static string Resize(string oldPath, string newPath, int maxWidth, int maxSize)
{
    var fac = new ImageProcessor.ImageFactory();
    var img = fac.Load(oldPath);
    if (img.Image.Width > maxWidth || img.Image.Height > maxWidth)
    {
        var w = 0;
        var h = 0;
        if (img.Image.Width > img.Image.Height)
        {
            w = maxWidth;
            h = img.Image.Height * maxWidth / img.Image.Width;
        }
        else
        {
            h = maxWidth;
            w = img.Image.Width * maxWidth / img.Image.Height;
        }

        var fac2 = img.Resize(new Size()
        {
            Width = w,
            Height = h
        });
        fac2.Save(newPath);

        fac2.Dispose();
        img.Dispose();
        fac.Dispose();

        var fileInfo = new FileInfo(newPath);
        if (fileInfo.Length <= maxSize) return newPath;

        var newPath2 = FileUtil.GetTempFilePath(Root);
        System.IO.File.Move(newPath, newPath2);
        if (File.Exists(newPath2))
        {
            File.Delete(newPath);
        }
        var mw = (int)(maxWidth * 0.8);
        return Resize(newPath2, newPath, mw, maxSize);
    }

    System.IO.File.Copy(oldPath, newPath);
    return newPath;
}

/// <summary>
/// 缩放图片大小
/// </summary>
/// <param name="stream"></param>
/// <param name="maxWidth"></param>
/// <param name="maxSize"></param>
private static Stream Resize(Stream stream, int maxWidth, int maxSize)
{
    var fac = new ImageProcessor.ImageFactory();
    var img = fac.Load(stream);
    if (img.Image.Width <= maxWidth && img.Image.Height <= maxWidth && stream.Length <= maxSize) return stream;

    var w = 0;
    var h = 0;
    if (img.Image.Width > img.Image.Height)
    {
        w = maxWidth;
        h = img.Image.Height * maxWidth / img.Image.Width;
    }
    else
    {
        h = maxWidth;
        w = img.Image.Width * maxWidth / img.Image.Height;
    }

    var fac2 = img.Resize(new Size()
    {
        Width = w,
        Height = h
    });

    var ms = new MemoryStream();
    fac2.Save(ms);

    fac2.Dispose();
    img.Dispose();
    fac.Dispose();

    if (ms.Length > maxSize)
    {
        var mw = (int)(maxWidth * 0.8);
        return Resize(ms, mw, maxSize);
    }
    return ms;
}
