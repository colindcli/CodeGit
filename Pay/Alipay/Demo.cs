using Aop.Api;
using Aop.Api.Request;
using Newtonsoft.Json;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverUrl = "https://openapi.alipaydev.com/gateway.do";
            var appId = "2016091500517929";
            var privateKeyPem = @"MIIEpAIBAAKCAQEAy1BNsQ9uKA7bwgOSbr0G0PVIXJWLIsZrN1zmVjJnSgOKq5ZYn7gcjmXwhHngeaECsfWlOCIvE6xoG6rWwHYB2rBWSu3DhYxJ/P/tb6SA1KLS0531gUpQ3sM79DoBlQeYcK1fFcOkPdfm/w99NtCqfGL/qTpVqaOMKhPPI94Ss58IN3U9DCYWpTdgHQEVx2fZMlbWKZ3X0YiX529wQMHueTPbUTvgBwaPeLpB3GtYodUQaa+XBXNdB5YwY4tmlnH9JRu7DaWPoofAI6L64LG6SB8ODiL36+2bXXboKdXZNWezWNYHJrl3B7ryPzNMZrvrytHGHcK7Gx2kTYIGW+zeuQIDAQABAoIBAQChPZuP64PaQwZn1LtiFYl9WLm9q1/AAYpwSr3l6G1gFnT4ZfD9Il+LUy8vcRTkgRwJFJ6maP3a5WVfY9qSokQQMr8NB4mDtWHMQxAD9Xuypzr6VxCoK879C+rVYtd0YKS139lEAneUEFEDQT51pYE/yqelhzz+n1T+3dzuPiWoyvG0MvHXUR/rLFJtVTGv6S3wMinMDiCZJOypNTpIJWVzzElDcpJkY7HM47vh4HFKfoVLeg36Q687Kl6uKI72n5S7KtFIi4uVIdK/iJODugmpPxMVU29IKWIsVH0VJii0sL2wxQldUxHr4gIrRCUWrMms7a58m3+9WZkrtP9VGGH9AoGBAPB4cUcZRCpMZZDjOLmb6FRnrBd7JYfCbxCGGVZRk5dkPYdulZ7xEruycNifas6Wj1g3gwUZB8tkF7Z6RDe79PiOSYi3qZ5dVqHt3FHtRpCDY6BgjoYAS8BWXiB0XvUYk8UdzNK281g35ZBoQlhc8jQFumy1LmaGs5+3Cl2b9tvTAoGBANhxkcssxr+WhPl5E5TSZcEkK/G5C5cVIsAOgXPhhPiKvF6iTJ8tPVGhruLfwweKLWdUfi1u2kGILfNsnml8LZzy/PyWbzeJy+BGmEWaZzsR9uRGnuiYmcp9TyPlL5TIvQuLWgLSIgXk7fb+2b4qI45lxbF0T9hlLrA5Jiwj+7/DAoGAYjjABEuEtUZJQhodpGrTRg6mk1zAoqg/l18+4Cwn/fF61GPOB4LupO4o+8J57PJAkMbz5FQqA+DZcraUQLOsRw5PaOGkDcSQS0oN8QTbCtIwEmT+MivSxThB8D24P6KPUTdv3y3NTd5fDVIYr2EfqTIhyJ9k2Ynxuq4YNM4s0fMCgYEAivX/QI5y27ZrkS+m3rlmEuaCQCMpycoMEXo+qoNzt456+dcBkoCdCbfgbFB1CnYwSFL633h4L/Kabdqiqd7L+iQXQKPkq6eQeWFZifZjqI0UEeM1SugOdO6WzNjr/34RwUOqen0m0tnz3cwpR+oOAfJdRl0Clk//9av7UgOYMbkCgYB6sc786s79aRcAa4sbDOaMEGVKv+Vh59xZ24mOUxHnr2XEFNJOX+QF8LWpB0/wcnYwbcK7qT3aS/a7vGpxibBF33B2yWfMNcD+Yo75ElM8Jznk95qlC/kljtSPxQ08kEM10dtkWV7NFirAogBA+8KowD/e6frjrxicWqlldiMyJg==";
            var format = "json";
            var version = "1.0";
            var signType = "RSA2";
            var alipayPulicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA3B/tkuZB+zL2MPeBB/Oen0KHc58I9pVbcWobyBOnliFZybV51lCzqt0+1Z/2rR3+D34gnbexNuHX6hyaZmxJKN23zpRWxaMCMCpln45aJKchkeczTZifXL0E4auQQCMICbzDwchJ+/HvcavG3Tje+A8/t1NgYoSPkEu7ig8ztIeipHEyjDmNSk4uulObeZD9veTPJN83sqnqgev4UtPEqs2Y0fRTaKZIXKDBdYBYATtoubGEJLeMoWLpm4TcjNFhxq6Ip3NvKM0U476/gV/Sqnh4z/IgfN/90z2yaScvWBZTXM08z+ElhxGWcpToZtxoBGAj4yVDaC9lf/vaFEGb+wIDAQAB";
            var notifyUrl = "http://www.fzxgj.top/alipay/notifyUrl";
            var returnUrl = "http://www.fzxgj.top/alipay/returnUrl";
            var charset = "utf-8";
            var client = new DefaultAopClient(serverUrl, appId, privateKeyPem, format, version,signType,alipayPulicKey,charset, false);
            var request = new AlipayTradePagePayRequest
            {
                BizContent = JsonConvert.SerializeObject(new BizContentModel()
                {
                    body = "Iphone6 16G",
                    subject = "Iphone6 16G",
                    out_trade_no = "20150320010101003",
                    total_amount = decimal.Parse("88.88"),
                    product_code = "FAST_INSTANT_TRADE_PAY"
                })
            };
            request.SetNotifyUrl(notifyUrl);
            request.SetReturnUrl(returnUrl);
            var response = client.pageExecute(request);
            var form = response.Body;
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
}
