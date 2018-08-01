using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public abstract class RepositoryBase
{
    private static readonly string ConnString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ToString();

    protected T Db<T>(Func<IDbConnection, T> func)
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

    protected void Db(Action<IDbConnection> action)
    {
        using (var conn = new SqlConnection(ConnString))
        {
            conn.Open();
            action(conn);
            conn.Close();
            conn.Dispose();
        }
    }

    protected T Db<T>(Func<IDbConnection, IDbTransaction, T> func)
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

    protected void Db(Action<IDbConnection, IDbTransaction> action)
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

    public List<T> GetList<T>()
    {
        return Db(db => db.GetList<T>().ToList());
    }
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
