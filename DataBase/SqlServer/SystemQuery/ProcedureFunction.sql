--获取所有的存储过程、函数
SELECT
	b.xtype+'.'+b.name [Key],
	a.text Value
FROM syscomments a
INNER JOIN sysobjects b
	ON b.id=a.id
ORDER BY b.xtype ASC,
	b.name ASC;