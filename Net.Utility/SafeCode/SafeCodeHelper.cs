using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class SafeCodeHelper
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static string GenerateSafeCode(out MemoryStream ms)
    {
        var chars = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ".ToCharArray();
        var random = new Random();

        var checkCode = string.Empty;
        for (var i = 0; i < 4; i++)
        {
            var rc = chars[random.Next(0, chars.Length)];
            if (checkCode.IndexOf(rc) > -1)
            {
                i--;
                continue;
            }
            checkCode += rc;
        }

        var iwidth = checkCode.Length * 17;
        var image = new Bitmap(iwidth, 25);
        var g = Graphics.FromImage(image);
        g.Clear(Color.White);
        //定义颜色
        Color[] c = { Color.DarkBlue, Color.DarkOrange, Color.DarkRed, Color.DarkViolet, Color.Chartreuse, Color.DarkTurquoise, Color.Black };
        var rand = new Random();

        //输出不同字体和颜色的验证码字符
        for (var i = 0; i < checkCode.Length; i++)
        {
            var cindex = rand.Next(c.Length);
            var f = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Italic);
            var b = new SolidBrush(c[cindex]);
            g.DrawString(checkCode.Substring(i, 1), f, b, (i * 14), 0, StringFormat.GenericDefault);
        }

        //画一个边框
        //g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);

        //输出到浏览器
        var stream = new MemoryStream();
        image.Save(stream, ImageFormat.Jpeg);
        ms = stream;
        ms.Position = 0;
        g.Dispose();
        image.Dispose();

        return checkCode;
    }
}