## 判断Pc端|移动端、安卓|IOS、微信|QQ



> 所有的UserAgent

- [useragentstring](http://www.useragentstring.com/pages/useragentstring.php?name=All)


> 库

- 判断PC移动：[express-useragent](https://github.com/biggora/express-useragent)
- UA转换库：[ua-parser-js](https://github.com/faisalman/ua-parser-js)
- UA解析库：[Browser.js](https://github.com/mumuy/browser/blob/master/Browser.js)



> PC端移动端

- 简单判断

(/mobile/i).test(navigator.userAgent.toLowerCase());


-- 复杂判断

if ((navigator.userAgent.match(/(phone|pad|pod|iPhone|iPod|ios|iPad|Android|Mobile|BlackBerry|IEMobile|MQQBrowser|JUC|Fennec|wOSBrowser|BrowserNG|WebOS|Symbian|Windows Phone)/i))) {
    alert('手机端')
}else{
    alert('PC端')
}


> 安卓

(/android|adr/i).test(navigator.userAgent.toLowerCase());


> 苹果【不确定】

(/iphone|iPad|ipod|ios|mac/i).test(navigator.userAgent.toLowerCase());


> 微信

(/micromessenger/i).test(navigator.userAgent.toLowerCase());


> Pc版微信

(/micromessenger/i).test(navigator.userAgent.toLowerCase()) && (/windowswechat/i).test(navigator.userAgent.toLowerCase());
