using System.Collections.Generic;
using System.Linq;

public class RepositoryDa : BaseDb
{
    /// <summary>
    /// 插入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    public void Insert<T>(T model) where T : IFileDb
    {
        Db<T>(db =>
        {
            db.Insert(model);
        });
    }
    /// <summary>
    /// 批量插入数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lists"></param>
    public void InsertBulk<T>(List<T> lists) where T : IFileDb
    {
        Db<T>(db =>
        {
            db.InsertBulk(lists);
        });
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    public void Update<T>(T model) where T : IFileDb
    {
        Db<T>(db =>
        {
            db.Update(model);
        });
    }
    /// <summary>
    /// 批量更新数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lists"></param>
    public void Update<T>(List<T> lists) where T : IFileDb
    {
        Db<T>(db =>
        {
            db.Update(lists);
        });
    }

    /// <summary>
    /// 返回所有数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<T> GetList<T>() where T : IFileDb
    {
        var lists = new List<T>();
        Db<T>(db =>
        {
            lists = db.FindAll().ToList();
        });
        return lists;
    }
}