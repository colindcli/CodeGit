### SqlServer脚本

- 系统表列查询：SystemQuery
- 创建表值函数：FunctionTable.sql
- 创建标量值函数：FunctionValue.sql

- 数据备份：Backup.sql
- 日志截断：DeleteLog_2012.sql

- 日期转换：DateVarchar.sql [转换示例](https://github.com/colindcli/CodeGit/blob/master/SqlServer/images/date.png)
- 分割字符串：FnStringSplit.sql
- 查找区间日期：BetweenDate.sql


## 查询

- 连续排序：ROW_NUMBER()OVER(ORDER BY t.TypeId ASC) Id
- 分组连续排序：ROW_NUMBER()OVER(PARTITION BY t.TypeId ORDER BY t.CreateDate DESC) Id，取每组第一条则：Id=1

- 跳跃排序：RANK()OVER(ORDER BY t.TypeId ASC)
- 分组跳跃排序：DENSE_RANK()OVER(PARTITION BY t.TypeId ORDER BY t.CreateDate DESC)
