$.ajaxSetup({
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    error: function (jqXHR, textStatus, errorMsg) {
        alert('发送AJAX请求到"' + this.url + '"时出错[' + jqXHR.status + ']：' + errorMsg);
    }
});

var layer = layui.layer;
var laypage = layui.laypage;
var laydate = layui.laydate;
var upload = layui.upload;
var table = layui.table;

var dj = {
    post: function (url, data, callback) {
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data || {}),
            success: function (data) {
                if (data.code !== 0) {
                    layer.msg(data.msg);
                } else {
                    callback(data.data);
                }
            }
        });
    },
    get: function (url, data, callback) {
        $.get(url, data, function (data) {
            if (data.code !== 0) {
                layer.msg(data.msg);
            } else {
                callback(data.data);
            }
        });
    },
    alert: function (content, callback, title) {
        layer.alert(content,
            { title: title || '温馨提示' },
            function (index) {
                layer.close(index);
                if (callback) {
                    callback.call(new Object());
                }
            });
    },
    confirm: function (content, yesCallback, noCallback, title) {
        layer.confirm(content, { icon: 3, title: title || '温馨提示' }, function (index) {
            layer.close(index);
            if (yesCallback) {
                yesCallback.call(new Object(), index);
            }
        }, function (index) {
            layer.close(index);
            if (noCallback) {
                noCallback.call(new Object(), index);
            }
        });
    },
    warn: function (content, closeCallback) {
        layer.msg(content, function () {
            if (closeCallback) {
                closeCallback.call(new Object());
            }
        });
    },
    error: function (content) {
        layer.alert(content,
            { icon: 2, title: '错误提示' },
            function (index) {
                layer.close(index);
            });
    },
    success: function (content) {
        layer.msg(content, { icon: 6 });
    },
    iframe: function (url, closeCallback, width, height) {
        width = width || ($(document).width() / 2);
        height = height || ($(document).height() / 2);
        layer.open({
            type: 2,
            content: url,
            area: [width + 'px', height + 'px'],
            cancel: function () {
                if (closeCallback) {
                    closeCallback.call(new Object());
                }
            }
        });
    },
    dialog: function (id, loadedCallback, closeCallback, width, height) {
        width = width || ($(document).width() / 2);
        height = height || ($(document).height() / 2);
        layer.open({
            type: 1,
            content: $('#' + id),
            area: [width + 'px', height + 'px'],
            cancel: function (index) {
                layer.close(index);
                this.content.hide();
                if (closeCallback) {
                    closeCallback.call(new Object());
                }
            },
            success: function (layero, index) {
                if (loadedCallback) {
                    loadedCallback.call(layero, index);
                }
            }
        });
    },
    selectDate: function (id) {
        laydate.render({
            elem: '#' + id
            //,type: 'date' //默认，可不填
        });
    },
    upload: function (id, url, data, callback) {
        var _this = this;
        upload.render({
            elem: '#' + id
            , url: url //必填项
            , data: data || {} //可选项。额外的参数，如：{id: 123, abc: 'xxx'}
            , auto: true
            , done: function (res, index, upload) {
                if (callback) {
                    callback.call(new Object(), res, index, upload);
                }
            }
            , error: function (index, upload) {
                _this.alert('上传失败！');
            }
        });
    },
    date: function (id) {
        laydate.render({
            elem: '#' + id
            //,type: 'date' //默认，可不填
        });
    },
    pager: function (id, pageIndex, pageSize, total, callback) {
        laypage.render({
            elem: id //注意，这里的 test1 是 ID，不用加 # 号
            , count: total //数据总数，从服务端得到
            , limit: pageSize //每页显示的条数。
            , curr: pageIndex
            , jump: function (obj, first) {
                if (callback) {
                    callback(new Object(), obj, first);
                }
            }
        });
    },
    colNormal: function (id, name, width) {
        var obj = { field: id, title: name, type: 'normal' };
        if (width) {
            obj.width = width;
        }
        return obj;
    },
    colCheckbox: function (id, name, width) {
        var obj = { field: id, title: name, type: 'checkbox' };
        if (width) {
            obj.width = width;
        }
        return obj;
    },
    colNumbers: function (id, name, width) {
        var obj = { field: id, title: name, type: 'numbers' };
        if (width) {
            obj.width = width;
        }
        return obj;
    },
    treeView: function (id, callback, width, height) {
        treeViewObj.init({
            id: id,
            url: '/Manager/Organization/GetOrganizationList',
            width: width || 300,
            height: height || 350,
            callback: function (treeNode) {
                callback.call(new Object(), treeNode);
            },
            zIndex: 99999
        });
    },
    select: function (id, selectedId, width, multiple) {
        select2Obj.select(id, selectedId, width, multiple);
    },
    select2: function (id, data, selectedId, width, multiple) {
        select2Obj.select2(id, data, selectedId, width, multiple);
    },
    selectAjax: function (id, selectedId, width, multiple) {
        var url = "/Manager/User/GetUserList";
        select2Obj.selectAjax(id, url, selectedId, width, multiple);
    }
};

