--查询数据库中所有表的记录条数以及占用磁盘空间大小
SELECT
	TableName=obj.name,
	TotalRows=prt.rows,
	[SpaceUsed(KB)]=SUM(alloc.used_pages)* 8
FROM sys.objects obj
JOIN sys.indexes idx
	ON obj.object_id=idx.object_id
JOIN sys.partitions prt
	ON obj.object_id=prt.object_id
JOIN sys.allocation_units alloc
	ON alloc.container_id=prt.partition_id
WHERE obj.type='U'
	  AND idx.index_id IN
		  (
			  0, 1
		  )
GROUP BY obj.name,
	prt.rows
ORDER BY [SpaceUsed(KB)] DESC;



--查询硬盘空间
Exec master.dbo.xp_fixeddrives


--查询数据库服务器各数据库日志文件的大小及利用率
DBCC SQLPERF(LOGSPACE)