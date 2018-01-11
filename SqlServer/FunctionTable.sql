--创建表值函数
CREATE FUNCTION [dbo].[FnTable]
(
	@Id int
)
RETURNS @tb TABLE(Id int)
BEGIN
	INSERT INTO @tb(Id) values(@Id);
	RETURN;
END