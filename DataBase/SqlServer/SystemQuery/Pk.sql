--主键（已验证）
SELECT z.name FROM sys.indexes t 
INNER JOIN sys.index_columns y ON t.object_id=object_id('dbo.Article') AND t.is_primary_key=1 AND y.object_id=t.object_id AND y.index_id=t.index_id
INNER JOIN sys.syscolumns z ON z.id=t.object_id AND z.colid=y.column_id
ORDER BY z.colid ASC;



--
WITH indexCTE
AS
(
    SELECT ic.column_id,
           ic.index_column_id,
           ic.object_id
    FROM sys.indexes idx
    INNER JOIN sys.index_columns ic
        ON idx.index_id=ic.index_id
        AND idx.object_id=ic.object_id
    WHERE idx.object_id=OBJECT_ID(@Tablename)
        AND idx.is_primary_key=1
)
SELECT 
       colm.name ColumnName
FROM sys.columns colm
INNER JOIN sys.types systype
    ON colm.system_type_id=systype.system_type_id
    AND colm.user_type_id=systype.user_type_id
LEFT JOIN syscomments sc ON colm.default_object_id=sc.id
LEFT JOIN sys.extended_properties prop
    ON colm.object_id=prop.major_id
    AND colm.column_id=prop.minor_id
LEFT JOIN indexCTE
    ON colm.column_id=indexCTE.column_id
    AND colm.object_id=indexCTE.object_id
WHERE colm.object_id=OBJECT_ID(@Tablename)
AND CAST(CASE WHEN indexCTE.column_id IS NULL THEN 0 ELSE 1 END AS Bit)=1;



--other
SELECT COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity;
