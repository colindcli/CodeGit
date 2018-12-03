---表索引

--is_primary_key主键
--is_unique唯一值
--type:1聚集,2非聚集,0堆(没主键&&不是聚集)
SELECT * FROM sys.indexes i INNER JOIN sys.objects o ON o.type IN('U','V') AND i.object_id = o.object_id;


------------------------------------


--表索引字段

--key_ordinal索引排序(从小到大，等于0不要)
--is_descending_key顺序倒序(1倒序,0顺序)
DECLARE @ObjectId INT,@IndexId INT;

SELECT c.name+CASE WHEN x.is_descending_key=1 THEN ' DESC' ELSE ' ASC' END+',' FROM (
SELECT * FROM sys.index_columns ic WHERE ic.object_id=@ObjectId AND ic.index_id=@IndexId
) x INNER JOIN sys.columns c ON x.object_id = c.object_id AND x.column_id = c.column_id
INNER JOIN sys.indexes i ON x.object_id = i.object_id AND x.index_id = i.index_id
WHERE x.key_ordinal>0
ORDER BY x.object_id ASC,x.index_id ASC,x.key_ordinal ASC,x.is_descending_key ASC
FOR XML PATH('');