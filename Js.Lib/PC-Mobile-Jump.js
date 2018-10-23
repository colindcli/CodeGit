// 手机、PC根据大小互跳：判断移动端、判断pc端
<script>
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
</script>


//https://github.com/nong0126/nong0126.github.com/blob/cc3a12525bee53a0bda5d8ffed1e733786569c3c/_site/html5/js/html5.js
<script>
(function (window) {
    //判断移动端（判断手机）
    var isMobi = false;
    if (/(iPhone|iPad|iPod|android|windows phone os|iemobile)/i.test(navigator.userAgent)) {
        isMobi = true;
    }
})();
</script>