//threeebase64编码的字符串转为图片
public static bool Base64StringToImage(string strbase64, string path)
{
    try
    {
        var arr = Convert.FromBase64String(strbase64);
        var ms = new MemoryStream(arr);
        var bmp = new Bitmap(ms);

        bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        ms.Close();
        return true;
    }
    catch (Exception ex)
    {
        LogHelper.Fatal(ex.Message, ex);
        return false;
    }
}