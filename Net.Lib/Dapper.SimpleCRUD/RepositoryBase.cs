using Dapper;
using ExecuteSqlBulk;
using MyProject.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

public abstract class RepositoryBase
{
    private static readonly string ConnString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ToString();

    protected T Db<T>(Func<SqlConnection, T> func)
    {
        using (var conn = new SqlConnection(ConnString))
        {
            conn.Open();
            var result = func(conn);
            conn.Close();
            conn.Dispose();
            return result;
        }
    }

    protected void Db(Action<SqlConnection> action)
    {
        using (var conn = new SqlConnection(ConnString))
        {
            conn.Open();
            action(conn);
            conn.Close();
            conn.Dispose();
        }
    }

    protected T Db<T>(Func<SqlConnection, SqlTransaction, T> func)
    {
        using (var conn = new SqlConnection(ConnString))
        {
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
                var result = func(conn, tran);
                tran.Commit();
                conn.Close();
                conn.Dispose();
                return result;
            }
            catch (Exception)
            {
                tran.Rollback();
                conn.Close();
                conn.Dispose();
                throw;
            }
        }
    }

    protected void Db(Action<SqlConnection, SqlTransaction> action)
    {
        using (var conn = new SqlConnection(ConnString))
        {
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
                action(conn, tran);
                tran.Commit();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception)
            {
                tran.Rollback();
                conn.Close();
                conn.Dispose();
                throw;
            }
        }
    }

    #region 操作

    public T Get<T>(object id)
    {
        return Db(db => db.Get<T>(id));
    }

    public Task<T> GetAsync<T>(object id)
    {
        return Task.Run(() => Db(db => db.Get<T>(id)));
    }

    public List<T> GetList<T>(object whereConditions = null)
    {
        return Db(db => db.GetList<T>(whereConditions).ToList());
    }

    public Task<List<T>> GetListAsync<T>(object whereConditions = null)
    {
        return Task.Run(() => Db(db => db.GetList<T>(whereConditions).ToList()));
    }

    public int Update<T>(T m)
    {
        return Db(db => db.Update(m));
    }

    public Task<int> UpdateAsync<T>(T m)
    {
        return Task.Run(() => Db(db => db.Update(m)));
    }

    public int Delete<T>(T m)
    {
        return Db(db => db.Delete(m));
    }

    public Task<int> DeleteAsync<T>(T m)
    {
        return Task.Run(() => Db(db => db.Delete(m)));
    }

    public int DeleteList<T>(List<T> list)
    {
        return Db(db => db.DeleteList<T>(list));
    }

    public Task<int> DeleteListAsync<T>(List<T> list)
    {
        return Task.Run(() => Db(db => db.DeleteList<T>(list)));
    }

    public void BulkInsert<T>(List<T> list)
    {
        Db(db => db.BulkInsert(list));
    }

    public int BulkUpdate<T>(List<T> list, Func<T, object> columnUpdateExpression, Func<T, object> columnPrimaryKeyExpression) where T : new()
    {
        return Db(db => db.BulkUpdate(list, columnUpdateExpression, columnPrimaryKeyExpression));
    }

    public int BulkDelete<T>(List<T> list, Func<T, object> columnPrimaryKeyExpression) where T : new()
    {
        return Db(db => db.BulkDelete(list, columnPrimaryKeyExpression));
    }

    public void BulkDeleteAll<T>()
    {
        Db(db => db.BulkDelete<T>());
    }

    #endregion
}

public static class RepositoryExtension
{
    public static void Update<T>(this IDbConnection db, object id, Action<T> action, IDbTransaction tran = null)
    {
        var obj = db.Get<T>(id, tran);
        if (obj == null)
            return;

        action.Invoke(obj);
        db.Update(obj, tran);
    }
}
