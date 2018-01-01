CREATE PROCEDURE [dbo].[Sp_Backup]
AS
--sp_configure 'show advanced options',1
--reconfigure
--go
--sp_configure 'xp_cmdshell',1
--reconfigure
--go

DECLARE @database varchar(50),@filename varchar(100),@PathBak varchar(300),@execStr varchar(300),@PathRar varchar(300),@PathRarMoveTo varchar(300);
SELECT @database=Name
FROM Master..SysDataBases
WHERE DbId=
	(
		SELECT Dbid
		FROM Master..SysProcesses
		WHERE Spid=@@spid
	);
SET @filename=CONVERT(Varchar(20), GETDATE(), 120);
SET @filename=@database+'_'+REPLACE(REPLACE(@filename, ':', ''), ' ', '_');
SET @PathBak='D:\'+@filename+'.bak';
SET @PathRar='D:\'+@filename+'.rar';
SET @PathRarMoveTo='D:\Data\Bak\'+@filename+'.rar';

DECLARE @filenameext Varchar(300);
SET @filenameext=@filename+'.rar';
SELECT	@filenameext AS filenameext,
		@PathRarMoveTo AS backuppath;

BACKUP DATABASE @database TO DISK=@PathBak;

SET @execStr='C:\PROGRA~2\WinRAR\winrar.exe a -ibck '+@PathRar+' '+@PathBak;
EXEC master..xp_cmdshell @execStr;

SET @execStr='DEL '+@PathBak;
EXEC master..xp_cmdshell @execStr;

SET @execStr='Move '+@PathRar+' '+@PathRarMoveTo;
EXEC master..xp_cmdshell @execStr;
