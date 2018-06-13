--表索引
SELECT
	o.name [Key],
	i.index_id,
	i.name,
	CASE WHEN i.type_desc='CLUSTERED' THEN '聚集' ELSE '非聚集' END+': '+SUBSTRING(ikey.cols, 3, LEN(ikey.cols)) Value
FROM sys.objects o
JOIN sys.indexes i
	ON i.object_id=o.object_id
CROSS APPLY
(
	SELECT
		', '+c.name+CASE ic.is_descending_key WHEN 1 THEN ' DESC' ELSE ' ASC' END
	FROM sys.index_columns ic
	JOIN sys.columns c
		ON ic.object_id=c.object_id
		   AND ic.column_id=c.column_id
	WHERE ic.object_id=i.object_id
		  AND ic.index_id=i.index_id
		  AND ic.is_included_column=0
	ORDER BY ic.key_ordinal
	FOR XML PATH('')
) AS ikey(cols)
OUTER APPLY
(
	SELECT
		', '+c.name
	FROM sys.index_columns ic
	JOIN sys.columns c
		ON ic.object_id=c.object_id
		   AND ic.column_id=c.column_id
	WHERE ic.object_id=i.object_id
		  AND ic.index_id=i.index_id
		  AND ic.is_included_column=1
	ORDER BY ic.index_column_id
	FOR XML PATH('')
) AS inc(cols)
WHERE o.object_id IN
	  (
		  SELECT
			  id
		  FROM sys.sysobjects t
		  WHERE t.xtype='U'
	  )
	  AND i.type IN
		  (
			  1, 2
		  )
ORDER BY o.name,
	i.index_id;