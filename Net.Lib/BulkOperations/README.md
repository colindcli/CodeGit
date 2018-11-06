## 批量插入(批量添加)/批量更新/批量删除：BulkOperations


> [Z.BulkOperations](https://www.nuget.org/packages/Z.BulkOperations/) / [doc](https://bulk-operations.net/) / [github](https://github.com/zzzprojects/Bulk-Operations)

> SQL Server, SQL Azure, SQL Compact, MySQL and SQLite.

> 调用方法：RepositoryBulkBase.cs

Db<Demo>(db =>
{
    //添加
    //db.ColumnOutputExpression = m => new { m.DemoId }; //返回自增长主键
    //db.BulkInsert(items);


    //更新
    //db.IgnoreOnUpdateExpression = m => new { m.DemoName }; //排除字段
    //db.ColumnInputExpression = m => new { m.DemoId, m.DemoName }; //更新字段(主键是必须的)
    //db.BulkUpdate(items);


    //删除
    //db.BulkDelete(items);
});