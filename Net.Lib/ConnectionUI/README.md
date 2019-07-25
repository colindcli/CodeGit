## 数据库连接界面

> 缺点没有MySql连接


``` c#
/// <summary>
/// 获取VS.NET自带的数据库连接对话框的数据库连接信息
/// </summary>
/// <returns>数据库连接</returns>
public string GetDatabaseConnectionString()
{
    var connDialog = new DataConnectionDialog();

    connDialog.DataSources.Add(DataSource.AccessDataSource); // Access 
    connDialog.DataSources.Add(DataSource.OdbcDataSource); // ODBC
    connDialog.DataSources.Add(DataSource.OracleDataSource); // Oracle 
    connDialog.DataSources.Add(DataSource.SqlDataSource); // Sql Server
    connDialog.DataSources.Add(DataSource.SqlFileDataSource); // Sql Server File

    // 初始化
    connDialog.SelectedDataSource = DataSource.SqlDataSource;
    connDialog.SelectedDataProvider = DataProvider.SqlDataProvider;
    //也可以提前设计好连接字符串。
    //connDialog.ConnectionString = "Data Source=.;Initial Catalog=XJGasBottles_test;User ID=sa;Password=123456";
    //只能够通过DataConnectionDialog类的静态方法Show出对话框
    //不同使用dialog.Show()或dialog.ShowDialog()来呈现对话框

    var connString = string.Empty;
    if (DataConnectionDialog.Show(connDialog) == DialogResult.OK)
    {
        connString = connDialog.ConnectionString;
    }
    return connString;
}
```