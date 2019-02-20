using OAuthLogin;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ThirdPartyLoginController : Controller
    {
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Wechat()
        {
            var res = new Wechat().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new ThirdPartyUserModel
                {
                    Uid = res.result.Value<string>("uid"),
                    Name = res.result.Value<string>("nickname"),
                    Img = res.result.Value<string>("headimgurl"),
                    Token = res.token
                };
            }

            return null;
        }


        /// <summary>
        /// QQ登录
        /// </summary>
        /// <returns></returns>
        public ActionResult QQ()
        {
            var res = new QQ().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new ThirdPartyUserModel
                {
                    Uid = res.result.Value<string>("openid"),
                    Name = res.result.Value<string>("nickname"),
                    Img = res.result.Value<string>("figureurl"),
                    Token = res.token
                };
            }

            return null;
        }


        /// <summary>
        /// 微博登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Weibo()
        {
            var res = new Weibo().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new ThirdPartyUserModel
                {
                    Uid = res.result.Value<string>("idstr"),
                    Name = res.result.Value<string>("name"),
                    Img = res.result.Value<string>("profile_image_url"),
                    Token = res.token
                };
            }

            return null;
        }


        /// <summary>
        /// Facebook登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Facebook()
        {
            var res = new Facebook().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new ThirdPartyUserModel
                {
                    Uid = res.result.Value<string>("id"),
                    Name = res.result.Value<string>("name"),
                    Img = res.result["picture"]["data"].Value<string>("url"),
                    Token = res.token
                };
            }

            return null;
        }


        /// <summary>
        /// Kakao登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Kakao()
        {
            var res = new Kakao().Authorize();

            if (res != null && res.code == 0)
            {
                var m = new ThirdPartyUserModel
                {
                    Uid = res.result.Value<string>("uid"),
                    Name = res.result.Value<string>("nickname"),
                    Img = res.result.Value<string>("thumbnail_image"),
                    Token = res.token
                };
            }

            return null;
        }
    }
}