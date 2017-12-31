var layUiObj = {
    alert: function(content, callback, title) {
        layer.alert(content, { title: title || '温馨提示' },
            function(index) {
                layer.close(index);
                if (callback) {
                    callback.call(new Object());
                }
            });
    },
    confirm: function(content, yesCallback, noCallback, title) {
        layer.confirm(content, { icon: 3, title: title || '温馨提示' }, function(index) {
            layer.close(index);
            if (yesCallback) {
                yesCallback.call(new Object(), index);
            }
        }, function(index) {
            layer.close(index);
            if (noCallback) {
                noCallback.call(new Object(), index);
            }
        });
    },
    warn: function(content, closeCallback) {
        layer.msg(content, function() {
            if (closeCallback) {
                closeCallback.call(new Object());
            }
        });
    },
    error: function(content) {
        layer.alert(content, { icon: 2, title: '错误提示' },
            function(index) {
                layer.close(index);
            });
    },
    success: function(content) {
        layer.msg(content, { icon: 6 });
    },
    iframe: function(url, closeCallback, width, height) {
        width = width || ($(document).width() / 2);
        height = height || ($(document).height() / 2);
        layer.open({
            type: 2,
            content: url,
            area: [width + 'px', height + 'px'],
            cancel: function() {
                if (closeCallback) {
                    closeCallback.call(new Object());
                }
            }
        });
    },
    dialog: function(id, loadedCallback, closeCallback, width, height, option) {
        var self = this;
        width = width || ($(document).width() / 2);
        height = height || ($(document).height() / 2);
        var opt = {
            type: 1,
            content: $('#' + id),
            area: [width + 'px', height + 'px'],
            cancel: function(index) {
                layer.close(index);
                this.content.hide();
                if (closeCallback) {
                    closeCallback.call(new Object());
                }
            },
            success: function(layero, index) {
                var obj = {
                    layero: layero,
                    index: index,
                    t: this
                };
                if (loadedCallback) {
                    loadedCallback.call(new Object(), obj);
                }
                self.escClose(obj);
            },
            title: ''
        };
        if (option) {
            $.extend(opt, option);
        }
        layer.open(opt);
    },
    dialogClose: function(obj) {
        layer.close(obj.index);
        obj.t.content.hide();
    },
    escClose: function(obj) {
        var self = this;
        $(document).unbind("keyup").on("keyup",
            function(e) {
                var code = e.originalEvent.keyCode;
                if (parseInt(code) == 27) {
                    self.dialogClose(obj);
                }
            });
    },
    selectDate: function(id) {
        laydate.render({
            elem: '#' + id,
            theme: '#d1282b'
                //,type: 'date' //默认，可不填
        });
    },
    upload: function(id, data, callback) {
        var _this = this;
        upload.render({
            elem: '#' + id,
            url: '/File/Upload' //必填项
                ,
            data: data || {} //可选项。额外的参数，如：{id: 123, abc: 'xxx'}
                ,
            auto: true,
            done: function(res, index, upload) {
                if (callback) {
                    callback.call(new Object(), res, index, upload);
                }
            },
            error: function(index, upload) {
                _this.alert('上传失败！');
            }
        });
    },
    date: function(id, callback) {
        laydate.render({
            elem: '#' + id,
            //,type: 'date' //默认，可不填
            change: function(value, date, endDate) {
                if (callback) {
                    callback.call(new Object(), value, date, endDate);
                }
            },
            theme: '#d1282b',
            position: 'fixed'
        });
    },
    pager: function(id, pageIndex, pageSize, total, callback) {
        laypage.render({
            elem: id //注意，这里的 test1 是 ID，不用加 # 号
                ,
            count: total //数据总数，从服务端得到
                ,
            limit: pageSize //每页显示的条数。
                ,
            curr: pageIndex,
            jump: function(obj, first) {
                if (callback) {
                    callback.call(new Object(), obj, first);
                }
            }
        });
    },
    colNormal: function(id, name, width, laytpl) {
        var obj = { field: id, title: name, type: 'normal' };
        if (width) {
            obj.width = width;
        }
        if (laytpl) {
            obj.templet = laytpl;
        }
        return obj;
    },
    colDate: function(id, name, width) {
        var obj = { field: id, title: name, type: 'normal' };
        if (width) {
            obj.width = width;
        }
        obj.templet = '<div>{{ d.' + id + '==null?"-": d.' + id + '.ToDate() }}</div>';
        return obj;
    },
    colEdit: function(id, editClassName) {
        if (!editClassName) {
            editClassName = 'edit';
        }
        var obj = {
            field: id,
            title: '',
            templet: '<span><button data-id="{{ d.' + id + ' }}" class="' + editClassName + ' layui-btn btn40">编辑</button><span>'
        };
        return obj;
    },
    colCheckbox: function(id, name, width) {
        var obj = { field: id, title: name, type: 'checkbox' };
        if (width) {
            obj.width = width;
        }
        return obj;
    },
    colNumbers: function(id, name, width) {
        var obj = { field: id, title: name, type: 'numbers' };
        if (width) {
            obj.width = width;
        }
        return obj;
    },
    editor: function(id) {
        var index = layedit.build('ArticleContent', {
            tool: [
                'strong' //加粗
                , 'italic' //斜体
                , 'underline' //下划线
                , 'del' //删除线

                , '|' //分割线

                , 'left' //左对齐
                , 'center' //居中对齐
                , 'right' //右对齐
                , 'link' //超链接
                , 'unlink' //清除链接
                , 'face' //表情
            ]
        });
        return index;
    },
    setEditorContent: function(id, content) {
        var obj = $("#" + id).parent().find(".layui-layedit");
        obj.find("iframe").contents().find("body").html($('#' + id).text());
    },
    getEditorContent: function(index) {
        return layedit.getContent(index);
    }
};