var TableObject = function (option) {
    var defaultOption = {
        id: {
            table: '',
            add: '',
            edit: '',
            delete: '',
            dialog: ''
        },
        url: {
            table: '',
            getForm: '',
            postForm: '',
            delete: ''
        },
        cols: [[]],
        tableDoneCallback: function (self, res, curr, count) {
            //console.log(self, res, curr, count);
        },
        dialogRenderCallback: function (self, data, layero, index) {
            //console.log(self, data, layero, index);
        },
        postFormCallback: function (self, data) {
            //console.log(self, data);
        },
        deleteCallback: function (self, data) {
            //console.log(self, data);
        },
        tableConfig: {
            height: 500,
            page: true
        },
        dialogConfig: {
            width: null,
            height: null
        }
    };

    var obj = {
        dialogIndex: null,
        tableObj: null,
        tableOption: null,
        init: function (param) {
            var self = this;
            layer.closeAll();
            self.renderTable(param);
            self.renderAction();
        },
        renderTable: function (param) {
            var self = this;
            var tObj = {
                elem: '#' + self.id.table,
                height: self.tableHeight,
                cols: self.cols,
                url: self.url.table,
                done: function (res, curr, count) {
                    self.tableDoneCallback(self, res, curr, count);
                },
                page: true,
                where: param
            };
            self.tableOption = $.extend({}, tObj, self.tableConfig);
            self.tableObj = table.render(self.tableOption);
        },
        renderAction: function () {
            var self = this;

            if (self.id.add !== '') {
                $("#" + self.id.add).unbind("click").on("click",
                    function () {
                        self.renderDialog();
                    });
            }

            if (self.id.edit !== '') {
                $("#" + self.id.edit).unbind("click").on("click",
                    function () {
                        var rows = self.getRowIds();
                        if (rows.length == 0) {
                            dj.warn("请选择编辑的行！");
                        } else if (rows.length > 1) {
                            dj.warn("请选择一行！");
                        } else {
                            dj.get(self.id.getForm,
                                rows,
                                function (data) {
                                    self.renderDialog(data);
                                });
                        }
                    });
            }

            if (self.id.delete !== '') {
                $("#" + self.id.delete).unbind("click").on("click",
                    function () {
                        var rows = self.getRowIds();
                        if (rows.length == 0) {
                            dj.warn("请选择删除的行！");
                        } else {
                            dj.confirm("确定删除吗？",function() {
                                dj.post(self.url.delete, rows, function (data) {
                                    dj.success("删除成功");
                                    self.deleteCallback.call(new Object(), self, data);
                                });
                            });
                        }
                    });
            }

            if (self.id.dialog !== '') {
                $("#" + self.id.dialog + " form").on("submit",
                    function () {
                        var fields = $(this).serializeArray();
                        var fObj = {};
                        $.each(fields,
                            function (i, field) {
                                fObj[field.name] = field.value;
                            });
                        dj.post(self.url.postForm, fObj, function (data) {
                            layer.close(self.dialogIndex);
                            dj.success("保存成功");
                            self.postFormCallback.call(new Object(), self, data);
                        });
                        return false;
                    });
            }
        },
        renderDialog: function (data) {
            var self = this;
            var width = self.dialogConfig.width || ($(document).width() / 2);
            var height = self.dialogConfig.height || ($(document).height() / 2);
            var dObj = {
                type: 1,
                content: $('#' + self.id.dialog),
                area: [width + 'px', height + 'px'],
                zIndex: 1000,
                cancel: function (index) {
                    layer.close(index);
                    this.content.hide();
                },
                success: function (layero, index) {
                    self.dialogIndex = index;
                    self.dialogRenderCallback.call(new Object(), self, data, layero, index);
                    self.setValue(data);
                }
            };
            var dOpt = $.extend({}, dObj, self.dialogConfig);
            layer.open(dOpt);
        },
        getRowIds: function () {
            var self = this;
            var checkStatus = table.checkStatus(self.id.table);
            var ids = [];
            $.each(checkStatus.data,
                function (i, item) {
                    ids.push(item);
                });
            return ids;
        },
        reload: function () {
            var self = this;
            self.tableObj.reload(self.tableOption);
        },
        setValue: function (jsonObject) {
            $.each(jsonObject,
                function (name, val) {
                    if (val !== "") {
                        var htmlType = $("[name='" + name + "']").attr("type");
                        if (htmlType === "text" ||
                            htmlType === "textarea" ||
                            htmlType === "select-one" ||
                            htmlType === "hidden" ||
                            htmlType === "button") {
                            $("[name='" + name + "']").val(val);
                        } else if (htmlType === "radio") {
                            $("input[type=radio][name='" + name + "'][value='" + val + "']")
                                .attr("checked", true);
                        } else if (htmlType === "checkbox") {
                            var vals = val.split(';');
                            for (var i = 0; i < vals.length; i++) {
                                $("input[type=checkbox][name='" + name + "'][value='" + vals[i] + "']")
                                    .attr("checked", true);
                            }
                        }
                    }
                });
        },
        clearForm: function () {
            var self = this;
            $('#'+self.id.dialog+" form").clearForm();
        }
    };

    var opt = $.extend({}, defaultOption, obj, option);
    return opt;
};