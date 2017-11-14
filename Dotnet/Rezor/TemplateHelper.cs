using RazorEngine;
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
}
