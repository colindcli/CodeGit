## markdown组件

> MarkdownSharp之md转html: [nuget](https://www.nuget.org/packages/MarkdownSharp/)


var md = new Markdown();
var result = md.Transform(text);
return Content(result, "text/html");