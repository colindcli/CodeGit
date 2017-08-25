/// <summary>
/// 解密
/// </summary>
/// <param name="DeString">字符串</param>
/// <param name="Key">解密码</param>
/// <returns></returns>
public static string Decode(string DeString, string Key = "DcEncryt")
{
    DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
    provider.Key = Encoding.ASCII.GetBytes(Key.Substring(0, 8));
    provider.IV = Encoding.ASCII.GetBytes(Key.Substring(0, 8));
    byte[] buffer = new byte[DeString.Length / 2];
    for (int i = 0; i < (DeString.Length / 2); i++)
    {
        int num2 = Convert.ToInt32(DeString.Substring(i * 2, 2), 0x10);
        buffer[i] = (byte)num2;
    }
    MemoryStream stream = new MemoryStream();
    CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
    stream2.Write(buffer, 0, buffer.Length);
    stream2.FlushFinalBlock();
    stream.Close();
    return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());
}

/// <summary>
/// 加密
/// </summary>
/// <param name="EnString">字符串</param>
/// <param name="Key">加密密码</param>
/// <returns></returns>
public static string Encode(string EnString, string Key = "DcEncryt")
{
    DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
    provider.Key = Encoding.ASCII.GetBytes(Key.Substring(0, 8));
    provider.IV = Encoding.ASCII.GetBytes(Key.Substring(0, 8));
    byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(EnString);
    MemoryStream stream = new MemoryStream();
    CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
    stream2.Write(bytes, 0, bytes.Length);
    stream2.FlushFinalBlock();
    StringBuilder builder = new StringBuilder();
    foreach (byte num in stream.ToArray())
    {
        builder.AppendFormat("{0:X2}", num);
    }
    stream.Close();
    return builder.ToString();
}

/// <summary>
/// MD5加密
/// </summary>
/// <param name="pwd"></param>
/// <returns></returns>
public static string MD5(string pwd)
{
    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5");
}
