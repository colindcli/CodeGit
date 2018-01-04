--OUTER_KEYS
SELECT 
	FK = OBJECT_NAME(pt.constraint_object_id),
	Referenced_tbl = OBJECT_NAME(pt.referenced_object_id),
	Referencing_col = pc.name, 
	Referenced_col = rc.name
FROM sys.foreign_key_columns AS pt
INNER JOIN sys.columns AS pc
ON pt.parent_object_id = pc.[object_id]
AND pt.parent_column_id = pc.column_id
INNER JOIN sys.columns AS rc
ON pt.referenced_column_id = rc.column_id
AND pt.referenced_object_id = rc.[object_id]
WHERE pt.parent_object_id = OBJECT_ID(@tableName);


--INNER_KEYS
SELECT 
	[Schema] = OBJECT_SCHEMA_NAME(pt.parent_object_id),
	Referencing_tbl = OBJECT_NAME(pt.parent_object_id),
	FK = OBJECT_NAME(pt.constraint_object_id),
	Referencing_col = pc.name, 
	Referenced_col = rc.name
FROM sys.foreign_key_columns AS pt
INNER JOIN sys.columns AS pc
ON pt.parent_object_id = pc.[object_id]
AND pt.parent_column_id = pc.column_id
INNER JOIN sys.columns AS rc
ON pt.referenced_column_id = rc.column_id
AND pt.referenced_object_id = rc.[object_id]
WHERE pt.referenced_object_id = OBJECT_ID(@tableName);
