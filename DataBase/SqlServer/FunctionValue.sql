--创建标量值函数
CREATE FUNCTION [dbo].[FnValue]
(
	@Id int
)
RETURNS int
AS
BEGIN
	RETURN @Id;
END