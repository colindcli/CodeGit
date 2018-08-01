using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

public abstract class RepositoryBase
{
    private static readonly string ConnString = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Data\\MyData.db";

    protected T Db<T>(Func<LiteDatabase, T> func)
    {
        using (var db = new LiteDatabase(ConnString))
        {
            return func.Invoke(db);
        }
    }

    protected void Db(Action<LiteDatabase> action)
    {
        using (var db = new LiteDatabase(ConnString))
        {
            action.Invoke(db);
        }
    }
}

public static class RepositoryExtension
{
    public static List<T> GetList<T>(this LiteDatabase db, Expression<Func<T, bool>> predicate = null)
    {
        return predicate == null
            ? db.GetCollection<T>().FindAll().ToList()
            : db.GetCollection<T>().Find(predicate).ToList();
    }

    public static void Insert<T>(this LiteDatabase db, T m)
    {
        var id = GetKeyValue(m);
        db.GetCollection<T>().Insert(id, m);
    }

    public static void Update<T>(this LiteDatabase db, T m)
    {
        var id = GetKeyValue(m);
        db.GetCollection<T>().Update(id, m);
    }

    public static T Get<T>(this LiteDatabase db, BsonValue id)
    {
        return db.GetCollection<T>().FindById(id);
    }

    public static void Delete<T>(this LiteDatabase db, Expression<Func<T, bool>> predicate)
    {
        db.GetCollection<T>().Delete(predicate);
    }

    /// <summary>
    /// 获取主键值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static BsonValue GetKeyValue<T>(T obj)
    {
        var type = typeof(T);
        var descriptor = new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);

        if (descriptor == null)
            return null;

        object id = null;
        foreach (PropertyDescriptor propertyDescriptor in descriptor.GetProperties())
        {
            var key = propertyDescriptor.Name;
            if (string.Equals(key, "id", StringComparison.OrdinalIgnoreCase))
            {
                id = propertyDescriptor.GetValue(obj);
                break;
            }

            var b = false;
            foreach (Attribute validationAttribute in propertyDescriptor.Attributes)
            {
                if (!(validationAttribute is KeyAttribute))
                    continue;

                b = true;
                break;
            }

            if (!b)
                continue;

            id = propertyDescriptor.GetValue(obj);
            break;
        }

        return id == null ? null : new BsonValue(id);
    }
}