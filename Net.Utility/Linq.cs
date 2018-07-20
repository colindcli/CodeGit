## Linq to object

//Left join 左连接
var lists = (from a in aLists
            join b in bLists on a.Id equals b.Key into ts
            from t in ts.DefaultIfEmpty()
            select new {a, t}).ToList();