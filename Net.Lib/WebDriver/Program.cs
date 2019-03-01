//https://www.nuget.org/packages/Selenium.Support/ (3.141.0)
//https://www.nuget.org/packages/Selenium.WebDriverBackedSelenium/ (3.141.0)
//https://chromedriver.storage.googleapis.com/index.html?path=2.46/（下载2.46文件放到bin目录）
//chrome 版本 72.0.3626.119（正式版本） （64 位）

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

//截图
namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("请输入网址：");
            //var url = "https://blog.nuget.org/";
            var url = Console.ReadLine()?.Replace("请输入网址：", "");

            var guid = Guid.NewGuid().ToString();
            var fileName = $"{AppDomain.CurrentDomain.BaseDirectory}{guid}";

            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);

            var heightStr = driver.ExecuteJavaScript<object>("return document.documentElement.scrollHeight+\"|\"+document.documentElement.clientHeight+\"|\"+document.documentElement.clientWidth").ToString();


            var obj = heightStr.Split('|');
            var scrollHeight = int.Parse(obj[0]);
            var clientHeight = int.Parse(obj[1]);
            var clientWidth = int.Parse(obj[2]);

            var pageSize = scrollHeight / clientHeight;
            int index = 0;
            for (; index < pageSize; index++)
            {
                driver.ExecuteScript($"window.scrollTo(0,{clientHeight * index})");
                driver.GetScreenshot().SaveAsFile($@"{fileName}{index}.png", ScreenshotImageFormat.Png);
            }

            if (scrollHeight % clientHeight > 0)
            {
                driver.ExecuteScript($"window.scrollTo(0,{scrollHeight})");
                driver.GetScreenshot().SaveAsFile($@"{fileName}{index}.png", ScreenshotImageFormat.Png);
            }


            var bmp = new Bitmap(clientWidth - (scrollHeight % clientHeight > 0 ? 18 : 0), clientHeight * (index + 1));
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

            bmp.Save($@"{fileName}.png", ImageFormat.Png);

            g.Dispose();
            bmp.Dispose();


            for (var j = 0; j <= i; j++)
            {
                File.Delete($@"{fileName}{j}.png");
            }

            Console.WriteLine(guid);
        }
    }
}
