## EF Sqlite安装 （未成功）


数据源：
sqlite odbc: https://www.devart.com/odbc/sqlite/download.html （30天免费期）

扩展：
Vs2017没有sqlite数据源，需要安装扩展，vs2017工具->扩展和更新安装：SQLite/SQL Server Compact ToolBox

nuget：
sqlite安装： https://www.nuget.org/packages/System.Data.SQLite/
sqlite和ef安装: https://www.nuget.org/packages/System.Data.SQLite.EF6/


不支持vs2017



- https://github.com/ErikEJ/SqlCeToolbox/wiki/EF6-workflow-with-SQLite-DDEX-provider
Install Toolbox
Install SQLite in GAC
Install SQLite EF provider in project
Run EDM Wizard



