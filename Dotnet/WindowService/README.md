> ASP.NET Window服务

- 创建 Windows服务 文件HostService.cs
- HostService.cs设计模式下，右键 添加安装程序 ProjectInstaller.cs
- ProjectInstaller.cs设计模式下：serviceProcessInstaller1 属性Account设为LocalSystem；serviceInstaller1设置属性Discription、DisplayName、ServiceName，StartType设为Automatic
- 以管理员身份运行InstallService.bat或UnInstallService.bat
