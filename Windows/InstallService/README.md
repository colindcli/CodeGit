## exe安装为服务

- [doc](README.png) / [doc](https://blog.csdn.net/github_39376455/article/details/78882600)

- 下载安装文件：(InstallService)(http://dl.fzxgj.top/Files/Server/InstallService.zip)

- 放置到目录如：C:\InstallService

- 管理员运行cmd：C:\InstallService\instsrv.exe IdeaRegisterServer C:\InstallService\srvany.exe

- 开始 - 运行(或按下键盘上的Windows+R)输入regedit,点击确定或按回车,可以打开注册表编辑器。

- 然后进入注册表在HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services下找到刚刚注册的服务名IdeaRegisterServer，在IdeaRegisterServer新建一个项，名称为“Parameters”

- 单击选中它然后在右侧的窗口新建一个字符串值名称为“Application”，将其值设置为你针要做为服务运行的程序的路径，例如我的路径为“C:\InstallService\IntelliJIDEALicenseServer_windows_amd64.exe”。

- 然后可以再建立一个AppDirectory指定程序运行的初始目录C:\InstallService



## 注册服务软件

- [添加删除服务工具](http://dl.fzxgj.top/Files/Windows/%E6%9C%8D%E5%8A%A1%E7%AE%A1%E7%90%86%E5%B7%A5%E5%85%B7.zip)


## bat转exe

- [bat转exe](http://dl.fzxgj.top/Files/Windows/bat%E8%BD%ACexe.zip)