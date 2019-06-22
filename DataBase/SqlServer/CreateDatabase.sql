USE [master]
GO

--创建数据库
CREATE DATABASE [Agent]
ON PRIMARY
	   (
		   NAME=N'Agent',
		   FILENAME=N'E:\Db2008\Agent.ndf',
		   SIZE=5MB,
		   MAXSIZE=UNLIMITED,
		   FILEGROWTH=1024KB
	   )
LOG ON
	(
		NAME=N'Agent_log',
		FILENAME=N'E:\Db2008\Agent_log.ldf',
		SIZE=1MB,
		MAXSIZE=UNLIMITED,
		FILEGROWTH=10%
	);
GO

