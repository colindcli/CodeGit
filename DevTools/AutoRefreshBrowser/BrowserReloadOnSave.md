## Browser Reload on Save

1、安装插件：
- 自动刷新浏览器：[BrowserReloadonSave](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.BrowserReloadonSave)
- 自动保存和自动编译：[Autosave2017](https://marketplace.visualstudio.com/items?itemName=fluffyerug.Autosave2017)

2、配置：
web.config配置：
<appSettings>
  <add key="vs:EnableBrowserLink" value="false"/>
</appSettings>

<system.web>
  <compilation debug="false" targetFramework="4.5" />
</system.web>

3、iis运行时配置：Web项目 > 属性 > Web > 服务器(本地IIS：项目url输入网址)