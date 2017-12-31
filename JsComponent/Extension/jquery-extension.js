$.ajaxSetup({
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    error: function(jqXHR, textStatus, errorMsg) {
        alert('发送AJAX请求到"' + this.url + '"时出错[' + jqXHR.status + ']：' + errorMsg);
    }
});


var obj = {
    post: function(url, data, callback) {
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data || {}),
            success: function(data) {
                if (data.code !== 0) {
                    layer.msg(data.msg);
                } else {
                    callback(data.data);
                }
            }
        });
    },
    postFrom: function($formObj, url, callback, setParamCallback) {
        var self = this;
        $formObj.unbind("submit").on("submit", function() {
            var fields = $(this).serializeArray();
            var obj = {};
            $.each(fields, function(i, field) {
                obj[field.name] = field.value;
            });
            if (setParamCallback) {
                var otherParam = setParamCallback();
                $.extend(obj, otherParam);
            }
            self.post(url, obj, callback);
            return false;
        });
    },
    get: function(url, data, callback) {
        $.get(url, data, function(data) {
            if (data.code !== 0) {
                layer.msg(data.msg);
            } else {
                callback(data.data);
            }
        });
    },
    setFormValue: function(jsonObject) {
        var patt = new RegExp("/Date\\((\\d+)\\)/");
        var padate = new RegExp('(\\d{4})-(\\d{2})-(\\d{2})T(\\d{2}):(\\d{2}):(\\d{2})');
        $.each(jsonObject,
            function(name, val) {
                if (val !== "") {
                    if (patt.test(val)) {
                        val = val.ToDate(val);
                    } else if (padate.test(val)) {
                        val = val.substr(0, 10);
                    }

                    var htmlType = $("[name='" + name + "']").attr("type");
                    if (htmlType === "text" ||
                        htmlType === "textarea" ||
                        htmlType === "select-one" ||
                        htmlType === "hidden" ||
                        htmlType === "button") {
                        $("[name='" + name + "']").val(val);
                    } else if (htmlType === "radio") {
                        var obj = $("input[type=radio][name='" + name + "'][value='" + val + "']");
                        obj.prop("checked", true).next().find("i").click();
                    } else if (htmlType === "checkbox") {
                        var obj = $("input[type=checkbox][name='" + name + "']");
                        obj.prop("checked", !val).next().find("i").click();

                        //var vals = val.split(';');
                        //for (var i = 0; i < vals.length; i++) {
                        //    $("input[type=checkbox][name='" + name + "'][value='" + vals[i] + "']")
                        //        .prop("checked", true);
                        //}
                    } else {
                        $("[name='" + name + "']").val(val);
                    }
                }
            });
    }
};