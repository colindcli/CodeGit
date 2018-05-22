## Browser Reload on Save

1、下载安装：https://marketplace.visualstudio.com/items?itemName=MadsKristensen.BrowserReloadonSave

2、配置：
web.config配置：
<appSettings>
  <add key="vs:EnableBrowserLink" value="false"/>
</appSettings>

<system.web>
  <compilation debug="false" targetFramework="4.5" />
</system.web>
