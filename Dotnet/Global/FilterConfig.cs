using System.Web.Mvc;

/// <summary>
/// MVC Filter
/// </summary>
public class FilterConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filters"></param>
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new ErrorAttribute());
    }
}
