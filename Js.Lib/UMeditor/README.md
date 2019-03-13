## UMeditor

- [下载](http://ueditor.baidu.com/website/download.html#mini) / [github](https://github.com/fex-team/umeditor)

> 引用 (需jquery.js)

- /themes/default/css/umeditor.min.css
- /umeditor.config.js
- /umeditor.min.js
- /lang/zh-cn/zh-cn.js


> html

    <!-- 加载编辑器的容器 -->
    <script id="ArticleContent" name="ArticleContent" type="text/plain">@(Html.Raw(Model.ArticleContent))</script>
    
> js

    editorObj.init('ArticleContent');
    
> 动态设置编辑器内容
    
    editorObj.setContent('初始化内容！');
    
> 上传附件

    //图片上传配置区
    , imageUrl: URL +"../../File/EditorUpload"             //图片上传提交地址
    , imagePath:URL + "../../"                         //图片修正地址，引用了fixedImagePath,如有特殊需求，可自行配置
    
> 上传后返回对象：[FileReturnModel.cs](https://github.com/colindcli/CodeGit/blob/master/Js.Lib/UMeditor/FileReturnModel.cs)



## bug修复

- umeditor1_2_2-utf8-net：修复两端对齐，justifyjustify替换为justifyfull



## 百度地图生成器

- http://api.map.baidu.com/lbsapi/createmap/index.html