## .Net4.6版本在.Net4.5服务器运行

- 打开nuget,然后卸载Microsoft.CodeDom.Providers.DotNetCompilerPlatform 包
- Web.csproj里删除一下两行xml代码

``` xml
<Import Project="$(AppData)\Packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.0\build\net46\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props" Condition="Exists('$(AppData)\Packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.0\build\net46\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')" />

<Target Name="EnsureNuGetPackageBuildImports" BeforeTargets="PrepareForBuild">
    <PropertyGroup>
      <ErrorText>这台计算机上缺少此项目引用的 NuGet 程序包。使用“NuGet 程序包还原”可下载这些程序包。有关更多信息，请参见 http://go.microsoft.com/fwlink/?LinkID=322105。缺少的文件是 {0}。</ErrorText>
    </PropertyGroup>
    <Error Condition="!Exists('$(AppData)\Packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.0\build\net46\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props')" Text="$([System.String]::Format('$(ErrorText)', '$(AppData)\Packages\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.2.0.0\build\net46\Microsoft.CodeDom.Providers.DotNetCompilerPlatform.props'))" />
    <Error Condition="!Exists('$(AppData)\Packages\Microsoft.Net.Compilers.3.1.0\build\Microsoft.Net.Compilers.props')" Text="$([System.String]::Format('$(ErrorText)', '$(AppData)\Packages\Microsoft.Net.Compilers.3.1.0\build\Microsoft.Net.Compilers.props'))" />
    <Error Condition="!Exists('$(AppData)\Packages\RazorGenerator.MsBuild.2.5.0\build\RazorGenerator.MsBuild.targets')" Text="$([System.String]::Format('$(ErrorText)', '$(AppData)\Packages\RazorGenerator.MsBuild.2.5.0\build\RazorGenerator.MsBuild.targets'))" />
  </Target>
```