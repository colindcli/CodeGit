using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class RepositoryBase
{
    private static string ConnStr { get; set; }

    public string ConnString
    {
        get
        {
            if (ConnStr != null)
            {
                return ConnStr;
            }

            ConnStr = Config.GetValue("ConnectionStrings:DefaultConnectionString");
            return ConnStr;
        }
    }

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
}