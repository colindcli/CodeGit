// ==UserScript==
// @name         填写表单
// @namespace    http://tampermonkey.net/
// @version      1
// @description  data
// @author       微信:tonylee899
// @match        https://*.accesso.com/*
// @grant        none
// @require https://code.jquery.com/jquery-2.1.4.min.js
// @run-at document-start
// ==/UserScript==

//https://cf-cw.secure-cdn.accesso.com/guestOptions/Wizard?_cid=903018729.1525146900
(function() {
    'use strict';

    // Your code here...
    String.prototype.compare = function(str) {
        if (this.toLowerCase() == str.toLowerCase()) {
            return true;
        } else {
            return false;
        }
    };
    var userDataObj = {
        has: false,
        init: function() {
            var self = this;
            setInterval(function() {
                self.try();
            }, 1000);
        },
        try: function() {
            var self = this;
            var bd = $("body");

            if (window.location.href.indexOf('accesso.com/billingView') > -1) {
                if (bd.length > 0 && !self.has) {
                    console.log(new Date());
                    self.has = true;
                    bd.css({
                        "padding-bottom": "215px"
                    });
                    bd.append('<div id="userDataDiv" style="position: fixed;border-top: 2px solid #43b25c;bottom: 0px;width: 100%;background: #dbf0e0;z-index: 99999999;"><div class="userdc" style="padding: 20px;"><textarea id="userData" style="width: 100%;height: 120px;background: #fff;padding: 12px 12px;font-size: 16px;font-weight: normal;color: #000;border: 1px solid #ccc;border-radius: 4px;"></textarea><div style="text-align: right;margin: 8px 0;"><input id="userdataBtn" type="button" value="写入表单" class="btn btn-primary btn-block ng-binding" style=""></div></div></div>');
                    $("#userData").unbind("blur").on("blur", function() {
                        self.handdler();
                        self.handdler();
                    }).focus();
                    $("#userdataBtn").unbind().on("click", function() {
                        self.handdler();
                        self.handdler();
                    });

                    //$("#userData").val("451210682361145200|11/20|758|Herbert Bachert|4 Tanner Circle|CATHARINES|ONTARIO|L2N3M9|CANADA|2233444555|inumbr@nichestarvation.com|||||||	-Unchecked");
                }
            } else {
                $("#userDataDiv").remove();
                self.has = false;
            }
        },
        handdler: function() {
            var text = $("#userData").val();
            var array = text.split('|');
            $.each(array, function(i, item) {
                item = $.trim(item);
                //card Number
                if (i == 0) {
                    $("#account").val(item).change().focus().blur();
                }
                if (i == 1) {
                    var ym = item.split('/');
                    $("#cardExpirationMonth").val("string:" + ym[0]).change().focus().blur();
                    $("#cardExpirationYear").val("string:" + ym[1]).change().focus().blur();
                }
                if (i == 2) {
                    $("#securityCode").val(item).change().focus().blur();
                }
                //姓名
                if (i == 3) {
                    if (item != '') {
                        $("#fname,#middleInitial,#lname").val('');
                        var arr = item.split(' ');
                        if (arr.length == 1) {
                            $("#fname").val(arr[0]);
                        } else if (arr.length == 2) {
                            $("#fname").val(arr[0]);
                            $("#lname").val(arr[1]);
                        } else if (arr.length == 3) {
                            $("#fname").val(arr[0]);
                            $("#middleInitial").val(arr[1]);
                            $("#lname").val(arr[2]);
                        } else {
                            var ars = [];
                            $.each(arr, function(j, v) {
                                if (j < arr.length - 1) {
                                    ars.push(v);
                                }
                            });
                            $("#fname").val(ars.join(' '));
                            $("#lname").val(arr[arr.length - 1]);
                        }

                        $("#fname,#middleInitial,#lname").change().focus().blur();
                    }
                }
                //电话
                if (i == 9) {
                    $("#phone").val(item).change().focus().blur();
                }
                //邮箱
                if (i == 10) {
                    $("#email,#emailConfirmation").val(item).change().focus().blur();
                }
                //street address
                if (i == 4) {
                    $("#address1").val(item).change().focus().blur();
                }
                //apt/suite
                if (i == 4) {
                    //$("#address2").val(item).change().focus().blur();
                }
                //country
                if (i == 8) {
                    var cObj = $('[name="country"]').eq(1);
                    var co = cObj.find("option");
                    var v = "";
                    $.each(co, function() {
                        var txt = $(this).text();
                        if (txt.compare(item)) {
                            v = $(this).val();
                            return;
                        }
                    });
                    cObj.val(v).change().focus().blur();
                }
                //zip
                if (i == 7) {
                    $("#zip").val(item).change().focus().blur();
                }
                //city
                if (i == 5) {
                    $("#inputCity").val(item).change().focus().blur();
                }
                //province
                if (i == 6) {
                    var vs = "";
                    var opts = $('[name="state"] option');
                    console.log($('[name="state"]').html());
                    $.each(opts, function() {
                        var txt = $(this).text();
                        console.log(txt, item, txt.compare(item));
                        if (txt.compare(item)) {
                            vs = $(this).val();
                            return;
                        }
                    });
                    if (vs == "") {
                        vs = "string:ON";
                    }
                    $('[name="state"]').val(vs).change().focus().blur();
                }

                console.log(i + ":" + item);
            });
        }
    };
    userDataObj.init();

})();