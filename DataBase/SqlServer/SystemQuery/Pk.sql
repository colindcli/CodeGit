--主键
SELECT i.object_id,ic.column_id,c.name,i.is_primary_key FROM sys.indexes i 
INNER JOIN sys.objects o ON i.object_id=o.object_id AND o.type IN('U','V')
INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
INNER JOIN sys.columns c ON i.object_id = c.object_id AND ic.column_id = c.column_id
WHERE i.is_primary_key=1;
