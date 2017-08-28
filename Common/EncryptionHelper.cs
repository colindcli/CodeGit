using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#region DES加密/解密
/// <summary>
/// DES加密字符串
/// </summary>
/// <param name="str">待加密的字符串</param>
/// <param name="key">加密密钥,要求为8位</param>
/// <returns>加密成功返回加密后的字符串，失败返回null</returns>
public static string EncryptDes(string str, string key)
{
    try
    {
        var rgbKey = Encoding.ASCII.GetBytes(key.Substring(0, 8));
        var rgbIv = rgbKey;
        var inputByteArray = Encoding.ASCII.GetBytes(str);
        var dcsp = new DESCryptoServiceProvider();
        var mStream = new MemoryStream();
        var cStream = new CryptoStream(mStream, dcsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        var ret = new StringBuilder();
        foreach (var b in mStream.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        return ret.ToString();
    }
    catch (Exception ex)
    {
        throw ex;
    }
}
/// <summary>
/// DES解密字符串
/// </summary>
/// <param name="str">待解密的字符串</param>
/// <param name="key">解密密钥,要求为8位,和加密密钥相同</param>
/// <returns>解密成功返回解密后的字符串，失败返回null</returns>
public static string DecryptDes(string str, string key)
{
    try
    {
        var rgbKey = Encoding.ASCII.GetBytes(key);
        var rgbIv = rgbKey;
        var inputByteArray = new byte[str.Length / 2];
        for (var x = 0; x < str.Length / 2; x++)
        {
            var i = (Convert.ToInt32(str.Substring(x * 2, 2), 16));
            inputByteArray[x] = (byte)i;
        }
        var dcsp = new DESCryptoServiceProvider();
        var mStream = new MemoryStream();
        var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        return Encoding.ASCII.GetString(mStream.ToArray());
    }
    catch (Exception ex)
    {
        throw ex;
    }
}
#endregion

#region 非对称算法

public static string EncryptByPublic(string str, string publicKey)
{
    var crypt = new RSACryptoServiceProvider();
    crypt.FromXmlString(publicKey);
    var enc = new UTF8Encoding();

    var bytes = enc.GetBytes(str);
    bytes = crypt.Encrypt(bytes, false);
    return Convert.ToBase64String(bytes);
}
public static string DecryptByPrivate(string str, string privateKey)
{
    var crypt = new RSACryptoServiceProvider();
    crypt.FromXmlString(privateKey);
    var enc = new UTF8Encoding();
    var bytes = Convert.FromBase64String(str);
    var decryptbyte = crypt.Decrypt(bytes, false);
    return enc.GetString(decryptbyte);
}
public static string SignDataByPrivate(string str, string privateKey)
{
    var crypt = new RSACryptoServiceProvider();
    crypt.FromXmlString(privateKey);
    var bytes = Encoding.UTF8.GetBytes(str);
    var signData = crypt.SignData(bytes, "SHA1");
    return Convert.ToBase64String(signData);
}

public static bool VerifyDataByPublic(string str, string signKey, string publicKey)
{
    var crypt = new RSACryptoServiceProvider();
    crypt.FromXmlString(publicKey);
    var bytes = Encoding.UTF8.GetBytes(str);
    var signData = Convert.FromBase64String(signKey);
    return crypt.VerifyData(bytes, "SHA1", signData);
}

#endregion

#region 不可逆加密
/// <summary>
/// 不可逆加密字符串
/// </summary>
/// <param name="str">普通的字符串</param>
/// <returns>加密后的二进制字符串</returns>
/// <remarks>不可逆加密</remarks>
public static string EncryptMd5(string str)
{
    var algroithm = new MD5CryptoServiceProvider();
    var buffer = algroithm.ComputeHash(Encoding.UTF8.GetBytes(str));
    return BitConverter.ToString(buffer);
}

/// <summary>
/// SHA256加密，不可逆转
/// </summary>
/// <param name="str">string str:被加密的字符串</param>
/// <returns>返回加密后的字符串</returns>
public static string EncryptSha256(string str)
{
    var pwd = "";
    using (var hash = (HashAlgorithm)SHA256.Create())
    {
        var s = hash.ComputeHash(Encoding.UTF8.GetBytes(str));
        foreach (var c in s)
        {
            pwd = pwd + c.ToString("X2");
        }
    }
    return pwd;
}
#endregion
