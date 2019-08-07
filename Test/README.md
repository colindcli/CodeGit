# Selenium测试

> Selenium: https://github.com/SeleniumHQ

- [github](https://github.com/SeleniumHQ/selenium)

> Chrome浏览器测试插件

- [Selenium IDE](https://chrome.google.com/webstore/detail/selenium-ide/mooikfkahbdckldjjndioackbalphokd)
- [Github](https://github.com/seleniumhq/selenium-ide)
- [命令行文档](https://www.seleniumhq.org/selenium-ide/docs/en/api/commands/)

> C#调用浏览器测试

- 官方网站：https://www.seleniumhq.org/download/（Selenium Client & WebDriver Language Bindings的C#）
- NuGet安装：WebDriver、WebDriverBackedSelenium、Support
- C# Code：

```C#
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;

var driver = new ChromeDriver(new ChromeOptions());
driver.Url = "https://www.baidu.com/";
driver.Manage().Window.Size = new Size(1920, 1080);
//driver.FindElement(By.CssSelector(".kw")).SendKeys("搜索");
driver.FindElement(By.Id("kw")).SendKeys("搜索");
driver.FindElement(By.Id("su")).Click();
```

```C#
var driver = new ChromeDriver(new ChromeOptions());
var selenium = new WebDriverBackedSelenium(driver, "https://www.baidu.com/");
selenium.Start();
selenium.Open("/");
selenium.Type("css=#kw", "key"); //https://www.seleniumhq.org/docs/09_selenium_ide.jsp#locating-elements
```
