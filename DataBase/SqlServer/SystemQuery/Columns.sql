SELECT 
	TABLE_CATALOG AS [Database],
	TABLE_SCHEMA AS Owner, 
	TABLE_NAME AS TableName, 
	COLUMN_NAME AS ColumnName, 
	ORDINAL_POSITION AS OrdinalPosition, 
	COLUMN_DEFAULT AS DefaultSetting, 
	IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType, 
	CHARACTER_MAXIMUM_LENGTH AS MaxLength, 
	DATETIME_PRECISION AS DatePrecision,
	COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity,
	COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsComputed') as IsComputed,
	(SELECT TOP 1 t.value FROM sys.extended_properties t WHERE t.major_id=OBJECT_ID(COLUMNS.TABLE_NAME) AND minor_id=COLUMNS.ORDINAL_POSITION) Remark
FROM  INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME=@tableName AND TABLE_SCHEMA=@schemaName
ORDER BY OrdinalPosition ASC;


--2
SELECT
	x.name [Key],
	y.name Value
FROM sysobjects x
INNER JOIN syscolumns y
	ON x.xtype='U'
	   AND y.id=x.id
ORDER BY x.name ASC,
	y.name ASC;