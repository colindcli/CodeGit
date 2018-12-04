## T4模板

> 安装NuGet

- https://www.nuget.org/packages/Microsoft.VisualStudio.TextTemplating.15.0/
- https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp/


> 引进命名空间

<#@ import namespace="System.Collections.Generic" #>
<#@ assembly name="GenerateCodeByT4.exe" #>
<#@ import namespace="GenerateCodeByT4.Entity" #>
<#@ parameter type="System.Collections.Generic.List<GenerateCodeByT4.Entity.ColumnModel>" #>


> 引进dapper.dll

<#@ assembly name="$(SolutionDir)\packages\Dapper.1.50.5\lib\net451\Dapper.dll" #>
<#@ import namespace="Dapper" #>