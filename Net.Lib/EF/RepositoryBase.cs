using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using WeProject.Common.log4;

namespace Project.DataAccess
{
    public class RepositoryBase : IDisposable
    {
        private static readonly string ConnString = ConfigurationManager.AppSettings["DefaultConnStr"].ToString();

        public RepositoryBase()
        {

        }

        private static readonly int warnMilliseconds = 300;
        public T Db<T>(Func<IDbConnection, T> func)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var sw = new Stopwatch();
                sw.Start();
                var result = func(conn);
                sw.Stop();
                if (sw.ElapsedMilliseconds >= warnMilliseconds)
                {
                    LogHelper.AddSqlLog($"[Dapper 1]警告({sw.ElapsedMilliseconds}ms):{func.Target.GetType()}.{func.Method.Name}");
                }
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
                var sw = new Stopwatch();
                sw.Start();
                action(conn);
                sw.Stop();
                if (sw.ElapsedMilliseconds >= warnMilliseconds)
                {
                    LogHelper.AddSqlLog($"[Dapper 2]警告({sw.ElapsedMilliseconds}ms):{action.Target.GetType()}.{action.Method.Name}");
                }
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        public class DataLog
        {
            public List<string> Logs { get; set; } = new List<string>();

            public void AddLog(string s)
            {
                if (s.StartsWith("Opened connection") || s.StartsWith("Closed connection") || s.StartsWith("-- Executing at"))
                {
                    return;
                }
                Logs.Add(s);
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public T Query<T>(Func<WePorjectDbContext, T> func)
        {
            var context = new WePorjectDbContext();
            var log = new DataLog();
            context.Database.Log = log.AddLog;
            var sw = new Stopwatch();
            sw.Start();
            var result = func(context);
            sw.Stop();
            if (sw.ElapsedMilliseconds >= warnMilliseconds)
            {
                LogHelper.AddSqlLog($"[EF Query 1]警告({sw.ElapsedMilliseconds}ms):{func.Target.GetType()}.{func.Method.Name}\r\n{string.Join("", log.Logs)}");
            }
            return result;
        }
        [System.Diagnostics.DebuggerStepThrough]
        public void Query(Action<WePorjectDbContext> actions)
        {
            var context = new WePorjectDbContext();
            var log = new DataLog();
            context.Database.Log = log.AddLog;
            var sw = new Stopwatch();
            sw.Start();
            actions(context);
            sw.Stop();
            if (sw.ElapsedMilliseconds >= warnMilliseconds)
            {
                LogHelper.AddSqlLog($"[EF Query 2]警告({sw.ElapsedMilliseconds}ms):{actions.Target.GetType()}.{actions.Method.Name}\r\n{string.Join("", log.Logs)}");
            }
        }
        [System.Diagnostics.DebuggerStepThrough]
        public T Execute<T>(Func<WePorjectDbContext, T> func)
        {
            var context = new WePorjectDbContext();
            var log = new DataLog();
            context.Database.Log = log.AddLog;
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                context.Database.BeginTransaction();
                T t = func(context);
                context.SaveChanges();
                context.Database.CurrentTransaction.Commit();
                sw.Stop();
                if (sw.ElapsedMilliseconds >= warnMilliseconds)
                {
                    LogHelper.AddSqlLog($"[EF Execute 1]警告({sw.ElapsedMilliseconds}ms):{func.Target.GetType()}.{func.Method.Name}\r\n{string.Join("", log.Logs)}");
                }
                return t;
            }
            catch (Exception ex)
            {
                sw.Stop();
                context.Database.CurrentTransaction.Rollback();
                LogHelper.Fatal(ex.Message, ex);
                throw;
            }
        }
        [System.Diagnostics.DebuggerStepThrough]
        public void Execute(Action<WePorjectDbContext> actions)
        {
            var context = new WePorjectDbContext();
            var log = new DataLog();
            context.Database.Log = log.AddLog;
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                context.Database.BeginTransaction();
                actions(context);
                context.SaveChanges();
                context.Database.CurrentTransaction.Commit();
                sw.Stop();
                if (sw.ElapsedMilliseconds >= warnMilliseconds)
                {
                    LogHelper.AddSqlLog($"[EF Execute 2]警告({sw.ElapsedMilliseconds}ms):{actions.Target.GetType()}.{actions.Method.Name}\r\n{string.Join("", log.Logs)}");
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                context.Database.CurrentTransaction.Rollback();
                LogHelper.Fatal(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            //不能Dispose 因为有些查询是延时的,调用释放会在真正执行时出异常
            //context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
