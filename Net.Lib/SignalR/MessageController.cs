using MessageService.Entity;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MessageService.WebApi.Controllers
{
    public class MessageController : ApiController
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet]
        public void Registration(int userId)
        {
            var cookie = new HttpCookie("UserId", userId.ToString())
            {
                Expires = DateTime.Now.AddYears(1)
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public bool Send(DynamicMessage request)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            hubContext.Clients.Users(request.UserIds.Select(p => p.ToString()).ToList()).broadcastMessage(request.DynamicShowArea);
            return true;
        }
    }
}
