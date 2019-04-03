RESTORE DATABASE [Ourtm]
FROM DISK='E:\***.bak'
WITH
	NOUNLOAD,
	REPLACE,
	STATS=10,
	MOVE '***'
	TO 'E:\***.mdf',
	MOVE '***_log'
	TO 'E:\***_log.ldf';
GO