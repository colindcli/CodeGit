--查找所有下级
WITH Temp AS(
	SELECT * FROM dbo.Tree t WHERE t.Id=1
	UNION ALL
	SELECT x.* FROM dbo.Tree x INNER JOIN Temp y ON x.ParentId=y.Id
)
SELECT * FROM Temp t;

--查找所有上级
WITH Temp AS(
	SELECT * FROM dbo.Tree t WHERE t.Id=2
	UNION ALL
	SELECT x.* FROM dbo.Tree x INNER JOIN Temp y ON x.Id=y.ParentId
)
SELECT * FROM Temp t;