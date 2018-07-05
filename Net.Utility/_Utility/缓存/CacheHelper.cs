using System;
using System.Web;
using System.Web.Caching;

public class CacheHelper
{
    /// <summary>
    /// 获取数据缓存
    /// </summary>
    /// <param name="cacheKey">键</param>
    public static object GetCache<T>(string cacheKey)
    {
        var objCache = HttpRuntime.Cache;
        return (T)objCache[cacheKey];
    }

    /// <summary>
    /// 设置数据缓存
    /// </summary>
    public static void SetCache<T>(string cacheKey, T objObject)
    {
        var objCache = HttpRuntime.Cache;
        objCache.Insert(cacheKey, objObject);
    }

    /// <summary>
    /// 设置数据缓存
    /// </summary>
    public static void SetCache<T>(string cacheKey, T objObject, TimeSpan timeout)
    {
        var objCache = HttpRuntime.Cache;
        objCache.Insert(cacheKey, objObject, null, DateTime.MaxValue, timeout, CacheItemPriority.NotRemovable, null);
    }

    /// <summary>
    /// 设置数据缓存(指定缓存到时间)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="objObject"></param>
    /// <param name="absoluteExpiration">Convert.ToDateTime("2018-12-31")</param>
    /// <param name="slidingExpiration">TimeSpan.Zero</param>
    public static void SetCache<T>(string cacheKey, T objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        var objCache = HttpRuntime.Cache;
        objCache.Insert(cacheKey, objObject, null, absoluteExpiration, slidingExpiration);
    }

    /// <summary>
    /// 移除指定数据缓存
    /// </summary>
    public static void RemoveAllCache(string cacheKey)
    {
        var cache = HttpRuntime.Cache;
        cache.Remove(cacheKey);
    }

    /// <summary>
    /// 移除全部缓存
    /// </summary>
    public static void RemoveAllCache()
    {
        var cache = HttpRuntime.Cache;
        var cacheEnum = cache.GetEnumerator();
        while (cacheEnum.MoveNext())
        {
            if (cacheEnum.Key != null)
                cache.Remove(cacheEnum.Key.ToString());
        }
    }
}