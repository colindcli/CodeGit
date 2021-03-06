--执行最慢的SQL语句
SELECT
	(total_elapsed_time/execution_count)/1000 N'平均时间ms',
	total_elapsed_time/1000 N'总花费时间ms',
	total_worker_time/1000 N'所用的CPU总时间ms',
	total_physical_reads N'物理读取总次数',
	total_logical_reads/execution_count N'每次逻辑读次数',
	total_logical_reads N'逻辑读取总次数',
	total_logical_writes N'逻辑写入总次数',
	execution_count N'执行次数',
	SUBSTRING(st.text, (qs.statement_start_offset/2)+1,
	((CASE statement_end_offset
		WHEN-1 THEN DATALENGTH(st.text)
		ELSE qs.statement_end_offset
	END
	-qs.statement_start_offset)/2)+1) N'执行语句',
	creation_time N'语句编译时间',
	last_execution_time N'上次执行时间'
FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) st
WHERE SUBSTRING(st.text, (qs.statement_start_offset/2)+1,
	((CASE statement_end_offset
		WHEN-1 THEN DATALENGTH(st.text)
		ELSE qs.statement_end_offset
	END
	-qs.statement_start_offset)/2)+1) NOT LIKE '�tch%'
ORDER BY total_elapsed_time/execution_count DESC;


--总耗CPU最多的前个SQL:
SELECT TOP 20
	total_worker_time/1000 AS [总消耗CPU 时间(ms)],
	execution_count [运行次数],
	qs.total_worker_time/qs.execution_count/1000 AS [平均消耗CPU 时间(ms)],
	last_execution_time AS [最后一次执行时间],
	max_worker_time/1000 AS [最大执行时间(ms)],
	SUBSTRING(qt.text, qs.statement_start_offset/2+1,
	(CASE
		WHEN qs.statement_end_offset=-1 THEN DATALENGTH(qt.text)
		ELSE qs.statement_end_offset
	END-qs.statement_start_offset)/2+1)
	AS [使用CPU的语法],
	qt.text [完整语法],
	qt.dbid,
	dbname=DB_NAME(qt.dbid),
	qt.objectid,
	OBJECT_NAME(qt.objectid, qt.dbid) ObjectName
FROM sys.dm_exec_query_stats qs WITH (NOLOCK)
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS qt
WHERE execution_count>1
ORDER BY total_worker_time DESC


--平均耗CPU最多的前个SQL:
SELECT TOP 20
	total_worker_time/1000 AS [总消耗CPU 时间(ms)],
	execution_count [运行次数],
	qs.total_worker_time/qs.execution_count/1000 AS [平均消耗CPU 时间(ms)],
	last_execution_time AS [最后一次执行时间],
	min_worker_time/1000 AS [最小执行时间(ms)],
	max_worker_time/1000 AS [最大执行时间(ms)],
	SUBSTRING(qt.text, qs.statement_start_offset/2+1,
	(CASE
		WHEN qs.statement_end_offset=-1 THEN DATALENGTH(qt.text)
		ELSE qs.statement_end_offset
	END-qs.statement_start_offset)/2+1)
	AS [使用CPU的语法],
	qt.text [完整语法],
	qt.dbid,
	dbname=DB_NAME(qt.dbid),
	qt.objectid,
	OBJECT_NAME(qt.objectid, qt.dbid) ObjectName
FROM sys.dm_exec_query_stats qs WITH (NOLOCK)
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS qt
WHERE execution_count>1
ORDER BY (qs.total_worker_time/qs.execution_count/1000) DESC;
