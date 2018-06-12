using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// 
/// </summary>
public class DesEncrypt
{
    /// <summary>
    /// 
    /// </summary> 
    /// <param name="text"></param> 
    /// <param name="sKey"></param> 
    /// <returns></returns> 
    public static string Encrypt(string text, string sKey)
    {
        var des = new DESCryptoServiceProvider();
        var inputByteArray = Encoding.Default.GetBytes(text);
        var rgbKey = Encoding.ASCII.GetBytes(sKey.Substring(0, 8));
        des.Key = rgbKey;
        des.IV =   rgbKey;
        var ms = new System.IO.MemoryStream();
        var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        var ret = new StringBuilder();
        foreach (var b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        return ret.ToString();
    }

    /// <summary> 
    /// 
    /// </summary> 
    /// <param name="text"></param> 
    /// <param name="sKey"></param> 
    /// <returns></returns> 
    public static string Decrypt(string text, string sKey)
    {
        var des = new DESCryptoServiceProvider();
        var len = text.Length / 2;
        var inputByteArray = new byte[len];
        for (var x = 0; x < len; x++)
        {
            var i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
            inputByteArray[x] = (byte)i;
        }
        var rgbKey = Encoding.ASCII.GetBytes(sKey.Substring(0, 8));
        des.Key = rgbKey;
        des.IV = rgbKey;
        var ms = new System.IO.MemoryStream();
        var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return Encoding.Default.GetString(ms.ToArray());
    }
}