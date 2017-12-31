CREATE FUNCTION [dbo].[FnTable]
(
	@Id int
)
RETURNS @tb TABLE(Id int)
BEGIN
	INSERT INTO @tb(Id) values(@Id);
	RETURN;
END