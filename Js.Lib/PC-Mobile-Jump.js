<!-- 手机、PC根据大小互跳 -->
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
