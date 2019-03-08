## 路由配置


- {filename}、{*filename}、正则表达式

routes.IgnoreRoute("FileApi/{[0-9a-z-]_files}/{filename}.png");
routes.IgnoreRoute("FileApi/{file}/{filename}.htm");
routes.IgnoreRoute("Files/CompanyLogos/{*filename}");