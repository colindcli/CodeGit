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
            var cookie = request.Cookies["UserId"];
            return !string.IsNullOrWhiteSpace(cookie?.Value) ? cookie.Value : null;
        }
    }
}
