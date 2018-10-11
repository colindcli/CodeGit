using System;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// 验证码
/// </summary>
public class VerifyCodeHelper
{
    [DllImport("WmCode.dll")]
    private static extern bool LoadWmFromFile(string filePath, string password);

    [DllImport("WmCode.dll")]
    private static extern bool LoadWmFromBuffer(byte[] fileBuffer, int fileBufLen, string password);

    [DllImport("WmCode.dll")]
    private static extern bool GetImageFromFile(string filePath, StringBuilder vcode);

    [DllImport("WmCode.dll")]
    private static extern bool GetImageFromBuffer(byte[] fileBuffer, int imgBufLen, StringBuilder vcode);

    [DllImport("WmCode.dll")]
    private static extern bool SetWmOption(int optionIndex, int optionValue);

    [DllImport("urlmon.dll", EntryPoint = "URLDownloadToFileA")]
    private static extern int URLDownloadToFile(int pCaller, string szUrl, string szFileName, int dwReserved, int lpfnCb);

    private static readonly string DatPath = $"{AppDomain.CurrentDomain.BaseDirectory}test.dat";

    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="imgPath"></param>
    /// <param name="datPwd"></param>
    /// <returns></returns>
    public static string GetCode(string imgPath, string datPwd)
    {
        if (LoadWmFromFile(DatPath, datPwd))
        {
            //设置值
            SetWmOption(6, 90);

            var result = new StringBuilder();
            GetImageFromFile(imgPath, result);
            return result.ToString();
        }

        return "";
    }
}
