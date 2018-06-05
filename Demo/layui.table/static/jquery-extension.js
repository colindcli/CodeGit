$(function () {
    $.extend({
        tableObjec: function (option) {
            var tableObj = {
                tableId: '',
                tableOption: {
                    url: '',
                    page: false,
                    where: {},
                    cols: [[]]
                },
                menuItem: null,
                init: function () {
                    var self = this;
                    $.extend(self, option);
                    var obj = $("#" + self.tableId).parent();
                    if (!obj.hasClass(self.tableId)) {
                        obj.addClass(self.tableId);
                    }
                    if (!$('#' + self.tableId).attr('lay-filter')) {
                        $('#' + self.tableId).attr('lay-filter', self.tableId);
                    }
                    self.render();
                    self.sort();
                },
                render: function () {
                    var self = this;
                    var table = layui.table;
                    var height = $('#' + self.tableId).parent().height();
                    var opt = {
                        elem: '#' + self.tableId,
                        height: height,
                        done: function (res, curr, count) {
                            self.menu();
                            self.fixbug();
                        }
                    };
                    $.extend(opt, self.tableOption);
                    table.render(opt);
                },
                menu: function () {
                    var self = this;
                    if (!$.isEmptyObject(self.menuItem)) {
                        $.each(self.menuItem, function (i, item) {
                            var fn = item.callback;
                            item.callback = function (key, opt) {
                                self.openDialog(key, opt);
                                fn.call(new Object(),key, opt, this);
                            };
                        })
                        $.contextMenu({
                            selector: '.' + self.tableId + ' .layui-table-body table tr',
                            items: self.menuItem
                        });
                    }
                },
                openDialog: function (key, opt) {
                    console.log(key, opt)
                },
                sort: function () {
                    var self = this;
                    var table = layui.table;
                    var filterName = $('#' + self.tableId).attr('lay-filter');
                    table.on('sort(' + filterName + ')', function (obj) {
                        var where = $.extend({}, self.tableOption.where || {}, {
                            field: obj.field,
                            order: obj.type
                        });
                        table.reload(self.tableId, {
                            initSort: obj,
                            where: where
                        });
                    });
                },
                fixbug: function () {
                    var self = this;
                    var obj = $('.' + self.tableId + ' .layui-table-body').first();
                    obj.css({ 'overflow': 'hidden' });
                    setTimeout(function () {
                        obj.css({ 'overflow': 'auto' });
                    }, 0)
                }
            }
            tableObj.init();
        }
    });
});
