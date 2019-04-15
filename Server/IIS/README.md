## IIS

**禁用系统缓存**

输出缓存 - 添加- “*” 启用用户模式缓存（使用文件更改通知）、内核模式缓存（使用文件更改通知）


**安装IIS后问题及配置：**

控制面板 - 程序和功能 - 启动或关闭windows功能 - Internet Information services - 万维网服务 - 应用程序开发功能
    
    勾选：
    1、ASP.NET 3.5
    2、ASP.NET 4.6
    3、ISAPI扩展
    4、ISAPI筛选器
    5、.NET Extensibility 3.5
    6、.NET Extensibility 4.6
    
> 不能在此路径中使用此配置节。如果在父级别上锁定了该节，便会出现这种情况。锁定是默认设置的(overrideModeDefault="Deny")，或者是通过包含
> overrideMode="Deny" 或旧有的 allowOverride="false" 的位置标记明确设置的？？？


    C:\windows\system32\inetsrv\appcmd unlock config -section:system.webServer/handlers
    C:\windows\system32\inetsrv\appcmd unlock config -section:system.webServer/modules

    
> 处理程序“ExtensionlessUrlHandler-Integrated-4.0”在其模块列表中有一个错误模块“ManagedPipelineHandler”？？？

    32位机器： c:\windows\microsoft.net\framework\v4.0.30319\aspnet_regiis.exe -i
    64位机器： c:\windows\microsoft.net\framework64\v4.0.30319\aspnet_regiis.exe -i



## IIS反向代理

在Windows Server 2012 R2上 安装ARR，URL Rewriter组件。
ARR3.0需要如下组件支持：Web Farm Framework 2.2（该组件又需要Web Platform Installer 3.0 和 WebDeploy 2.0组件的支持）
URL Rewriter2.0（For IIS7.0，支持Win 2012 R2）直接安装即可。
下载地址：http://www.iis.net/downloads/microsoft/application-request-routing
　　　　      http://www.iis.net/downloads/microsoft/web-farm-framework
　　　　      http://www.microsoft.com/en-us/download/details.aspx?id=7435

- 启用ARR：在IIS左栏点击树的最顶级，双击Application Request Routing，然后在右侧点击server proxy settings，对其启用。
- 点击站点，选Url rewrite添加规则。

注意：编辑入站规则的模式匹配域名后半部分，如：`http://www.iis.net/downloads/microsoft/application-request-routing`，匹配 `downloads/microsoft/application-request-routing`




## 一台服务器IIS支持绑定多个HTTPS站点


> 方法一：

- 打开注册表：Win+R组合键打开运行，输入“regedit”。

- 找到注册表项：HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HTTP\Parameters\SslSniBindingInfo，将注册表值改为2。

- 重启IIS服务。


> 方法二：

- C:\Windows\system32\inetsrv\config\applicationHost.config、

- 默认一个站点带一个这样的配置：<binding protocol="https" bindingInformation="*:443" />
- 修改成：<binding protocol="https" bindingInformation="*:443:www.baidu.om" />

- 切记需要对应的每个站点都修改。