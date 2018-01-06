/// <summary>
/// 服务端
/// </summary>
public class ServerPoint
{
    /// <summary>
    /// 获取当前应用程序指定CacheKey的Cache值
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <returns></returns>
    public static object GetCache(string CacheKey)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        return objCache[CacheKey];
    }

    /// <summary>
    /// 设置当前应用程序指定CacheKey的Cache值
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <param name="objObject"></param>
    public static void SetCache(string CacheKey, object objObject)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject);
    }

    /// <summary>
    /// 设置当前应用程序指定CacheKey的Cache值
    /// </summary>
    /// <param name="CacheKey"></param>
    /// <param name="objObject"></param>
    /// <param name="absoluteExpiration">所插入对象将到期并被从缓存中移除的时间。要避免可能的本地时间问题（例如从标准时间改为夏时制），请使用 System.DateTime.UtcNow</param>
    /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象到期时之间的时间间隔。如果该值等效于 20 分钟，则对象在最后一次被访问 20 分钟之后将到期并被从缓存中移除。如果使用可调到期，则absoluteExpiration 参数必须为 System.Web.Caching.Cache.NoAbsoluteExpiration</param>
    public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        System.Web.Caching.Cache objCache = HttpRuntime.Cache;
        objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
    }
}

/// <summary>
/// 客户端
/// </summary>
public class ClientPoint
{
    /// <summary>
    /// 设置客户端缓存
    /// </summary>
    /// <param name="expires"></param>
    public static void SetCache(DateTime expires)
    {
        HttpContext.Current.Response.Cache.SetETag(DateTime.Now.Ticks.ToString());
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now);

        //public 以指定响应能由客户端和共享（代理）缓存进行缓存。
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);

        //是允许文档在被视为陈旧之前存在的最长绝对时间。
        HttpContext.Current.Response.Cache.SetMaxAge(TimeSpan.FromTicks(expires.Ticks));

        HttpContext.Current.Response.Cache.SetSlidingExpiration(true);
    }
}
