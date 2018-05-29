--查找区间日期
DECLARE @BeginDate date='2017-11-15',@EndDate date='2017-11-20';

SELECT * FROM(
	SELECT '2017-11-01' BeginDate,'2017-11-30' EndDate
	UNION ALL
	SELECT '2017-12-01' BeginDate,'2017-12-31' EndDate
	UNION ALL
	SELECT '2018-01-01' BeginDate,'2018-01-31' EndDate
) a WHERE
	(
		(@BeginDate BETWEEN a.BeginDate AND a.EndDate)
		OR
		(a.BeginDate>=@BeginDate and a.EndDate<=@EndDate)
		OR
		(@EndDate BETWEEN a.BeginDate AND a.EndDate)
	);