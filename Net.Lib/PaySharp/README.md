## PaySharp：支付宝(Alipay)、微信支付(Wechatpay)、银联支付(Unionpay)、QQ钱包支付(Qpay)

- 项目地址：https://github.com/Varorbc/PaySharp MIT


> 项目安装

- 核心文件：https://www.nuget.org/packages/PaySharp.Core 、Install-Package PaySharp.Core
- Mvc必须安装(包含注入)：https://www.nuget.org/packages/PaySharp.Core.Mvc
- Alipay安装：https://www.nuget.org/packages/PaySharp.Alipay
- Wechat安装：https://www.nuget.org/packages/PaySharp.Wechatpay
- 银联安装：https://www.nuget.org/packages/PaySharp.Unionpay
- QQ钱包安装：https://www.nuget.org/packages/PaySharp.Qpay


- 添加Global.asax.cs文件内容（如果使用Register注册需要添加Web.config文件）
- 添加Certs (银联支付需要)
- 添加Controlers文件


> 支付官方Demo：https://github.com/Varorbc/PaySharp/tree/master/document