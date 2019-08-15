# Excel转换: DwrExcel


> 导入(推荐)：

- Npoi.Mapper：[Npoi.Mapper](https://www.nuget.org/packages/Npoi.Mapper/) / [doc](http://donnytian.github.io/Npoi.Mapper/) [参考:ImportHelper.cs]


> 导出(推荐)：

- Npoi:[NPOI](https://github.com/dotnetcore/NPOI) 参考：ExportHelper.cs


>其他：

- 读写Excel：[NPOI](https://github.com/dotnetcore/NPOI)
- 读取Excel：[ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader)/[ExcelDataReader.DataSet](https://www.nuget.org/packages/ExcelDataReader.DataSet/) / [demo](https://github.com/ExcelDataReader/ExcelDataReader)
- EPPlus: [EPPlus](https://github.com/JanKallman/EPPlus) / [nuget](https://www.nuget.org/packages/EPPlus/)
- Excel与Object互转：[Excel2Object](https://github.com/chsword/Excel2Object)
- 文件转数据对象：[FileHelpers](https://github.com/MarcosMeli/FileHelpers)



# 问题

> 安全透明方法“NPOI.OpenXml4Net.OPC.ZipPackage..ctor(System.IO.Stream, NPOI.OpenXml4Net.OPC.PackageAccess)”尝试访问安全关键方法“ICSharpCode.SharpZipLib.Zip.ZipInputStream..ctor(System.IO.Stream)”失败。

程序集“NPOI.OpenXml4Net, Version=2.4.1.0, Culture=neutral, PublicKeyToken=0df73ec7942b34e1”标记为 AllowPartiallyTrustedCallersAttribute 并且使用 2 级安全透明模型。默认情况下，2 级透明将导致 AllowPartiallyTrustedCallers 程序集中的所有方法都变成安全透明的，这可能是导致发生此异常的原因。

- 解决：（卸载后重新引用NPOI和SharpZipLib）
- 检查packages.config文件引用的版本：NPOI的targetFramework版本比ICSharpCode.SharpZipLib的targetFramework版本低。
- 检查csproj的Reference引用的版本：..\packages\NPOI.2.4.1\lib\net40\NPOI.dll改：..\packages\NPOI.2.4.1\lib\net45\NPOI.dll（4条记录）



> sheet.CopyRow: System.NullReferenceException: 未将对象引用设置到对象的实例。在 NPOI.SS.Util.SheetUtil.CopyRow(ISheet sheet, Int32 sourceRowIndex, Int32 targetRowIndex)

- 解决：复制行时复制合并单元格的bug，NPOI bug版本2.4.1（https://github.com/dotnetcore/NPOI/issues/134），使用DwrExcel的CopyRowSample(1, 2)代替>>>>>>> .r253
