//https://www.nuget.org/packages/Selenium.Support/ (3.141.0)
//https://www.nuget.org/packages/Selenium.WebDriverBackedSelenium/ (3.141.0)
//https://chromedriver.storage.googleapis.com/index.html?path=2.46/（下载2.46文件放到bin目录）
//chrome 版本 72.0.3626.119（正式版本） （64 位）

//版本对应表：https://sites.google.com/a/chromium.org/chromedriver/downloads

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Screenshot.DataAccess;

//网页截图
internal class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="savePath"></param>
    /// <param name="report"></param>
    private bool Run(string url, string savePath, Action<string, ConsoleColor> report)
    {
        try
        {
            var dir = $"{AppDomain.CurrentDomain.BaseDirectory}Files";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var fileName = $"{dir}/{Guid.NewGuid()}";

            var driver = new ChromeDriver();

            var win = driver.Manage().Window;

            // LogHelper.Debug($"设置前窗口大小：{win.Size.Width} {win.Size.Height}");

            win.Size = new Size(1971, 1080);

            // LogHelper.Debug($"设置后窗口大小：{win.Size.Width} {win.Size.Height}");

            driver.Navigate().GoToUrl(url);
            var heightStr = driver.ExecuteJavaScript<object>("return document.documentElement.scrollHeight+\"|\"+document.documentElement.clientHeight+\"|\"+document.documentElement.clientWidth").ToString();

            var obj = heightStr.Split('|');
            var scrollHeight = int.Parse(obj[0]);
            var clientHeight = int.Parse(obj[1]);
            var clientWidth = int.Parse(obj[2]);

            var pageSize = scrollHeight / clientHeight;

            // LogHelper.Debug($"Js获取窗口大小：{clientWidth}  {scrollHeight}  {clientHeight}");

            int index = 0;
            for (; index < pageSize; index++)
            {
                driver.ExecuteScript($"window.scrollTo(0,{clientHeight * index})");
                //Thread.Sleep(500);
                driver.ExecuteScript("var items=document.querySelectorAll('*');var array=[];for(var i=0;i<items.length;i++){var name=items[i].tagName.toLocaleLowerCase();var flag=false;for(var j=0;j<array.length;j++){if(array[j]===name){flag=true;break}}if(!flag){array.push(name)}}for(var i=0;i<array.length;i++){var tagName=array[i];var rows=document.getElementsByTagName(tagName);for(var j=0;j<rows.length;j++){var pt=window.getComputedStyle(rows[j],null).position;if(pt==='fixed'){rows[j].style.setProperty('position','absolute','important');rows[j].style.setProperty('top','','important');rows[j].style.setProperty('bottom','','important');rows[j].style.setProperty('left','','important');rows[j].style.setProperty('right','','important')}}};");
                //Thread.Sleep(5000);
                driver.GetScreenshot().SaveAsFile($@"{fileName}{index}.png", ScreenshotImageFormat.Png);
            }

            if (scrollHeight % clientHeight > 0)
            {
                driver.ExecuteScript($"window.scrollTo(0,{scrollHeight})");
                driver.GetScreenshot().SaveAsFile($@"{fileName}{index}.png", ScreenshotImageFormat.Png);
            }
            driver.Close();
            driver.Dispose();

            var bmp = new Bitmap(clientWidth - (scrollHeight % clientHeight > 0 ? 18 : 0), scrollHeight);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            var i = 0;
            for (; i < index; i++)
            {
                var img = Image.FromFile($@"{fileName}{i}.png");
                g.DrawImage(img, 0, i * clientHeight, clientWidth, clientHeight);
                img.Dispose();
            }

            if (scrollHeight % clientHeight > 0)
            {
                var img = Image.FromFile($@"{fileName}{i}.png");
                g.DrawImage(img, 0, scrollHeight - clientHeight, clientWidth, clientHeight);


                img.Dispose();
            }

            bmp.Save($@"{savePath}", ImageFormat.Png);

            g.Dispose();
            bmp.Dispose();


            for (var j = 0; j <= i; j++)
            {
                File.Delete($@"{fileName}{j}.png");
            }

            return true;
        }
        catch (Exception ex)
        {
            report?.Invoke(ex.ToString(), ConsoleColor.Red);
            // LogHelper.Fatal(ex.Message, ex);
            return false;
        }
    }
}
