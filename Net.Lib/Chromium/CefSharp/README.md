# CefSharp

> https://github.com/cefsharp/CefSharp


> 创建一个项目：

- 创建winform，引用CefSharp.dll、CefSharp.Core.dll、CefSharp.WinForms.dll、CefSharp.BrowserSubprocess.Core.dll。

- 复制文件到bin目录，文件参考图片cefSharp.jpg，文件在csdn下载：https://download.csdn.net/download/winsty2008/11460255。

- 初始化，调用InitBrowser()方法

```c#
public ChromiumWebBrowser Browser;
public void InitBrowser()
{
    Cef.Initialize(new CefSettings());
    Browser = new ChromiumWebBrowser("https://www.baidu.com/");
    this.Controls.Add(Browser);
    Browser.Dock = DockStyle.Fill;
}
```


> 报错：CefSharp.Common contains unmanaged resoures, set your project and solution platform to x86 or x64. Alternatively for AnyCPU Support see https://github.com/cefsharp/CefSharp/issues/1714	SharpBrowser

1、在csproj文件的PropertyGroup节点下第一行增加一个配置项：

```xml
<CefSharpAnyCpuSupport>true</CefSharpAnyCpuSupport>
```

2、App.config的configuration下增加一个配置：

```xml
<runtime>
<assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
    <probing privatePath="x86"/>
</assemblyBinding>
</runtime>
```

3、启动配置

```C#
static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var settings = new CefSettings
        {
            BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe"
        };
        Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
        var browser = new MainForm();
        Application.Run(browser);
    }
}
```
