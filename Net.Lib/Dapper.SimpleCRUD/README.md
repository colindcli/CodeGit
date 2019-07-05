> Dapper.SimpleCRUD
- T4模板：https://www.nuget.org/packages/Dapper.SimpleCRUD.ModelGenerator/  （增加了列注释）
- 解决方案路径 Host.ResolveAssemblyReference("$(SolutionDir)");


> 安装

- 引用：System.ComponentModel.DataAnnotations
- Install-Package Microsoft.VisualStudio.TextTemplating.15.0 -Version 16.0.28727
- Install-Package EnvDTE -Version 8.0.2
- Install-Package EnvDTE100 -Version 10.0.3
- Install-Package System.ComponentModel.Annotations -Version 4.5.0


> App.config或Web.config必须用一下方式

``` C#
<connectionStrings>
<add name="DefaultConnectionString" connectionString="Data Source=.;uid=sa;pwd=123456;database=;" providerName="System.Data.SqlClient" />
</connectionStrings>
```


> 问题

- 正在运行转换: System.NullReferenceException: 未将对象引用设置到对象的实例: 调试T4看哪里出错(之前连接字符串写死报错)
