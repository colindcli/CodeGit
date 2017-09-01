using Newtonsoft.Json;
using Sider;
using System;

public class SiderHelper
{
    private const string Host = "127.0.0.1";
    private const int Port = 6379;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    private static T Db<T>(Func<RedisClient, T> func)
    {
        var client = new RedisClient(Host, Port);
        var b = func(client);
        client.Dispose();
        return b;
    }

    private static string ToJson(object value)
    {
        return JsonConvert.SerializeObject(value);
    }

    private static T FromJson<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value);
    }

    /// <summary>
    /// 写入键值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expireTime"></param>
    /// <returns></returns>
    public static bool Set(string key, object value, DateTime? expireTime = null)
    {
        return Db(p =>
        {
            var b = p.Set(key, ToJson(value));
            if (!b || !expireTime.HasValue) return b;
            var dt = Convert.ToDateTime(expireTime.Value.ToString("yyyy-MM-dd HH:mm:ss")).ToUniversalTime();
            var s = p.ExpireAt(key, dt);
            if (s) return true;
            p.Del(key);
            return false;
        });
    }

    /// <summary>
    /// 获取指定键值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T Get<T>(string key) where T : class
    {
        return Db(p =>
        {
            var s = p.Get(key);
            return !string.IsNullOrWhiteSpace(s) ? FromJson<T>(s) : null;
        });
    }

    /// <summary>
    /// 删除指定键值
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static int Delete(params string[] keys)
    {
        return Db(p => p.Del(keys));
    }

    /// <summary>
    /// 清空所有数据
    /// </summary>
    /// <returns></returns>
    public static bool FlushAll()
    {
        return Db(p => p.FlushAll());
    }
}
