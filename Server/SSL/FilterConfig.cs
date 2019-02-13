public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        //http跳转到https
        filters.Add(new RequireHttpsAttribute());
    }
}