--对比数据索引

SELECT CONCAT(t.schemaName,'.',t.tableName,' pk:',t.is_primary_key,' index:',t.type,' unique:',t.is_unique,' value:',(
	SELECT c.name+CASE WHEN x.is_descending_key=1 THEN ' DESC' ELSE ' ASC' END+',' FROM (
	SELECT * FROM sys.index_columns ic WHERE ic.object_id=t.object_id AND ic.index_id=t.index_id
	) x INNER JOIN sys.columns c ON x.object_id = c.object_id AND x.column_id = c.column_id
	INNER JOIN sys.indexes i ON x.object_id = i.object_id AND x.index_id = i.index_id
	WHERE x.key_ordinal>0
	ORDER BY x.object_id ASC,x.index_id ASC,x.key_ordinal ASC,x.is_descending_key ASC
	FOR XML PATH('')
)) piu FROM (
	SELECT SCHEMA_NAME(o.schema_id) schemaName,o.name tableName,i.type,i.type_desc,i.is_unique,i.is_primary_key,i.object_id,i.index_id FROM sys.indexes i 
	INNER JOIN sys.objects o ON o.type IN('U','V') AND i.object_id = o.object_id
) t ORDER BY t.schemaName,t.tableName,t.is_primary_key ASC,t.type ASC,t.is_unique;