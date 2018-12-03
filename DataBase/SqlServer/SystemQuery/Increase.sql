--查询到行时有自增长


--increment_value初始值
--seed_value增长值
--last_value当前值
SELECT * FROM sys.identity_columns ic WHERE ic.object_id=1;