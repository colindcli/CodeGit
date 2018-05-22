## 页面回跳参数

https://docs.open.alipay.com/270/alipay.trade.page.pay

对于PC网站支付的交易，在用户支付完成之后，支付宝会根据API中商户传入的return_url参数，通过GET请求的形式将部分支付结果参数通知到商户系统。


    total_amount=88.88&
    timestamp=2018-05-15+13%3A41%3A32&
    sign=PaTquTxlWJ7K3bHJu%2Fo9oBJm1t%2B0enr9Ilv3RmZGL%2BoO6Js%2FJKdxdInViObSdfD1bYHcyT3wALqahgsQST0WQYLNlBTy1TxDQKhJeJk%2B1jHAXbSI5tMU%2Fio%2F%2FncCLavXnE5vTxrFK%2FQDVerHSbhFovV6V369yTRHosGj6gRdCVc7or7RNeY07bGPpwjpjZJvY458TnWZZ3Ld7eRcZOkDdDjJcvkEPleOyVszPJtcCKQpYvaJLHSCueNCo2tY4OoDNw8ov8%2BeDrcHHlQA5U%2B4EdrbljCmbwfkwA6VVZ7ft%2F%2BeG8qgRlm69iy6CDnSqAepPLl5e%2F0Z2IHum0kPmIlSPw%3D%3D&
    trade_no=2018051521001004130200660936&
    sign_type=RSA2&
    auth_app_id=2016091500517929&
    charset=utf-8&
    seller_id=2088102175758686&
    method=alipay.trade.page.pay.return&
    app_id=2016091500517929&
    out_trade_no=20150320010101003&
    version=1.0
