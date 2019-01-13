// sdk: https://docs.open.alipay.com/54/103419/
using Aop.Api;
using Aop.Api.Request;
using Newtonsoft.Json;
using SiteManage.Common;
using SiteManage.Entity.Models;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

public class AlipayController : Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    // GET: Pay
    public ActionResult Pay()
    {
        var serverUrl = "https://openapi.alipaydev.com/gateway.do";
        var appId = Config.AlipayAppId;
        var privateKeyPem = Config.AlipayAppPrivateKey;
        var format = "json";
        var version = "1.0";
        var signType = "RSA2";
        var alipayPulicKey = Config.AlipayAlipayPublicKey;
        var notifyUrl = $"{Config.Domain}/alipay/PayNotify";
        var returnUrl = $"{Config.Domain}/alipay/Return";
        var charset = "utf-8";
        var client = new DefaultAopClient(serverUrl, appId, privateKeyPem, format, version, signType, alipayPulicKey, charset, false);
        var request = new AlipayTradePagePayRequest
        {
            BizContent = JsonConvert.SerializeObject(new BizContentModel()
            {
                body = "Iphone6 16G",
                subject = "Iphone6 16G",
                //out_trade_no = "20150320010102001",
                out_trade_no = DateTime.Now.Ticks.ToString(),
                total_amount = decimal.Parse("88.88"),
                product_code = "FAST_INSTANT_TRADE_PAY"
            })
        };
        request.SetNotifyUrl(notifyUrl);
        request.SetReturnUrl(returnUrl);
        var response = client.pageExecute(request);
        var form = response.Body;

        return Content(form, "text/html", Encoding.UTF8);
    }

    [HttpPost]
    public ActionResult PayNotify()
    {
        var dict = Request.Form.Cast<string>().ToDictionary(s => s, s => Request.Form[s]);
        var str = JsonConvert.SerializeObject(dict);
        var model = JsonConvert.DeserializeObject<NotifyModel>(str);

        LogHelper.Debug($"PayNotify: {JsonConvert.SerializeObject(model)}");

        return null;
    }

    [HttpGet]
    public ActionResult Return()
    {
        LogHelper.Debug($"Return: {Request.Url}");
        return null;
    }
}

public class BizContentModel
{
    public string body { get; set; }
    public string subject { get; set; }
    public string out_trade_no { get; set; }
    public decimal total_amount { get; set; }
    public string product_code { get; set; }
}

//https://docs.open.alipay.com/270/105902/
public class NotifyModel
{
    /// <summary>
    /// 交易创建时间
    /// </summary>
    public string gmt_create { get; set; }
    /// <summary>
    /// 编码格式
    /// </summary>
    public string charset { get; set; }
    /// <summary>
    /// 交易付款时间
    /// </summary>
    public string gmt_payment { get; set; }
    /// <summary>
    /// 通知时间
    /// </summary>
    public string notify_time { get; set; }
    /// <summary>
    /// 订单标题
    /// </summary>
    public string subject { get; set; }
    /// <summary>
    /// 签名
    /// </summary>
    public string sign { get; set; }
    /// <summary>
    /// 买家支付宝用户号
    /// </summary>
    public string buyer_id { get; set; }
    /// <summary>
    /// 商品描述
    /// </summary>
    public string body { get; set; }
    /// <summary>
    /// 开票金额
    /// </summary>
    public string invoice_amount { get; set; }
    /// <summary>
    /// 接口版本
    /// </summary>
    public string version { get; set; }
    /// <summary>
    /// 通知校验ID
    /// </summary>
    public string notify_id { get; set; }
    /// <summary>
    /// 支付金额信息
    /// </summary>
    public string fund_bill_list { get; set; }
    /// <summary>
    /// 通知类型
    /// </summary>
    public string notify_type { get; set; }
    /// <summary>
    /// 商户订单号
    /// </summary>
    public string out_trade_no { get; set; }
    /// <summary>
    /// 订单金额
    /// </summary>
    public string total_amount { get; set; }
    /// <summary>
    /// 交易状态: TRADE_SUCCESS
    /// </summary>
    public string trade_status { get; set; }
    /// <summary>
    /// 支付宝交易号
    /// </summary>
    public string trade_no { get; set; }
    /// <summary>
    /// 授权方的app_id
    /// </summary>
    public string auth_app_id { get; set; }
    /// <summary>
    /// 实收金额
    /// </summary>
    public string receipt_amount { get; set; }
    /// <summary>
    /// 集分宝金额
    /// </summary>
    public string point_amount { get; set; }
    /// <summary>
    /// 开发者的app_id
    /// </summary>
    public string app_id { get; set; }
    /// <summary>
    /// 付款金额
    /// </summary>
    public string buyer_pay_amount { get; set; }
    /// <summary>
    /// 签名类型
    /// </summary>
    public string sign_type { get; set; }
    /// <summary>
    /// 卖家支付宝用户号
    /// </summary>
    public string seller_id { get; set; }
}