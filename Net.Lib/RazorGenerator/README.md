## 编译视图文件为dll （项目发布后可以删除Views文件夹，能正常运行）

安装：
https://www.nuget.org/packages/RazorGenerator.Mvc/ （Install-Package RazorGenerator.Mvc -Version 2.4.9）
https://www.nuget.org/packages/RazorGenerator.MsBuild/  （Install-Package RazorGenerator.MsBuild -Version 2.5.0）


> 说明

- RazorGenerator.Mvc方式：需要手动一个一个修改视图文件【.cshtml】的属性：生成操作-无；自定义工具-RazorGenerator，该视图文件子项下出现视图类文件【视图文件名+.generated.cs】。
- RazorGenerator.MsBuild方式：不用修改视图文件属性，在创建视图时直接生成，视图类文件不在视图子项下，而在项目目录的\obj\CodeGen下。可用于创建视图类库。


> 其他相关

- [使用RazorGenerator实现项目模块分离](https://www.cnblogs.com/xiumukediao/p/5989793.html)
- [编译你的asp.net mvc Razor视图](http://blog.sina.com.cn/s/blog_63c151240102wbg3.html)