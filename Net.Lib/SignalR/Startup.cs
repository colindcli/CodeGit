using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MessageService.WebApi.Startup))]

namespace MessageService.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var idProvider = new CustomUserIdProvider();
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);

            app.MapSignalR();
        }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            try
            {
                if (request.Cookies.Count == 0)
                {
                    return null;
                }

                var exists = false;
                foreach (var item in request.Cookies)
                {
                    if (string.Equals(item.Key, "UserId", StringComparison.OrdinalIgnoreCase))
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {
                    return null;
                }

                var cookie = request.Cookies["UserId"];
                return !string.IsNullOrWhiteSpace(cookie?.Value) ? cookie.Value : null;
            }
            catch (Exception ex)
            {
                LogHelper.Fatal(string.Join("; ", request.Cookies.Keys.Select(p => p).ToList()), ex);
                return null;
            }

        }
    }
}
