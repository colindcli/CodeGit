using log4net;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Web;

public static class LogHelper
{
    private static readonly ILog Fatallog = LogManager.GetLogger("Fatallog");
    private static readonly ILog Errorlog = LogManager.GetLogger("Errorlog");
    private static readonly ILog Warnlog = LogManager.GetLogger("Warnlog");
    private static readonly ILog Infolog = LogManager.GetLogger("Infolog");
    private static readonly ILog Debuglog = LogManager.GetLogger("Debuglog");
    private static readonly ILog Sqllog = LogManager.GetLogger("Sqllog");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    private static string GetName(MethodBase method)
    {
        var name = (method.ReflectedType != null ? "类名:[" + method.ReflectedType.Name + "]; " : "") + "方法:[" +
                   method.Name + "];\r\n";
        if (HttpContext.Current?.Request.Url != null)
        {
            name += "网址：" + HttpContext.Current.Request.Url + ";\r\n";
        }
        return name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Fatal(object message, Exception ex = null)
    {
        if (!Fatallog.IsFatalEnabled) return;
        var method = new StackTrace().GetFrame(1).GetMethod();
        Fatallog.Fatal(GetName(method) + message, ex);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Error(object message, Exception ex = null)
    {
        if (!Errorlog.IsErrorEnabled) return;
        var method = new StackTrace().GetFrame(1).GetMethod();
        Errorlog.Error(GetName(method) + message, ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Warn(object message, Exception ex = null)
    {
        if (!Warnlog.IsWarnEnabled) return;
        var method = new StackTrace().GetFrame(1).GetMethod();
        Warnlog.Warn(GetName(method) + message, ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Info(object message, Exception ex = null)
    {
        if (!Infolog.IsInfoEnabled) return;
        var method = new StackTrace().GetFrame(1).GetMethod();
        Infolog.Info(GetName(method) + message, ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Debug(object message, Exception ex = null)
    {
        if (!Debuglog.IsDebugEnabled) return;
        var method = new StackTrace().GetFrame(1).GetMethod();
        Debuglog.Debug(GetName(method) + message, ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void AddSqlLog(object message, Exception ex = null)
    {
        if (!Fatallog.IsFatalEnabled) return;
        var method = new StackTrace().GetFrame(1).GetMethod();
        Sqllog.Debug(GetName(method) + message, ex);
    }
}
