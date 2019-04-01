//Install-Package Dapper.SimpleCRUD
//Install-Package ExecuteSqlBulk
using Dapper;
using ExecuteSqlBulk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

    public int Add<T>(T m)
    {
        return Db(db => db.Insert<int, T>(m));
    }

    public TReturn Add<TReturn, T>(T m)
    {
        return Db(db => db.Insert<TReturn, T>(m));
    }

    public Task<TReturn> AddAsync<TReturn, T>(T m)
    {
        return Task.Run(() => Db(db => db.Insert<TReturn, T>(m)));
    }

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
        if (whereConditions == null)
        {
            return Db(db => db.GetList<T>().ToList());
        }
        if (whereConditions is string)
        {
            return Db(db => db.GetList<T>(whereConditions.ToString()).ToList());
        }
        return Db(db => db.GetList<T>(whereConditions).ToList());
    }

    public Task<List<T>> GetListAsync<T>(object whereConditions = null)
    {
        if (whereConditions == null)
        {
            return Task.Run(() => Db(db => db.GetList<T>().ToList()));
        }
        if (whereConditions is string)
        {
            return Task.Run(() => Db(db => db.GetList<T>(whereConditions.ToString()).ToList()));
        }
        return Task.Run(() => Db(db => db.GetList<T>(whereConditions).ToList()));
    }

    public List<T> GetListByBulk<T>(object whereConditions)
    {
        return Db(db => db.GetListByBulk<T>(whereConditions).ToList());
    }

    public Task<List<T>> GetListByBulkAsync<T>(object whereConditions)
    {
        return Task.Run(() => Db(db => db.GetListByBulk<T>(whereConditions).ToList()));
    }

    public bool Update<T>(T m)
    {
        return Db(db => db.Update(m)) > 0;
    }

    public Task<bool> UpdateAsync<T>(T m)
    {
        return Task.Run(() => Db(db => db.Update(m)) > 0);
    }

    public bool Delete<T>(T m)
    {
        return Db(db => db.Delete(m)) > 0;
    }

    public Task<bool> DeleteAsync<T>(T m)
    {
        return Task.Run(() => Db(db => db.Delete(m)) > 0);
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
    /// <summary>
    /// 批量获取列表(依赖Dapper)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="whereConditions"></param>
    /// <returns></returns>
    private static string ToSelectSql<T>(object whereConditions)
    {
        var name = typeof(T).Name;
        var sb = new StringBuilder($"SELECT * FROM {name}");
        var fields = whereConditions?.GetType().GetProperties();
        if (fields?.Length > 0)
        {
            sb.Append(" WHERE");
            var addAnd = false;
            foreach (var field in fields)
            {
                if (addAnd)
                {
                    sb.Append(" AND");
                }
                else
                {
                    addAnd = true;
                }

                var fieldName = field.Name;
                var fieldValue = field.GetValue(whereConditions);
                switch (fieldValue)
                {
                    case string _:
                        sb.Append($" {fieldName}=@{fieldName}");
                        break;
                    case IEnumerable _:
                        sb.Append($" {fieldName} IN @{fieldName}");
                        break;
                    default:
                        sb.Append($" {fieldName}=@{fieldName}");
                        break;
                }
            }
        }

        sb.Append(";");

        return sb.ToString();
    }
    
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="connection"></param>
    /// <param name="whereConditions"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetListByBulk<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        var sqlStr = ToSelectSql<T>(whereConditions);
        return connection.Query<T>(sqlStr, whereConditions, transaction: transaction, commandTimeout: commandTimeout);
    }

    public static void Update<T>(this SqlConnection db, object id, Action<T> action, SqlTransaction tran = null)
    {
        var obj = db.Get<T>(id, tran);
        if (obj == null)
        {
            return;
        }

        action.Invoke(obj);
        db.Update(obj, tran);
    }
}