## 7z压缩

- 安装 SevenZipSharp.Interop

``` c#
static void Main(string[] args)
{
    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\'), Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
    SevenZipBase.SetLibraryPath(path);

    var zip = new SevenZipCompressor
    {
        CompressionLevel = CompressionLevel.Ultra
    };
    zip.CompressFiles(@"E:\1.7z", new[] { @"E:\1.bak" });
}
```