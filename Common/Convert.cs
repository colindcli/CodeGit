/// <summary>
/// Byte/Stream/String/File等互转
/// </summary>
public class ConvertIO
{
    /// <summary>
    /// 字节组转流
    /// </summary>
    /// <param name="bt">字节组</param>
    /// <returns>流</returns>
    public static Stream ByteToStream(byte[] bt)
    {
        return new MemoryStream(bt);
    }

    /// <summary>
    /// 字节组转字符串
    /// </summary>
    /// <param name="bt">字节组</param>
    /// <returns>字符串</returns>
    public static string ByteToString(byte[] bt)
    {
        return Encoding.UTF8.GetString(bt);
    }

    /// <summary>
    /// 文件转流
    /// </summary>
    /// <param name="fileName">文件地址</param>
    /// <returns>流</returns>
    public static Stream FileToStream(string fileName)
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
    public static byte[] StreamToByte(Stream stream)
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
    public static bool StreamToFile(Stream stream, string fileName)
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
    public static string StreamToString(Stream stream)
    {
        return ByteToString(StreamToByte(stream));
    }

    /// <summary>
    /// 字符串转字节组
    /// </summary>
    /// <param name="s">字符串</param>
    /// <returns>字节组</returns>
    public static byte[] StringToByte(string s)
    {
        return Encoding.UTF8.GetBytes(s);
    }

    /// <summary>
    /// 字符串转流
    /// </summary>
    /// <param name="s">字符串</param>
    /// <returns>流</returns>
    public static Stream StringToStream(string s)
    {
        return ByteToStream(StringToByte(s));
    }
}
