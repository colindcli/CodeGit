-- 截断数据库日志


-- Sqlserver2012 截断日记
ALTER DATABASE Ourtm SET RECOVERY SIMPLE WITH NO_WAIT
ALTER DATABASE Ourtm SET RECOVERY SIMPLE   --简单模式
DBCC SHRINKFILE (N'Ourtm_log' , 1, TRUNCATEONLY)   -- 1是大小  1M
ALTER DATABASE Ourtm SET RECOVERY FULL WITH NO_WAIT
ALTER DATABASE Ourtm SET RECOVERY FULL  --还原为完全模式