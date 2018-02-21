var editorObj = {
    id: '',
    obj: null,
    option: null,
    init: function (id, option) {
        var self = this;
        self.id = id;
        self.option = option;
        self.editor();
    },
    editor: function () {
        var self = this;
        self.obj = UM.getEditor(self.id, self.option);
    },
    setContent: function (content) {
        var self = this;
        self.obj.getEditor(self.id).setContent(content);
    },
    getContent: function () {
        var self = this;
        var v = self.obj.getEditor(self.id).getContent();
        return v;
    }
};
