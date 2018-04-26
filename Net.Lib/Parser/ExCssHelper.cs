//Install-Package ExCSS -Version 2.0.6（最新版本是.Net Core）

public class ExCssHelper
{
    /// <summary>
    /// 提取Css样式中的链接
    /// Install-Package ExCSS -Version 2.0.6（最新版本是.Net Core）
    /// </summary>
    /// <param name="cssDocument"></param>
    /// <param name="isIncludeBase64Image">是否包含Base64类型图片</param>
    /// <returns></returns>
    public static List<string> GetCssDocumentUrl(string cssDocument, bool isIncludeBase64Image)
    {
        var parser = new Parser();
        var stylesheet = parser.Parse(cssDocument);

        var paths = (from import in stylesheet.ImportDirectives
            where import.RuleType == RuleType.Import && !string.IsNullOrWhiteSpace(import.Href)
            select import.Href
        ).ToList();

        foreach (var stylesheetStyleRule in stylesheet.StyleRules)
        {
            foreach (var declaration in stylesheetStyleRule.Declarations)
            {
                var declarationTerm = declaration.Term as PrimitiveTerm;
                if (declarationTerm != null)
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
}