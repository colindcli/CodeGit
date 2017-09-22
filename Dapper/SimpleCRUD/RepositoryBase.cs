using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WeProject.DataAccess
{
    public abstract class RepositoryBase
    {
        private static readonly string ConnString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ToString();

        public T Db<T>(Func<IDbConnection, T> func)
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

        public void Db(Action<IDbConnection> action)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                action(conn);
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
