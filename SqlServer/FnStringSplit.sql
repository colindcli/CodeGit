--分割字符串
CREATE FUNCTION [dbo].[FnStringSplit]
(
	@SplitString NVARCHAR(MAX),
	@Separator NVARCHAR(10)=','
)
RETURNS @SplitStringsTable TABLE
(
	[value] NVARCHAR(MAX)
)
AS
BEGIN
	IF(@SplitString IS NOT NULL AND @SplitString!='')
		BEGIN
		DECLARE @CurrentIndex int=1,@NextIndex int,@ReturnText NVARCHAR(MAX);
		WHILE (@CurrentIndex<=LEN(@SplitString)+1)
			BEGIN
			SET @NextIndex=CHARINDEX(@Separator, @SplitString, @CurrentIndex);
			IF (@NextIndex=0 OR @NextIndex IS NULL)
				SET @NextIndex=LEN(@SplitString)+1;

			SET @ReturnText=SUBSTRING(@SplitString, @CurrentIndex, @NextIndex-@CurrentIndex);
			IF (@ReturnText IS NOT NULL AND @ReturnText!='')
				INSERT INTO @SplitStringsTable ([value]) VALUES (@ReturnText);

			SET @CurrentIndex=@NextIndex+1;
			END
		END
	RETURN
END
