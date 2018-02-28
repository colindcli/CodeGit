using RazorEngine; //Install-Package RazorEngine
using RazorEngine.Templating;

public class TemplateHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="templatePath">cshtml文件路径</param>
    /// <param name="model"></param>
    /// <returns></returns>
    public static string ToHtml<T>(string templatePath, T model)
    {
        var template = System.IO.File.ReadAllText(templatePath);
        return Engine.Razor.RunCompile(template, template.GetHashCode().ToString(), model.GetType(), model);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string RenderHtml<T>(T obj)
    {
        var dir = $@"{AppDomain.CurrentDomain.BaseDirectory}Views\";

        var templatePath = $"{dir}Template.cshtml";
        var template = System.IO.File.ReadAllText(templatePath).Trim();

        //https://antaris.github.io/RazorEngine/LayoutAndPartial.html
        var layoutPath = $"{dir}_Layout.cshtml";
        var layoutTemplate = System.IO.File.ReadAllText(layoutPath).Trim();

        var templateName = (layoutTemplate + template).GetHashCode().ToString();
        var service = Engine.Razor;
        service.AddTemplate(layoutName, layoutTemplate);
        service.AddTemplate(templateName, template);
        service.Compile(templateName);
        var result = service.Run(templateName, null, obj).Trim();

        return result;
    }
}