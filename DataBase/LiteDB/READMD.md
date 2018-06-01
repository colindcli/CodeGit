## LiteDb

> 图形管理工具
- https://github.com/JosefNemec/LiteDbExplorer (推荐)
- https://github.com/falahati/LiteDBViewer


> 排除不要的字段：BsonMapper.Global.Entity<Customer>().Ignore(p => p.DateTime);


> 注：Insert Upsert如使用BsonValue，则实体不能含有Id字段，相反同理。