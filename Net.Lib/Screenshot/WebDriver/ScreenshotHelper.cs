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
    /// <param name="pageSource"></param>
    /// <param name="log"></param>
    public static bool Run(string url, string savePath, out string pageSource, Action<object, Exception> log = null)
    {
        pageSource = null;
        try
        {
            var dir = $"{AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\')}/Files";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var fileName = $"{dir}/{Guid.NewGuid()}";

            var option = new ChromeOptions();
            //option.AddArgument("--window-size=1920,1080");
            option.AddArgument("headless");
            var driver = new ChromeDriver(option);

            var win = driver.Manage().Window;

            log?.Invoke($"设置前窗口大小：{win.Size.Width} {win.Size.Height}", null);

            win.Size = new Size(1971, 1080);

            log?.Invoke($"设置后窗口大小：{win.Size.Width} {win.Size.Height}", null);

            driver.Navigate().GoToUrl(url);
            pageSource = driver.PageSource;
            var heightStr = driver.ExecuteJavaScript<object>("return document.documentElement.scrollHeight+\"|\"+document.documentElement.clientHeight+\"|\"+document.documentElement.clientWidth").ToString();

            var obj = heightStr.Split('|');
            var scrollHeight = int.Parse(obj[0]);
            var clientHeight = int.Parse(obj[1]);
            var clientWidth = int.Parse(obj[2]);

            var pageSize = scrollHeight / clientHeight;

            log?.Invoke($"Js获取窗口大小：{clientWidth}  {scrollHeight}  {clientHeight}", null);

            int index = 0;
            for (; index < pageSize; index++)
            {
                driver.ExecuteScript($"window.scrollTo(0,{clientHeight * index})");
                driver.ExecuteScript("var items=document.querySelectorAll('*');var array=[];for(var i=0;i<items.length;i++){var name=items[i].tagName.toLocaleLowerCase();var flag=false;for(var j=0;j<array.length;j++){if(array[j]===name){flag=true;break}}if(!flag){array.push(name)}}for(var i=0;i<array.length;i++){var tagName=array[i];var rows=document.getElementsByTagName(tagName);for(var j=0;j<rows.length;j++){var pt=window.getComputedStyle(rows[j],null).position;if(pt==='fixed'){rows[j].style.setProperty('position','absolute','important');rows[j].style.setProperty('top','','important');rows[j].style.setProperty('bottom','','important');rows[j].style.setProperty('left','','important');rows[j].style.setProperty('right','','important')}}};");

                // 通过截图文件MD5值判断是否加载完成
                var num = 0;
                var md5 = new List<string>();
                while (num < 50)
                {
                    var ph = $@"{fileName}{index}_{num}.jpg";
                    driver.GetScreenshot().SaveAsFile(ph, ScreenshotImageFormat.Jpeg);
                    var m5 = FileMd5(ph);
                    md5.Add(m5);
                    File.Delete(ph);

                    var c = md5.Count(p => p == m5);
                    if (c >= 3)
                    {
                        break;
                    }
                    num++;
                    Thread.Sleep(200);
                }

                driver.GetScreenshot().SaveAsFile($@"{fileName}{index}.jpg", ScreenshotImageFormat.Jpeg);
            }

            if (scrollHeight % clientHeight > 0)
            {
                driver.ExecuteScript($"window.scrollTo(0,{scrollHeight})");
                driver.GetScreenshot().SaveAsFile($@"{fileName}{index}.jpg", ScreenshotImageFormat.Jpeg);
            }
            driver.Close();
            driver.Dispose();

            var bmp = new Bitmap(clientWidth - (scrollHeight % clientHeight > 0 ? 18 : 0), scrollHeight);
            var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            var i = 0;
            for (; i < index; i++)
            {
                var img = Image.FromFile($@"{fileName}{i}.jpg");
                g.DrawImage(img, 0, i * clientHeight, clientWidth, clientHeight);
                img.Dispose();
            }

            if (scrollHeight % clientHeight > 0)
            {
                var img = Image.FromFile($@"{fileName}{i}.jpg");
                g.DrawImage(img, 0, scrollHeight - clientHeight, clientWidth, clientHeight);


                img.Dispose();
            }

            bmp.Save($@"{savePath}", ImageFormat.Jpeg);

            g.Dispose();
            bmp.Dispose();


            for (var j = 0; j <= i; j++)
            {
                File.Delete($@"{fileName}{j}.jpg");
            }

            return true;
        }
        catch (Exception ex)
        {
            log?.Invoke(ex.Message, ex);
            return false;
        }
    }

    /// <summary>
    /// 判读文件MD5值
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static string FileMd5(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }
        try
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] retVal;
            lock (fileName)
            {
                var file = new FileStream(fileName, FileMode.Open);
                retVal = md5.ComputeHash(file);
                file.Close();
            }
            var sb = new StringBuilder();
            foreach (var t in retVal)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }
}
