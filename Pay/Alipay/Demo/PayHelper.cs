/// <summary>
/// 支付宝付款
/// </summary>
/// <returns></returns>
// GET: Pay
public static string AliPay(AlipayBizContentModel model)
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
        BizContent = JsonConvert.SerializeObject(model)
    };
    request.SetNotifyUrl(notifyUrl);
    request.SetReturnUrl(returnUrl);
    var response = client.pageExecute(request);
    return response.Body;
}