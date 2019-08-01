# CefSharp

> https://github.com/cefsharp/CefSharp


> 创建一个项目：

- 创建winform，引用CefSharp.dll、CefSharp.Core.dll、CefSharp.WinForms.dll。

- 复制文件到bin目录，文件参考图片cefSharp.jpg，文件在csdn下载。

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
