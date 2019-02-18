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
                //拿到结果数据，然后进行自定义跳转
                //res.result
            }

            return null;
        }


        // QQ
        public ActionResult QQ()
        {
            var res = new QQ().Authorize();

            if (res != null && res.code == 0)
            {
                //拿到结果数据，然后进行自定义跳转
                //res.result
            }

            return null;
        }


        // Weibo
        public ActionResult Weibo()
        {
            var res = new Weibo().Authorize();

            if (res != null && res.code == 0)
            {
                //拿到结果数据，然后进行自定义跳转
                //res.result
            }

            return null;
        }


        // Facebook
        public ActionResult Facebook()
        {
            var res = new Facebook().Authorize();

            if (res != null && res.code == 0)
            {
                //拿到结果数据，然后进行自定义跳转
                //res.result
            }

            return null;
        }


        // Kakao
        public ActionResult Kakao()
        {
            var res = new Kakao().Authorize();

            if (res != null && res.code == 0)
            {
                //拿到结果数据，然后进行自定义跳转
                //res.result
            }

            return null;
        }
    }
}