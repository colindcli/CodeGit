// 手机、PC根据大小互跳：判断移动端、判断pc端
var oWidth = document.documentElement.clientWidth || document.body.clientWidth;
if (oWidth < 1200) {} else {
    window.location.href = "default.html";
}
window.addEventListener("orientationchange", function() {
    var oWidth = document.documentElement.clientWidth || document.body.clientWidth;

    if (oWidth < 1200) {

    } else {
        window.location.href = "default.html";
    }
}, false);
window.addEventListener("resize", function() {
    var oWidth = document.documentElement.clientWidth || document.body.clientWidth;
    if (oWidth < 1200) {} else {
        window.location.href = "default.html";
    }
}, false);



//https://github.com/nong0126/nong0126.github.com/blob/cc3a12525bee53a0bda5d8ffed1e733786569c3c/_site/html5/js/html5.js
//判断移动端（判断手机）
var isMobi = false;
if (/(phone|pad|pod|iPhone|iPod|ios|iPad|Android|Mobile|BlackBerry|IEMobile|MQQBrowser|JUC|Fennec|wOSBrowser|BrowserNG|WebOS|Symbian|Windows Phone)/i.test(navigator.userAgent)) {
    isMobi = true
}

if ((navigator.userAgent.match(/(phone|pad|pod|iPhone|iPod|ios|iPad|Android|Mobile|BlackBerry|IEMobile|MQQBrowser|JUC|Fennec|wOSBrowser|BrowserNG|WebOS|Symbian|Windows Phone)/i))) {
    alert('手机端')
}else{
    alert('PC端')
}

// 是否在微信浏览器中
function wx() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/micromessenger/i) == "micromessenger") {
        return true;
    }
    return false;
};

// 是否在微信APP版的浏览器中
function wxMobile() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/micromessenger/i) == "micromessenger" &&  ua.match(/mobile/i) == "mobile") {
        return true;
    }
    return false;
};

// 是否在微信PC版的浏览器中
function wxPc() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/micromessenger/i) == "micromessenger" &&  ua.match(/windowswechat/i) == "windowswechat") {
        return true;
    }
    return false;
};