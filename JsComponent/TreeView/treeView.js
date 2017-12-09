var treeViewObj = {};
$(function () {
    treeViewObj = {
        option: {
            id: 'selectTreeView',
            url: '',
            width: 350,
            height: 400,
            callback: function (treeNode) { console.log(treeNode); },
            dialogId: 'dialogTreeview',
            treeViewId: 'treeView',
            zIndex: 20000
        },
        dialogObj: null,
        zTreeObj: null,
        init: function (opt) {
            var self = this;
            self.option = $.extend({}, self.option, opt);

            //
            var dialogObj = $("#" + self.option.dialogId);
            if (dialogObj.length === 0) {
                $("body").append('<div id="' + self.option.dialogId + '"><div id="' + self.option.treeViewId + '" class="ztree"></div></div>');
                dialogObj = $("#" + self.option.dialogId);
            }

            var height = self.option.height;
            $("#" + self.option.id).on("click", function () {
                self.dialogObj = dialog({
                    content: dialogObj,
                    width: self.option.width,
                    height: height,
                    zIndex: self.option.zIndex,
                    quickClose: true// 点击空白处快速关闭
                });
                self.dialogObj.show(document.getElementById(self.option.id));

                dialogObj.css({ "height": height, "overflow": "auto" });
                self.treeview();
            });
        },
        treeview: function () {
            var self = this;
            var setting = {
                view: {
                    dblClickExpand: false,
                    showLine: true,
                    selectedMulti: false
                },
                callback: {
                    onClick: function (event, treeId, treeNode) {
                        self.dialogObj.close().remove();
                        self.option.callback.call(new Object(), treeNode);
                    }
                },
                data: {
                    simpleData: {
                        enable: true,
                        idKey: "id",
                        pIdKey: "pId",
                        rootPId: 0
                    }
                }
            };
            $.get(self.option.url, function (nodes) {
                self.zTreeObj = $.fn.zTree.init($("#" + self.option.treeViewId), setting, nodes.data);
            });
        }
    };
});