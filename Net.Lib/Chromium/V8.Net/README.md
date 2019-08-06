# V8.Net & V8引擎

- (解析简单的js文件)：[v8dotnet](https://github.com/rjamesnw/v8dotnet) / [Nuget](https://www.nuget.org/packages/V8.Net/)

> 安装：

- Install-Package V8.Net
- 安装后根目录文件（V8_Net_Proxy_x64.dll、V8_Net_Proxy_x86.dll）属性设置为生成复制。

```C#
var v8Engine = new V8Engine(); // (note: you can pass in "false" to create your own context)
v8Engine.Execute("function foo(s) { /* Some JavaScript Code Here */ return s; }", "My V8.NET Console");
Handle result = v8Engine.DynamicGlobalObject.foo("bar!");
Console.WriteLine(result.AsString); // (or cast using "(string)result")
Console.WriteLine("Press any key to continue ...");
Console.ReadKey();
```
