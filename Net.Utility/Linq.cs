## Linq to object

//Left join 左连接
var lists = (from a in aLists
            join b in bLists on a.Id equals b.Key into ts
            from t in ts.DefaultIfEmpty()
            select new {a, t}).ToList();



public static IEnumerable<TResult> LeftJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
            IEnumerable<TInner> inner,
            Func<TSource, TKey> pk,
            Func<TInner, TKey> fk,
            Func<TSource, TInner, TResult> result)
        {
            return from s in source
                join i in inner
                    on pk(s) equals fk(i) into joinData
                from left in joinData.DefaultIfEmpty()
                select result(s, left);
        }


//list2的key值赋值给list1的key值
var rows = from a1 in list1
            join a2 in list2 on a1.Id equals a2.Id into temp
            let a = a1.Key = temp.FirstOrDefault()?.Key
            select a1;