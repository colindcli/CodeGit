//Install-Package ExCSS -Version 2.0.6（最新版本是.Net Core）
//var stylesheet = new Parser().Parse(text);

public class ExCssHelper
{
    /// <summary>
    /// 提取样式中的字体图标链接
    /// </summary>
    /// <param name="stylesheet"></param>
    /// <returns></returns>
    public static List<string> GetCssDocumentFontUrl(StyleSheet stylesheet)
    {
        var paths = new List<string>();
        foreach (var stylesheetStyleRule in stylesheet.FontFaceDirectives)
        {
            foreach (var declaration in stylesheetStyleRule.Declarations)
            {
                if (declaration.Term is PrimitiveTerm declarationTerm)
                {
                    var primitiveTerm = declarationTerm;
                    if (primitiveTerm.PrimitiveType != UnitType.Uri)
                        continue;
                    var url = primitiveTerm.Value;
                    paths.Add(url.ToString());
                }
                else if (declaration.Term is TermList)
                {
                    var termLists = (TermList)declaration.Term;
                    paths.AddRange(from termList in termLists
                                    select termList as PrimitiveTerm into term
                                    select term into primitiveTerm
                                    where primitiveTerm?.PrimitiveType == UnitType.Uri && primitiveTerm.Value != null
                                    select primitiveTerm.Value into url
                                    select url.ToString());
                }
            }
        }

        return paths;
    }

    /// <summary>
    /// 提取样式中的背景链接
    /// </summary>
    /// <param name="stylesheet"></param>
    /// <param name="isIncludeBase64Image">是否包含Base64类型图片</param>
    /// <returns></returns>
    public static List<string> GetCssDocumentBackgroupUrl(StyleSheet stylesheet, bool isIncludeBase64Image)
    {
        var paths = new List<string>();
        foreach (var stylesheetStyleRule in stylesheet.StyleRules)
        {
            foreach (var declaration in stylesheetStyleRule.Declarations)
            {
                if (declaration.Term is PrimitiveTerm declarationTerm)
                {
                    var primitiveTerm = declarationTerm;
                    if (primitiveTerm.PrimitiveType != UnitType.Uri)
                        continue;
                    var url = primitiveTerm.Value;
                    paths.Add(url.ToString());
                }
                else if (declaration.Term is TermList)
                {
                    var termLists = (TermList)declaration.Term;
                    paths.AddRange(from termList in termLists
                                    select termList as PrimitiveTerm into term
                                    select term into primitiveTerm
                                    where primitiveTerm?.PrimitiveType == UnitType.Uri && primitiveTerm.Value != null
                                    select primitiveTerm.Value into url
                                    select url.ToString());
                }
            }
        }
        if (isIncludeBase64Image)
        {
            return paths;
        }
        return paths.Where(p => !p.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase)).ToList();
    }

    /// <summary>
    /// 提取样式中的Import链接
    /// </summary>
    /// <param name="stylesheet"></param>
    /// <returns></returns>
    public static List<string> GetCssDocumentImport(StyleSheet stylesheet)
    {
        var paths = (from import in stylesheet.ImportDirectives
                        where import.RuleType == RuleType.Import && !string.IsNullOrWhiteSpace(import.Href)
                        select import.Href
        ).ToList();

        return paths;
    }
}