// 对浏览器的UserAgent进行正则匹配，不含有微信独有标识的则为其他浏览器
var useragent = navigator.userAgent;
if (useragent.match(/MicroMessenger/i) != 'MicroMessenger') {
    // 这里警告框会阻塞当前页面继续加载
    alert("请在微信端打开");
    // 以下代码是用javascript强行关闭当前页面
    var opened = window.open('', '_self');
    opened.opener = null;
    opened.close();
}