using LiteDB;
using System;

protected void Db<T>(Action<LiteCollection<T>> collection) where T : IFileDb
{
    var name = typeof(T).Name;
    using (var db = new LiteDatabase("MyData.db"))
    {
        var gc = db.GetCollection<T>(name);
        collection.Invoke(gc);
    }
}