## SignalR


> 安装

- Install-Package Microsoft.AspNet.SignalR -Version 2.4.1


> 根目录创建Startup.cs

```C#
using Microsoft.AspNet.SignalR;
using Owin;

/// <summary>
/// 
/// </summary>
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.MapSignalR();
    }
}
```


> 创建Hub

```C#
/// <summary>
/// 
/// </summary>
public class MessageHub : Hub
{

}
```


> 启动测试：Global.asax.cs


```C#
protected void Application_Start()
{
    Task.Run(() =>
    {
        for (var i = 0; i < 1000; i++)
        {
            //broadcastMessage前端定义的方法名
            GlobalHost.ConnectionManager.GetHubContext<MessageHub>().Clients.All.broadcastMessage(i);

            Thread.Sleep(1000);
        }
    });
}
```

> html页面

```html
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Demo</title>
</head>
<body>
    <script src="/Scripts/jquery-1.9.1.min.js"></script>
    <script src="/Scripts/jquery.signalR-2.4.1.min.js"></script>
    <script src="/signalr/hubs"></script>
    <script type="text/javascript">
        $(function () {
            $.connection.hub.logging = false;
            var chat = $.connection.messageHub;
            chat.client.broadcastMessage = function (obj) {
                console.log(obj);
            };
            //webSockets、foreverFrame、serverSentEvents、longPolling。
            //https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/introduction-to-signalr
            $.connection.hub.start({ transport: ['webSockets', 'longPolling'] });
        });
    </script>
</body>
</html>
```