## SVN设置

- 刷新解决方案树状态: 选项 - 环境 - 键盘 搜索：SvnRefreshStatus 设置ctrl+K,ctrl+Z


**SVN图标不显示问题**

> 1、VS2017图标：选项 》Source Control 》当前插件选择Svn
> 2、Win10文件夹图标：SVN Setting 》Icon Overlays 》Icon Set 》Win10
> 3、排除不提交文件夹：.vs packages temp .vscode .git dist node_modules aspnet_client PublishProfiles bin obj Logs Files *.o *.lo *.la *.al .libs *.so *.so.[0-9]* *.a *.pyc *.pyo __pycache__ *.rej *~ #*# .#* .*.swp .DS_Store [Tt]humbs.db

参考：
http://blog.csdn.net/aggio/article/details/79095166
https://www.cnblogs.com/jinzesudawei/p/8029084.html