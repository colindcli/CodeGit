//http://www.au92.com/archives/jquery-set-value-to-form.html

var setValue = function(name, val){
    if(val != ""){
        var htmlType = $("[name='"+name+"']").attr("type");
        if(htmlType == "text" || htmlType == "textarea" || htmlType == "select-one" || htmlType == "hidden" || htmlType == "button"){
            $("[name='"+name+"']").val(val);
        }else if(htmlType == "radio"){
            $("input[type=radio][name='"+name+"'][value='"+val+"']").attr("checked",true);
        }else if(htmlType == "checkbox"){
            var vals = val.split(",");
            for(var i=0; i < vals.length; i++){
                $("input[type=checkbox][name='"+name+"'][value='"+vals[i]+"']").attr("checked",true);
            }
        }
    }
};


//https://zhidao.baidu.com/question/410473862.html

(function ($) {
        $.fn.setform = function (jsonValue) {
            var obj = this;
            $.each(jsonValue, function (name, ival) {
                var $oinput = obj.find("input:[name=" + name + "]");
                if ($oinput.attr("type")== "radio" || $oinput.attr("type")== "checkbox") {
                   $oinput.each(function(){
                     if($(this).val()==ival)
                        $(this).attr("checked", "checked");
                   });
                }
                else
                {
                  obj.find("[name="+name+"]").val(ival); 
                 }
            });
        }
    })(jQuery)
