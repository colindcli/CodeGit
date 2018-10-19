using System;
using System.Configuration;
using System.Data.SqlClient;
using Z.BulkOperations;

public abstract class RepositoryBulkBase
{
    private static readonly string ConnString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ToString();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    protected void Db(Action<BulkOperation> action)
    {
        using (var conn = new SqlConnection(ConnString))
        {
            conn.Open();
            var bulk = new BulkOperation(conn)
            {
                BatchSize = 1000
            };
            action.Invoke(bulk);
            conn.Close();
            conn.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="action"></param>
    protected void Db<T>(Action<BulkOperation<T>> action) where T : class
    {
        using (var conn = new SqlConnection(ConnString))
        {
            conn.Open();
            var bulk = new BulkOperation<T>(conn)
            {
                BatchSize = 1000
            };
            action.Invoke(bulk);
            conn.Close();
            conn.Dispose();
        }
    }
}