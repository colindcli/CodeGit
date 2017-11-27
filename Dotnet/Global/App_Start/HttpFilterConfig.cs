using System.Web.Http.Filters;

/// <summary>
/// HTTP Filter
/// </summary>
public class HttpFilterConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filters"></param>
    public static void RegisterGlobalFilters(HttpFilterCollection filters)
    {
        filters.Add(new HttpErrorAttribute());
    }
}
