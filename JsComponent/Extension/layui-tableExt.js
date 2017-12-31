//layui-extension.js
var dj = $.extend({}, layUiObj);

var TableObject = function(option) {
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
        cols: [
            []
        ],
        tableDoneCallback: function(self, res, curr, count) {
            //console.log(self, res, curr, count);
        },
        dialogRenderCallback: function(self, data, layero, index) {
            //console.log(self, data, layero, index);
        },
        dialogCloseCallback: function(self, data, layero, index) {
            //console.log(self, data, layero, index);
        },
        postParamCallback: function() {
            //return {};
        },
        postFormCallback: function(self, data) {
            //console.log(self, data);
        },
        deleteCallback: function(self, data) {
            //console.log(self, data);
        },
        tableConfig: {
            //height: 500,
            //page: true
        },
        dialogConfig: {
            width: null,
            height: null
        }
    };

    var obj = {
        dialogIndex: null,
        dialogObj: null,
        tableObj: null,
        tableOption: null,
        init: function(param) {
            var self = this;
            layer.closeAll();
            self.renderTable(param);
            self.renderAction();
        },
        renderTable: function(param) {
            var self = this;
            var tObj = {
                elem: '#' + self.id.table,
                height: self.tableHeight,
                cols: self.cols,
                url: self.url.table,
                done: function(res, curr, count) {
                    self.clearForm();
                    self.tableDoneCallback(self, res, curr, count);
                },
                page: true,
                where: param
            };
            self.tableOption = $.extend({}, tObj, self.tableConfig);
            self.tableObj = table.render(self.tableOption);
        },
        renderAction: function() {
            var self = this;

            if (self.id.add !== '') {
                $("#" + self.id.add).unbind("click").on("click",
                    function() {
                        self.clearForm();
                        self.renderDialog();
                    });
            }

            if (self.id.edit !== '') {
                $("#" + self.id.edit).unbind("click").on("click",
                    function() {
                        var rows = self.getRowIds();
                        if (rows.length == 0) {
                            dj.warn("请选择编辑的行！");
                        } else if (rows.length > 1) {
                            dj.warn("请选择一行！");
                        } else {
                            dj.get(self.url.getForm,
                                rows[0],
                                function(data) {
                                    self.renderDialog(data);
                                });
                        }
                    });
            }

            if (self.id.delete !== '') {
                $("#" + self.id.delete).unbind("click").on("click",
                    function() {
                        var rows = self.getRowIds();
                        if (rows.length == 0) {
                            dj.warn("请选择删除的行！");
                        } else {
                            dj.confirm("确定删除吗？", function() {
                                dj.post(self.url.delete, rows, function(data) {
                                    dj.success("删除成功");
                                    self.reload();
                                    self.deleteCallback.call(new Object(), self, data);
                                });
                            });
                        }
                    });
            }

            if (self.id.dialog !== '') {
                $("#" + self.id.dialog + " form").unbind('submit').on("submit",
                    function() {
                        var fields = $(this).serializeArray();
                        var fObj = {};
                        $.each(fields,
                            function(i, field) {
                                fObj[field.name] = field.value;
                            });

                        $.extend(fObj, self.postParamCallback.call(new Object()));

                        dj.post(self.url.postForm, fObj, function(data) {
                            layer.close(self.dialogIndex);
                            self.dialogObj.content.hide();
                            dj.success("保存成功");
                            self.reload();
                            self.postFormCallback.call(new Object(), self, data);
                        });
                        return false;
                    });
            }
        },
        renderDialog: function(data) {
            var self = this;
            var width = self.dialogConfig.width || ($(document).width() / 2);
            var height = self.dialogConfig.height || ($(document).height() / 2);
            var dObj = {
                type: 1,
                content: $('#' + self.id.dialog),
                area: [width + 'px', height + 'px'],
                zIndex: 1000,
                cancel: function(index) {
                    layer.close(index);
                    this.content.hide();
                    self.dialogCloseCallback.call(new Object(), self, data, layer, index);
                },
                success: function(layero, index) {
                    self.dialogIndex = index;
                    self.clearForm();
                    self.dialogRenderCallback.call(new Object(), self, data, layero, index);
                    self.setValue(data);

                    var obj = {
                        layero: layero,
                        index: index,
                        t: this
                    };
                    dj.escClose(obj);
                }
            };
            self.dialogObj = $.extend({}, dObj, self.dialogConfig);
            layer.open(self.dialogObj);
        },
        getRowIds: function() {
            var self = this;
            var checkStatus = table.checkStatus(self.id.table);
            var ids = [];
            $.each(checkStatus.data,
                function(i, item) {
                    ids.push(item);
                });
            return ids;
        },
        reload: function() {
            var self = this;
            self.tableObj.reload(self.tableOption);
        },
        setValue: function(jsonObject) {
            dj.setFormValue(jsonObject);
        },
        clearForm: function() {
            var self = this;
            if (self.id.dialog != '') {
                $('#' + self.id.dialog + " form").clearForm();
            }
        }
    };

    var opt = $.extend({}, defaultOption, obj, option);
    return opt;
};