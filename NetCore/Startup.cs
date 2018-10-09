public class Startup
{
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        //路由
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        });
        //app.UseMvcWithDefaultRoute();
    }
}