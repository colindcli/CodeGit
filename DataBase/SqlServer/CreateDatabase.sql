--创建数据库
CREATE DATABASE [Demo]
ON PRIMARY
	   (
		   NAME=N'Demo',
		   FILENAME=N'D:\Database\Demo.ndf',
		   SIZE=5MB,
		   MAXSIZE=UNLIMITED,
		   FILEGROWTH=1024KB
	   )
LOG ON
	(
		NAME=N'Demo_log',
		FILENAME=N'D:\Database\Demo_log.ldf',
		SIZE=1MB,
		MAXSIZE=UNLIMITED,
		FILEGROWTH=10%
	);
GO