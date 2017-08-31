using Sider;
using System;

public class SiderHelper
{
    private const string Host = "127.0.0.1";
    private const int Port = 6379;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    private static T2 Db<T, T2>(Func<RedisClient<T>, T2> func)
    {
        var client = new RedisClient<T>(Host, Port);
        var b = func(client);
        client.Dispose();
        return b;
    }

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

    /// <summary>
    /// 写入键值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expireTime"></param>
    /// <returns></returns>
    public static bool Set<T>(string key, T value, DateTime? expireTime = null)
    {
        return Db<T, bool>(p =>
        {
            var b = p.Set(key, value);
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
    public static T Get<T>(string key)
    {
        return Db<T, T>(p => p.Get(key));
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
