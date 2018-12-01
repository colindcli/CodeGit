--查询自增长表和列
SELECT
	so.name TableName,						--表名字
	sc.name ColumnName,						--自增字段名字
	IDENT_CURRENT(so.name) CurrentValue,	--自增字段当前值
	IDENT_INCR(so.name) IncrValue,			--自增字段增长值
	IDENT_SEED(so.name) SeedValue			--自增字段种子值
FROM sysobjects so
INNER JOIN syscolumns sc
	ON so.id=sc.id
	   AND COLUMNPROPERTY(sc.id, sc.name, 'IsIdentity')=1;