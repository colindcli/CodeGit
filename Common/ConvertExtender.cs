using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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

/// <summary>
/// 字节组转字符串
/// </summary>
/// <param name="bt">字节组</param>
/// <returns>字符串</returns>
public static string ToString(this byte[] bt)
{
    return Encoding.UTF8.GetString(bt);
}

/// <summary>
/// 字符串转字节组
/// </summary>
/// <param name="s">字符串</param>
/// <returns>字节组</returns>
public static byte[] ToBytes(this string s)
{
    return Encoding.UTF8.GetBytes(s);
}
