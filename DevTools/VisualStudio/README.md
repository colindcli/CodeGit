## VS 设置

- CurrentSettings.vssettings 工具-选项-导入和导出设置

## Visual Studio 扩展

- [marketplace visualstudio](https://marketplace.visualstudio.com/)

- AnkhSVN
- 多选编辑：Select Next Occurrence 快捷键搜索 SelectNextOccurrence（选择下一个：Alt+D；全选：Alt+F3）
- Resharper
- 调试：Debug Attach Manager
- SQL Prompt Core: sql 智能感知
- 注释切换：Toggle Comment
- 关闭右边选项卡：Close Tabs To Right
- 常用快捷键：Hot Commands
- 生成代码段：Snippet Designer 系统目录：C:\Users\Administrator\Documents\Visual Studio 2017\Code Snippets
- 自动保存：Autosave2017
- 自动刷新：Browser Reload on Save
- 枚举生成器：Enum Case Generator
- 选中文字高亮：Highlight all occurrences of selected word++ （编辑》Selected Highlight++，选择第 3 行第 7 个棕色）
- 底部显示文件路径：File Path On Footer
- Dll 版本自增长：Auto Version Incrementer
- 安装：Microsoft Visual Studio 2017 Installer Projects

- 根据方法的返回对象生成对象字段：Mapping Generator 映射生成对象
- xeam Project Rename Tool VS 2013 项目命名
- Fix Namespace 修复命名空间
- Regionizer 2.1 with Auto Commenting

- Visual-Studio-jQuery-Code-Snippets
- BuildOnSave
- Format All Files
- Color Theme Editor
- OzCode
- Web Essentials
- Multi Edit Mode
- Clean Solution

## VS2017

- 默认不显示代码块竖线: 工具=>选项=>文本编辑器=>显示结构参考线
- 修改 cs 文件模板: D:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\ItemTemplates\CSharp\Code\2052\Class
- Resharper 取消 Ctrl+左键跳转：Environment - Search&Navigation - Rich mouse navigation in the editor)
- Resharper 取消变量类型注释：Environment - Editor - Inlay Hints - Show Inlay Hints 取消勾选
- 取消 VS Ctrl+点击: 选项 - 文本编辑器 - 常规 - 使用鼠标单击可执行转到定义
- 禁用 VS 警告：项目右键属性 - 生成 - “取消警告(S)”填写“1591”即可 （如：1591 是缺少对公共可见类型或成员“\*\*\*”的 XML 注释）
- 设置文档格式快捷键：选项 - 环境 - 键盘，搜索 CleanupCode，设置快捷键 Alt+Shit+F。或者 Resharper 设置保存时自动格式：Options - Code Editing - Code Cleanup - General，勾选 Save after...和 Autom...和 Only in...

## Release 生成时，排除文件，打开 csproj

```xml
<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <!-- 不复制pdb文件 -->
    <DebugType>none</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <!-- 阻止默认的 XML 和 PDB 文件复制到 RELEASE 的输出目录. 如*.dll、*.exe.config、*.exe 扩展名的文件可以被复制-->
    <AllowedReferenceRelatedFileExtensions>
    .dll
    .exe.config
    .exe
    </AllowedReferenceRelatedFileExtensions>
</PropertyGroup>
```
