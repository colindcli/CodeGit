--数据表所有字段

--所有表视图
WITH tbs AS(
	SELECT SCHEMA_ID(OBJECT_SCHEMA_NAME(o.object_id)) schemaId,OBJECT_SCHEMA_NAME(o.object_id) schemaName,o.object_id,OBJECT_NAME(o.object_id) tableName FROM sys.objects o 
	WHERE o.type IN('U','V')
),
--主键列
pk AS(
	SELECT i.object_id,ic.column_id,c.name,i.is_primary_key FROM sys.indexes i 
	INNER JOIN sys.objects o ON i.object_id=o.object_id AND o.type IN('U','V')
	INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
	INNER JOIN sys.columns c ON i.object_id = c.object_id AND ic.column_id = c.column_id
	WHERE i.is_primary_key=1
),
li AS(
	SELECT o.schemaName,o.tableName,c.column_id columnId,c.name columnName,c.max_length maxLength,ISNULL(p.is_primary_key, 0) isPrimaryKey,c.is_nullable isNullable,ISNULL(ep.value, '') remark,t.name dataType,CASE WHEN ic.object_id IS NOT NULL THEN 1 ELSE 0 END isIncrement FROM sys.columns c 
	INNER JOIN tbs o ON c.object_id = o.object_id
	INNER JOIN sys.types t ON c.user_type_id=t.user_type_id
	LEFT JOIN pk p ON c.object_id = p.object_id AND c.column_id = p.column_id
	LEFT JOIN sys.extended_properties ep ON ep.major_id=c.object_id AND ep.minor_id=c.column_id
	LEFT JOIN sys.identity_columns ic ON c.object_id = ic.object_id AND c.column_id = ic.column_id
)
SELECT * FROM li t ORDER BY t.schemaName ASC,t.tableName ASC,t.columnId ASC;