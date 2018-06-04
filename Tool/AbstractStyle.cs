//Install-Package HtmlAgilityPack.CssSelectors -Version 1.0.2
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//提取行内样式
namespace AbstractStyle
{
    class Program
    {
        static void Main(string[] args)
        {
            //样式名前缀
            var classPreName = "absty_";

            var html = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "1.html");
            var outerHtml = AbstractStyleHandler(html, classPreName);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "out.html", outerHtml);
        }

        /// <summary>
        /// 提取行内样式
        /// </summary>
        /// <param name="html"></param>
        /// <param name="classPreName"></param>
        /// <returns></returns>
        private static string AbstractStyleHandler(string html, string classPreName)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var styles = htmlDoc.QuerySelectorAll("[style]");

            var i = 0;
            var sb = new StringBuilder();
            sb.AppendLine("<style>");
            var nvs = new List<KeyValuePair<string, string>>();
            foreach (var htmlNode in styles)
            {
                var style = htmlNode.Attributes["style"];
                var className = $"{classPreName}{i}";

                if (nvs.Exists(p => string.Equals(style.Value, p.Value, StringComparison.OrdinalIgnoreCase)))
                {
                    var obj = nvs.Find(p => string.Equals(style.Value, p.Value, StringComparison.OrdinalIgnoreCase));
                    className = obj.Key;
                }
                else
                {
                    sb.AppendLine($"body .{className}{{{style.Value}}}");
                    nvs.Add(new KeyValuePair<string, string>(className, style.Value));
                }

                style.Remove();
                var classs = htmlNode.Attributes["class"];
                if (classs != null)
                {
                    classs.Value = $"{classs.Value} {className}";
                }
                else
                {
                    htmlNode.Attributes.Add("class", className);
                }

                i++;
            }

            sb.AppendLine("</style>");

            var head = htmlDoc.QuerySelector("head");
            var doc = new HtmlDocument();
            doc.LoadHtml(sb.ToString());
            var node = doc.QuerySelector("style");
            head.AppendChild(node);

            var outerHtml = htmlDoc.DocumentNode.OuterHtml;
            return outerHtml;
        }
    }
}
