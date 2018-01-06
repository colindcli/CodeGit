var select2Obj = {};
$(function() {
    //https://select2.org/
    select2Obj = {
        select: function(id, selectedId, width, multiple) {
            if (!multiple) {
                $('#' + id).prepend('<option disabled selected value="0">请选择</option>');
            }
            $('#' + id).select2({
                dropdownAutoWidth: true,
                multiple: multiple || false,
                width: width || 200,
                placeholder: '请选择',
                //allowClear: true
            });
            if (selectedId != null) {
                $('#' + id).val(selectedId).change();
            }
        },
        select2: function(id, data, selectedId, width, multiple) {
            if (!multiple) {
                $('#' + id).html('<option disabled selected value="0">请选择</option>');
            }
            $('#' + id).select2({
                data: data,
                dropdownAutoWidth: true,
                multiple: multiple || false,
                width: width || 200,
                placeholder: '请选择',
                //allowClear: true,
            });
            if (selectedId != null) {
                $('#' + id).val(selectedId).change();
            }
        },
        //?search=[term]&page=[page]; 设置默认值需要在select添加行：<option id="0">selected</option>
        selectAjax: function(id, url, selectedId, selectedName, width, multiple) {
            if (selectedId != null && selectedName != null) {
                $('#' + id).html('<option value="' + selectedId + '">' + selectedName + '</option>');
            }
            var obj = $('#' + id).select2({
                dropdownAutoWidth: true,
                multiple: multiple || false,
                width: width || 200,
                placeholder: '请选择',
                //allowClear: true,
                ajax: {
                    url: url,
                    data: function(params) {
                        var query = {
                            search: params.term,
                            page: params.page || 1
                        }
                        return query;
                    }
                }
            });
            return obj;
        }
    };
});