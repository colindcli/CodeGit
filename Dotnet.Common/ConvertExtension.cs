using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public static class ConvertExtender
{
    /// <summary>
    /// 字节组转流
    /// </summary>
    /// <param name="bt">字节组</param>
    /// <returns>流</returns>
    public static Stream ByteToStream(this byte[] bt)
    {
        return new MemoryStream(bt);
    }

    /// <summary>
    /// 字节组转字符串
    /// </summary>
    /// <param name="bt">字节组</param>
    /// <returns>字符串</returns>
    public static string ByteToString(this byte[] bt)
    {
        return Encoding.UTF8.GetString(bt);
    }

    /// <summary>
    /// 文件转流
    /// </summary>
    /// <param name="fileName">文件地址</param>
    /// <returns>流</returns>
    public static Stream FileToStream(this string fileName)
    {
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        byte[] bytes = new byte[fileStream.Length];
        fileStream.Read(bytes, 0, bytes.Length);
        fileStream.Close();
        Stream stream = new MemoryStream(bytes);
        return stream;
    }

    /// <summary>
    /// 流转字节组
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>字节组</returns>
    public static byte[] StreamToByte(this Stream stream)
    {
        //byte[] bt = new byte[stream.Length];
        //stream.Read(bt, 0, bt.Length);
        //stream.Seek(0, SeekOrigin.Begin);
        return ((MemoryStream)stream).ToArray();
    }

    /// <summary>
    /// 流转文件
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="fileName">文件地址</param>
    /// <returns>是否成功</returns>
    public static bool StreamToFile(this Stream stream, string fileName)
    {
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        FileStream fs = new FileStream(fileName, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(bytes);
        bw.Close();
        fs.Close();
        return File.Exists(fileName);
    }

    /// <summary>
    /// 流转字符串
    /// </summary>
    /// <param name="stream">流</param>
    /// <returns>字符串</returns>
    public static string StreamToString(this Stream stream)
    {
        return ByteToString(StreamToByte(stream));
    }

    /// <summary>
    /// 字符串转字节组
    /// </summary>
    /// <param name="s">字符串</param>
    /// <returns>字节组</returns>
    public static byte[] StringToByte(this string s)
    {
        return Encoding.UTF8.GetBytes(s);
    }

    /// <summary>
    /// 字符串转流
    /// </summary>
    /// <param name="s">字符串</param>
    /// <returns>流</returns>
    public static Stream StringToStream(this string s)
    {
        return ByteToStream(StringToByte(s));
    }

    /// <summary>
    /// 对象转字节组
    /// </summary>
    /// <param name="obj">原始数据</param>
    /// <returns>二进制</returns>
    public static byte[] ToBytes(this object obj)
    {
        var f = new BinaryFormatter();
        using (var inStream = new MemoryStream())
        {
            f.Serialize(inStream, obj);//对象序列化 
            inStream.Position = 0;
            return inStream.ToArray();
        }
    }

    /// <summary>
    /// 字节组转对象
    /// </summary>
    /// <param name="buffer">二进制</param>
    /// <returns>原始数据</returns>
    public static object ToObject(this byte[] buffer)
    {
        var f = new BinaryFormatter();
        using (var inStream = new MemoryStream(buffer))
        {
            return f.Deserialize(inStream);
        }
    }
}
