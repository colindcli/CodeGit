using Newtonsoft.Json;
using StackExchange.Redis;
using System;

public class StackExchangeRedisHelper
{
    //多个用“,”分割
    private const string ConnectString = @"127.0.0.1:6379";

    private static T Db<T>(Func<IDatabase, T> func)
    {
        var redis = ConnectionMultiplexer.Connect(ConnectString);
        var db = redis.GetDatabase();
        var v = func(db);
        redis.Close();
        redis.Dispose();
        return v;
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
    /// 写入
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiry">到期时间</param>
    /// <returns></returns>
    public static bool Set(string key, object value, DateTime? expiry = null)
    {
        return Db(p => p.StringSet(key, ToJson(value), expiry.HasValue ? expiry - DateTime.Now : null));
    }

    /// <summary>
    /// 读取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T Get<T>(string key) where T : class
    {
        return Db(p =>
        {
            var s = p.StringGet(key);
            return !string.IsNullOrWhiteSpace(s) ? FromJson<T>(s) : null as T;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool Delete(string key)
    {
        return Db(p => p.KeyDelete(key));
    }
}
