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
/// 32位MD5加密
/// </summary>
/// <param name="password"></param>
/// <returns></returns>
public static string MD5Encrypt32(string password)
{
    string cl = password;
    string pwd = "";
    MD5 md5 = MD5.Create(); //实例化一个md5对像
                            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
    // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
    for (int i = 0; i < s.Length; i++)
    {
        // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
        pwd = pwd + s[i].ToString("X");
    }
    return pwd;
}

public static string MD5Encrypt64(string password)
{
    string cl = password;
    //string pwd = "";
    MD5 md5 = MD5.Create(); //实例化一个md5对像
                            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
    return Convert.ToBase64String(s);
}
