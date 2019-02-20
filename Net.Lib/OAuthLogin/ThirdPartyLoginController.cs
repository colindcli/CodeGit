using OAuthLogin;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ThirdPartyLoginController : Controller
    {
        // Wechat
        public ActionResult Wechat()
        {
            var res = new Wechat().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new
                {
                    uid = res.result.Value<string>("uid"),
                    name = res.result.Value<string>("nickname"),
                    img = res.result.Value<string>("headimgurl"),
                    token = res.token
                };
            }

            return null;
        }


        // QQ
        public ActionResult QQ()
        {
            var res = new QQ().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new
                {
                    uid = res.result.Value<string>("openid"),
                    name = res.result.Value<string>("nickname"),
                    img = res.result.Value<string>("figureurl"),
                    token = res.token
                };
            }

            return null;
        }


        // Weibo
        public ActionResult Weibo()
        {
            var res = new Weibo().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new
                {
                    uid = res.result.Value<string>("idstr"),
                    name = res.result.Value<string>("name"),
                    img = res.result.Value<string>("profile_image_url"),
                    token = res.token
                };
            }

            return null;
        }


        // Facebook
        public ActionResult Facebook()
        {
            var res = new Facebook().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new
                {
                    uid = res.result.Value<string>("id"),
                    name = res.result.Value<string>("name"),
                    img = res.result["picture"]["data"].Value<string>("url"),
                    token = res.token
                };
            }

            return null;
        }


        // Kakao
        public ActionResult Kakao()
        {
            var res = new Kakao().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new
                {
                    uid = res.result.Value<string>("uid"),
                    name = res.result.Value<string>("nickname"),
                    img = res.result.Value<string>("thumbnail_image"),
                    token = res.token
                };
            }

            return null;
        }
    }
}